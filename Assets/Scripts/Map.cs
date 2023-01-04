using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    Transform[] battleSpace;

    Camera cam;



    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //CastRay();
    }
    RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
        Vector3 screenMousePosNear = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        RaycastHit hit;
        Physics.Raycast(screenMousePosNear, screenMousePosFar - screenMousePosNear, out hit);
        Debug.DrawRay(screenMousePosNear, screenMousePosFar - screenMousePosNear, Color.green);
        return hit;

    }
}
