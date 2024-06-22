using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraZoom: MonoBehaviour
{
     private Camera cam;
    private Vector3 zoomInPosition;
    private Vector3 originalPosition;
    private float zoomInSize;
    private float originalSize;
    private float zoomSpeed;
    private bool isOrthographic;
    Action onZoomComplete;
    public void Initialize(Camera camera, Vector3 zoomInPosition, float zoomInSize, float zoomSpeed, bool isOrthographic, Action onZoomComplete)
    {
        this.cam = camera;
        this.zoomInPosition = new Vector3(zoomInPosition.x, zoomInPosition.y, cam.transform.position.z);
        this.originalPosition = cam.transform.position;
        this.zoomInSize = zoomInSize;
        this.originalSize = isOrthographic ? cam.orthographicSize : cam.fieldOfView;
        this.zoomSpeed = zoomSpeed;
        this.isOrthographic = isOrthographic;
        this.onZoomComplete = onZoomComplete;
    }

    public void StartZoom(float duration)
    {
        StartCoroutine(ZoomCoroutine(duration));
    }

    private IEnumerator ZoomCoroutine(float duration)
    {
        yield return StartCoroutine(ZoomIn());
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(ZoomOut());

        onZoomComplete?.Invoke();
    }

    private IEnumerator ZoomIn()
    {
        while (Vector3.Distance(cam.transform.position, zoomInPosition) > 0.01f ||
               (isOrthographic ? Mathf.Abs(cam.orthographicSize - zoomInSize) > 0.01f : Mathf.Abs(cam.fieldOfView - zoomInSize) > 0.01f))
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, zoomInPosition, Time.deltaTime * zoomSpeed);

            if (isOrthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, Time.deltaTime * zoomSpeed);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomInSize, Time.deltaTime * zoomSpeed);
            }

            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        while (Vector3.Distance(cam.transform.position, originalPosition) > 0.01f ||
               (isOrthographic ? Mathf.Abs(cam.orthographicSize - originalSize) > 0.01f : Mathf.Abs(cam.fieldOfView - originalSize) > 0.01f))
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, originalPosition, Time.deltaTime * zoomSpeed);

            if (isOrthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalSize, Time.deltaTime * zoomSpeed);
            }

            yield return null;
        }
    }
}
