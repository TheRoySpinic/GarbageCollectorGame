using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapFiller : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerCar;

        [Header("Map objects"), Space]
        [SerializeField]
        private GameObject segmentPrefab;

        [SerializeField]
        private List<GameObject> activeSegments;

        [Header("Setup"), Space]
        [SerializeField]
        private float segmentSizeStep = 10;
        [SerializeField]
        private float minSegmentDestroyDistance = 10;
        [SerializeField]
        private float createNewSegmentDistance = 100;

        private void Awake()
        {
            FillMap();
        }

        private void Update()
        {
            if (transform.TransformPoint(activeSegments[0].transform.position).x < playerCar.transform.position.x
                && Vector3.Distance(activeSegments[0].transform.position, playerCar.transform.position) > minSegmentDestroyDistance)
            {
                Destroy(activeSegments[0]);
                activeSegments.RemoveAt(0);
            }

            if (Vector3.Distance(activeSegments[activeSegments.Count - 1].transform.position, playerCar.transform.position) < createNewSegmentDistance)
            {
                CreateMapSegment();
            }
        }

        private void FillMap()
        {
            for (int i = 0; i < 12; i++)
            {
                CreateMapSegment();
            }
        }

        private void CreateMapSegment()
        {
            GameObject segment = Instantiate(GetNextSegment(), transform);
            if (UnityEngine.Random.Range(0, 1f) > 0.7f)
            {
                MapSegment mapSegment = segment.GetComponent<MapSegment>();
                mapSegment.haveBaricade[UnityEngine.Random.Range(0, 3)] = true;
                mapSegment.UpdateBaricade();
            }
            segment.transform.localPosition = new Vector3(activeSegments[activeSegments.Count - 1].transform.localPosition.x + segmentSizeStep, segment.transform.localPosition.y, segment.transform.localPosition.z);
            activeSegments.Add(segment);
        }

        private GameObject GetNextSegment()
        {
            return segmentPrefab;
        }
    }
}