using Balance;
using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate
{
    public class MapGenerator : SingletonGen<MapGenerator>
    {
        public int centerLineIndex { get; private set; } = 4;

        [SerializeField]
        private Transform playerTransform = null;

        public EBiomeType currentBiome = EBiomeType.NONE;
        
        //map segment
        [SerializeField]
        private GameObject segmentPrefab = null;
        private Queue<MapSegment> mapSegments = new Queue<MapSegment>();
        private float currentPosition = 0;
        private float lastSegmentSize = 0;
        //-------------

        //road collider
        [SerializeField]
        private GameObject roadCollider = null;
        [SerializeField]
        private float roadColliderSize = 10;
        private float lastRoadColliderPosition = 0;
        private Queue<GameObject> roadColliderQueue = new Queue<GameObject>();
        [SerializeField]
        private float setNextRoadColliderDistance = 500;
        [SerializeField]
        private int roadSegmentCount = 3;
        //--------------
        
        private Transform tr = null;

        private MapBalance mapBalance = null;

        private List<int> lastIndexes = new List<int>();

        private MapBalance.BiomeConfig _cachedBiome = null;
        private MapBalance.BiomeConfig cachedBiome { get { return GetCurrentBiomeConfig(); } }

        private List<EBiomeType> lastBiomes = new List<EBiomeType>();

        private int maxBiomeSize = 0;
        private int biomeSegmentCounts = 0;


#if UNITY_EDITOR
        [SerializeField]
        private bool isDebugMode = false;
#endif

        public override void Init()
        {
            tr = transform;

            GameBalance.E_ConfigReady -= LoadBalance;

            if (mapBalance == null)
            {
                GameBalance.E_ConfigReady += LoadBalance;
            }
            else
            {
                LoadBalance();
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (isDebugMode)
                return;
#endif
            if(maxBiomeSize != 0 && biomeSegmentCounts > maxBiomeSize)
            {
                CheckNextBiome();
            }

            if(cachedBiome != null && currentPosition - playerTransform.position.x < cachedBiome.setNextSegmentDistance)
            {
                NextSegment();
            }

            if(lastRoadColliderPosition - playerTransform.position.x < setNextRoadColliderDistance)
            {
                NextRoadCollider();
            }
        }


        public void ResetMap()
        {
            currentPosition = 0;

            lastRoadColliderPosition = 0;

            lastBiomes.Clear();
            SetBiome(mapBalance.startBiome);

            maxBiomeSize = 0;
            biomeSegmentCounts = 0;

            FillMap();
        }

        public void ResetMap(EBiomeType biomeType)
        {
            currentPosition = 0;

            lastRoadColliderPosition = 0;

            lastBiomes.Clear();
            SetBiome(biomeType);

            maxBiomeSize = 0;
            biomeSegmentCounts = 0;

            FillMap();
        }

        public static MapBalance.BiomeConfig GetCurrentBiomeConfig()
        {
            if (instance._cachedBiome != null && instance._cachedBiome.biomeType.Equals(instance.currentBiome))
            {
                return instance._cachedBiome;
            }
            else
            {
                if (instance.mapBalance == null)
                    instance.LoadBalance();

                instance._cachedBiome = Array.Find(instance.mapBalance.biomes, (b) => { return b.biomeType.Equals(instance.currentBiome); });
                return instance._cachedBiome;
            }
        }

        public static MapBalance.BiomeConfig GetBiomeConfig(EBiomeType biomeType)
        {
            return Array.Find(instance.mapBalance.biomes, (b) => { return b.biomeType.Equals(biomeType); });
        }

        public void SetBiome(EBiomeType biomeType)
        {
            lastBiomes.Add(biomeType);

            currentBiome = biomeType;
            if (biomeType != EBiomeType.PREVIEW)
                maxBiomeSize = UnityEngine.Random.Range(cachedBiome.minBiomeSegments, cachedBiome.maxBiomeSegments);
            else
                maxBiomeSize = 0;

            biomeSegmentCounts = 0;
        }


        private void LoadBalance()
        {
            mapBalance = GameBalance.GetMapBalance();
        }

        private void CheckNextBiome()
        {
            //Проверяем есть ли обязательный к спавну биом. Если биом есть то завершаем подбор
            if(cachedBiome.nextBiome != EBiomeType.NONE)
            {
                SetBiome(cachedBiome.nextBiome);
                SpawnTransition();
                return;
            }

            bool active = true;
            int step = 0;
            while(active && step < 100)
            {
                step++;

                //Случайно с шансом выбираем следующий биом
                EBiomeType newBiome = GetBiomeByChange();

                //Проверяем значение "сколько раз должен неповторятся". Если биом есть то завершаем подбор
                int saveCount = GetBiomeConfig(newBiome).biomeSaveDublicateIndexes;
                if(lastBiomes.Count > saveCount)
                {
                    bool valideBiome = true;

                    for (int i = lastBiomes.Count - 1; i >= 0; --i)
                    {
                        if(lastBiomes[i].Equals(newBiome))
                        {
                            valideBiome = false;
                            break;
                        }
                    }

                    if (valideBiome)
                    {
                        active = false;
                    }
                    else
                    {
                        continue;
                    }
                }

                //Если сменили биом то спавним переход
                if(newBiome != currentBiome)
                {
                    SetBiome(newBiome);
                    SpawnTransition();
                    break;
                }
            }
        }

        private void SpawnTransition()
        {
            MapBalance.TransitionConfig transitionConfig = Array.Find(cachedBiome.transitions, (t) => { return t.previousBiome.Equals(lastBiomes[lastBiomes.Count - 2]); });

            if(transitionConfig != null && transitionConfig.useTransition)
            {
                MapBalance.SegmentConfig segmentConfig = null;

                if(transitionConfig.copyFromIndex != -1)
                {
                    segmentConfig = cachedBiome.transitions[transitionConfig.copyFromIndex].segment;
                }
                else
                {
                    segmentConfig = transitionConfig.segment;
                }

                NextSegment(segmentConfig);
            }
        }

        private EBiomeType GetBiomeByChange()
        {
            List<float> changes = new List<float>();

            float sum = 0;

            foreach(EBiomeType b in mapBalance.avableToRandom) 
            {
                    float value = GetBiomeConfig(b).biomeSpawnChange;
                    changes.Add(value);
                    sum += value;
            }

            float rand = UnityEngine.Random.Range(0, sum);
            sum = 0;

            for(int i = 0; i < changes.Count; ++i)
            {
                if(sum <= rand && rand < sum + changes[i])
                {
                    return mapBalance.avableToRandom[i];
                }
                sum += changes[i];
            }

            return EBiomeType.NONE;
        }

        private void FillMap()
        {
            GetCurrentBiomeConfig();

            maxBiomeSize = CalcBiomeSize();

            for (int i = 0; i < mapBalance.segmentCounts; i++)
            {
                NextSegment();
            }

            for (int i = 0; i < roadSegmentCount; i++)
            {
                NextRoadCollider();
            }
        }

        private int CalcBiomeSize()
        {
            return UnityEngine.Random.Range(cachedBiome.minBiomeSegments, cachedBiome.maxBiomeSegments);
        }

        private void NextSegment()
        {
            if(mapSegments.Count < mapBalance.segmentCounts)
            {
                for (int i = mapSegments.Count; i < mapBalance.segmentCounts; i++)
                {
                    SpawnSegment();
                }
            }

            bool active = true;
            int step = 0;

            while (active && step < 100)
            {
                step++;
                //генерим новый индекс
                int nextIndex = UnityEngine.Random.Range(0, GetCurrentBiomeConfig().segments.Length);

                //сверяем с числом неповторяющихся
                if (lastIndexes.Contains(nextIndex))
                {
                    continue;
                }

                if (lastIndexes.Count >= cachedBiome.segmentSaveDublicateIndexes)
                {
                    lastIndexes.RemoveAt(0);
                }

                active = false;
                lastIndexes.Add(nextIndex);
                //сетаем сегменту из очереди позицию и параметры
                MapSegment segment = mapSegments.Dequeue();

                segment.SetSegmentPosition(GetNextPosition());
                segment.NextSegment(nextIndex);

                lastSegmentSize = segment.size;

                mapSegments.Enqueue(segment);

                ++biomeSegmentCounts;
            }
        }

        private void NextSegment(MapBalance.SegmentConfig segmentConfig)
        {
            MapSegment segment = mapSegments.Dequeue();

            segment.SetSegmentPosition(GetNextPosition());
            segment.NextSegment(segmentConfig);

            lastSegmentSize = segment.size;

            mapSegments.Enqueue(segment);

            ++biomeSegmentCounts;
        }

        private void SpawnSegment()
        {
            MapSegment segment = Instantiate(segmentPrefab, tr).GetComponent<MapSegment>();
            mapSegments.Enqueue(segment);
        }

        private Vector3 GetNextPosition()
        {
            currentPosition += lastSegmentSize;

            return new Vector3(currentPosition, 0, 0);
        }

        private void NextRoadCollider()
        {
            GameObject last;

            if(roadColliderQueue.Count < roadSegmentCount)
            {
                last = Instantiate(roadCollider, tr);
            }
            else
            {
                last = roadColliderQueue.Dequeue();
            }

            Transform lt = last.transform;

            lt.localPosition = new Vector3(lastRoadColliderPosition, lt.localPosition.y, lt.localPosition.z);

            lastRoadColliderPosition += roadColliderSize;

            roadColliderQueue.Enqueue(last);
        }
    }
}