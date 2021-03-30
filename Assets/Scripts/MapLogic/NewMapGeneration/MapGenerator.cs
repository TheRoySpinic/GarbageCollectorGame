using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Generate
{
    public class MapGenerator : SingletonGen<MapGenerator>
    {
        [SerializeField]
        private GameObject segmentPrefab = null;

        [SerializeField]
        private int segmentCounts = 20;

        private Queue<MapSegment> mapSegments = new Queue<MapSegment>();

        private Transform tr = null;

        private void Awake()
        {
            tr = transform;
        }

        private void NextSegment()
        {
            if(mapSegments.Count < segmentCounts)
            {
                for (int i = mapSegments.Count; i < segmentCounts; i++)
                {
                    SpawnSegment();
                }
            }

            //генерим новый индекс
            //получаем индексы существующих сегментов из кеша
            //сверяем с числом неповторяющихся для биома

            //сетаем сегменту из очереди позицию и параметры
        }

        private void SpawnSegment()
        {
            MapSegment segment = Instantiate(segmentPrefab, tr).GetComponent<MapSegment>();
            mapSegments.Enqueue(segment);
        }
    }
}