using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapFiller : SingletonGen<MapFiller>
    {
        public static Action E_CreateSegment;
        [SerializeField]
        private GameObject playerCar = null;

        [Header("Map objects"), Space]
        [SerializeField]
        private List<GameObject> activeSegments = new List<GameObject>();

        [Header("Setup"), Space]
        [SerializeField]
        private float startSegmentsCount = 12;
        [SerializeField]
        private float nextSegmentSizeStep = 10;
        [SerializeField]
        private float minSegmentDestroyDistance = 10;
        [SerializeField]
        private float createNewSegmentDistance = 100;

        [Header("Base segments")]
        [SerializeField]
        private List<GameObject> arrivalBase = new List<GameObject>();
        [SerializeField]
        private List<GameObject> previewBase = new List<GameObject>();

        private Transform mapTransform = null;
        private Transform carTransform = null;
        private Transform firstTransform = null;
        private Transform lastTransform = null;

        private bool previewMode = true;

        override public void Init()
        {
            mapTransform = transform;
            carTransform = playerCar.transform;

            FillMap();
        }

        private void Update()
        {
            if (Vector3.Distance(lastTransform.position, carTransform.position) < createNewSegmentDistance)
            {
                CreateMapSegment();
            }

            if (activeSegments.Count > 0 && mapTransform.TransformPoint(firstTransform.position).x < carTransform.position.x
                && Vector3.Distance(firstTransform.position, carTransform.position) > minSegmentDestroyDistance)
            {
                Destroy(activeSegments[0]);
                activeSegments.RemoveAt(0);
                firstTransform = activeSegments[0].transform;
            }
        }


        public void SetPreviewMode(bool mode)
        {
            previewMode = mode;
        }

        public void ReloadMap(bool previewMode)
        {
            SetPreviewMode(previewMode);

            FillMap();
        }


        private void FillMap()
        {
            ClearMap();

            foreach(GameObject o in previewMode ? previewBase : arrivalBase)
            {
                GameObject segment = Instantiate(o, mapTransform);

                lastTransform = segment.transform;

                lastTransform.localPosition = firstTransform != null ? 
                    new Vector3(lastTransform.localPosition.x + lastTransform.GetComponent<MapSegment>().prefabSize, lastTransform.localPosition.y, lastTransform.localPosition.z) 
                    : Vector3.zero;

                if (!firstTransform)
                    firstTransform = segment.transform;

                activeSegments.Add(segment);
            }

            for (int i = 0; i < startSegmentsCount; i++)
            {
                CreateMapSegment();
            }
        }

        private void ClearMap()
        {
            while(mapTransform.childCount > 0)
            {
                DestroyImmediate(activeSegments[0]);
                activeSegments.RemoveAt(0);
            }

            firstTransform = null;
            lastTransform = null;
        }

        private void CreateMapSegment()
        {
            GameObject prefab = previewMode ? previewBase[UnityEngine.Random.Range(0, previewBase.Count)] : GetNextSegment();
            nextSegmentSizeStep = previewMode ? prefab.GetComponent<MapSegment>().prefabSize : nextSegmentSizeStep;


            GameObject segment = Instantiate(prefab, mapTransform);
            Transform sgTrasform = segment.transform;
            sgTrasform.localPosition = new Vector3(lastTransform.localPosition.x + nextSegmentSizeStep, sgTrasform.localPosition.y, sgTrasform.localPosition.z);
            activeSegments.Add(segment);
            lastTransform = sgTrasform;
            E_CreateSegment?.Invoke();
        }

        private GameObject GetNextSegment()
        {
            return MapManager.instance.GetNextSegment(out nextSegmentSizeStep);
        }
    }
}