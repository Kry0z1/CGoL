using System;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scale(float axis)
    {
        cam.orthographicSize = Math.Max(1, cam.orthographicSize - axis * zoomSpeed);
    }

    public void SetScale(float scale)
    {
        cam.orthographicSize = scale;
    }
}
