using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FruitSpawner : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    public GameObject orangePrefab;
    public GameObject mangoPrefab;
    public GameObject bananaPrefab;
    public GameObject strawberryPrefab;
    public GameObject peachPrefab;
    public GameObject applePrefab;

    private List<GameObject> spawnedFruit = new List<GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            if (spawnedFruit.Count == 0)
            {
                SpawnFruit(trackedImage.transform);
            }
        }

        foreach (var trackedImage in args.updated)
        {
            bool isTracking = trackedImage.trackingState == TrackingState.Tracking;

            foreach (GameObject fruit in spawnedFruit)
            {
                fruit.SetActive(isTracking);
            }
        }
    }

    void SpawnFruit(Transform poster)
    {
        // Top row
        SpawnOne(orangePrefab, poster, new Vector3(-0.11f, 0.02f, 0.065f));
        SpawnOne(mangoPrefab,  poster, new Vector3(0.00f, 0.02f, 0.065f));
        SpawnOne(bananaPrefab, poster, new Vector3(0.12f, 0.02f, 0.065f));

        // Bottom row
        SpawnOne(strawberryPrefab, poster, new Vector3(-0.11f, 0.02f, -0.065f));
        SpawnOne(peachPrefab,      poster, new Vector3(0.00f, 0.02f, -0.065f));
        SpawnOne(applePrefab,      poster, new Vector3(0.11f, 0.02f, -0.065f));
    }

    void SpawnOne(GameObject prefab, Transform parent, Vector3 position)
    {
        if (prefab == null) return;

        GameObject fruit = Instantiate(prefab, parent);

        fruit.transform.localPosition = position;
        fruit.transform.rotation = Quaternion.identity;

        spawnedFruit.Add(fruit);
    }
}