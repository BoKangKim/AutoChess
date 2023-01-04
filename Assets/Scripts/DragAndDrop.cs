using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    GameObject selectedObject;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if(selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                   
                }
            }
            else
            {

                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
                //Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                Vector3 worldPosition = cam.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);

                selectedObject = null;
            }
        }
        if (selectedObject != null)
        {

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = cam.ScreenToWorldPoint(position);

            selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
        }
    }

    RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
        Vector3 screenMousePosNear = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        RaycastHit hit;
        Physics.Raycast(screenMousePosNear, screenMousePosFar - screenMousePosNear, out hit);
        Debug.DrawRay(screenMousePosNear, screenMousePosFar - screenMousePosNear, Color.red);
        return hit;

    }
    
}
