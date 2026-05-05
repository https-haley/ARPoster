using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PandaARPlacer : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject pandaObject;
    public GameObject placementIndicator;
    public Text placementHintText;

    private bool pandaPlaced = false;
    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (pandaPlaced) return;

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        bool hitFound = arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        if (placementIndicator != null)
            placementIndicator.SetActive(hitFound);

        if (!hitFound) return;

        var pose = hits[0].pose;

        if (placementIndicator != null)
            placementIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);

        bool tapped = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#if UNITY_EDITOR
        tapped = tapped || Input.GetMouseButtonDown(0);
#endif
        if (tapped)
            PlacePanda(pose.position, pose.rotation);
    }

    void PlacePanda(Vector3 position, Quaternion rotation)
    {
        pandaPlaced = true;

        if (placementIndicator != null)
            placementIndicator.SetActive(false);

        if (pandaObject != null)
        {
            pandaObject.transform.position = position;

            Vector3 directionToCamera = Camera.main.transform.position - pandaObject.transform.position;
            directionToCamera.y = 0;

            if (directionToCamera.sqrMagnitude > 0.001f)
            {
                pandaObject.transform.rotation =
                    Quaternion.LookRotation(directionToCamera) * Quaternion.Euler(0, 0f, 0);
            }

            pandaObject.SetActive(true);
        }

        if (placementHintText != null)
            placementHintText.gameObject.SetActive(false);
    }
}
