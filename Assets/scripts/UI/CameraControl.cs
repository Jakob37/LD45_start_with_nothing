using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 0f;
    private float smoothTime = 4.3f;

    public float zoomInLevel = 0.8f;
    public float zoomOutLevel = 2f;

    private bool newLevelSet;

    void Awake() {
        cam = Camera.main;
        targetZoom = zoomInLevel;
        cam.orthographicSize = zoomInLevel;
    }

    void Start () {
        newLevelSet = false;
    }

    public void SetTargetZoom() {
        targetZoom = zoomOutLevel;
        newLevelSet = true;
    }

    void Update () {
        /* 
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            targetZoom = zoomInLevel;
            newLevelSet = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            targetZoom = zoomOutLevel;
            newLevelSet = true;
        }*/

        //float scrollData;
        //scrollData = Input.GetAxis("Mouse ScrollWheel");

        //targetZoom -= scrollData * zoomFactor;
        //targetZoom = Mathf.Clamp(targetZoom, 1.5f, 8f);
        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref smoothTime, Time.deltaTime);

        if (newLevelSet) {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref smoothTime, 1f);
        }
    }
}
