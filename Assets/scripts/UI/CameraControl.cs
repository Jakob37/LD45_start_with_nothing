using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  //public Transform center_trans;

  //private Vector3 orig_pos;

  private Camera cam;
  private float targetZoom;
  private float zoomFactor = 3f;
  private float zoomLerpSpeed = 10;

  void Start () {
    //center_trans = GameObject.FindObjectOfType<Player>().gameObject.transform;
    //orig_pos = transform.position;

    cam = Camera.main;
    targetZoom = cam.orthographicSize;
  }

  void SetTargetZoom() {

  }

  void Update () {

   //transform.position = new Vector3(center_trans.position.x, center_trans.position.y, orig_pos.z);

    float scrollData;
    scrollData = Input.GetAxis("Mouse ScrollWheel");

    targetZoom -= scrollData * zoomFactor;
    targetZoom = Mathf.Clamp(targetZoom, 1.5f, 8f);
    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

  }
}
