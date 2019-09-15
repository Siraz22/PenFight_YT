using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargets : MonoBehaviour
{

    public List<Transform> Targets;

    public Vector3 offset;

    private Vector3 velocity;
    public float smoothTime;

    public Camera cam;
    
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float ZoomLimiter = 50f;

    private void LateUpdate()
    {
        if (Targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }
       
    void Zoom()
    {
        //Debug.Log(GetGtreatestDist());

        float newZoom = Mathf.Lerp(maxZoom,minZoom,GetGtreatestDist() / ZoomLimiter);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGtreatestDist()
    {
        var bounds = new Bounds(Targets[0].position, Vector3.zero);

        for(int i =0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }

        return bounds.size.x;
    }

    void Move()
    {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(Targets.Count == 1)
        {
            return Targets[0].position;
        }

        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        for(int i=0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }

        return bounds.center;
    }
}
