using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Raycaster
{
    Camera raycastCamera;
    Plane toyHeightPlane;

    public Raycaster(Camera raycastCamera, float toyPickupHeight)
    {
        this.raycastCamera = raycastCamera;
        toyHeightPlane = new Plane(Vector3.up, -toyPickupHeight);
    }
    
    public bool CheckRaycastHit(out RaycastHit hit, LayerMask layer, float distance)
    {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, distance, layer);
    }
    public bool CheckRaycastPlane(out float hit, out Vector3 hitPos)
    {
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        if (toyHeightPlane.Raycast(ray, out hit))
        {
            hitPos = ray.GetPoint(hit);
            return true;
        }
        else
        {
            hitPos = Vector3.zero;
            return false;
           
        }
    }
}
