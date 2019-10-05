using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  private Camera cam;
  private float targetZoom;
  private float zoomFactor = 0f;
  private float smoothTime = 4.3f;

  void Start () {
    cam = Camera.main;
    targetZoom = cam.orthographicSize;
  }

  void SetTargetZoom() {

  }

  void Update () {

    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      targetZoom = 1;
    }

    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      targetZoom = 3;
    }

    //float scrollData;
    //scrollData = Input.GetAxis("Mouse ScrollWheel");

    //targetZoom -= scrollData * zoomFactor;
    //targetZoom = Mathf.Clamp(targetZoom, 1.5f, 8f);
    //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref smoothTime, Time.deltaTime);

    cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref smoothTime, 1f);
  }
}
