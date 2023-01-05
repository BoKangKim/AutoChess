using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    GameObject selectedObject;
    Camera cam;
    int unitLayer;
    int spaceLayer;
    Vector3 beforePoint;

    private void Start()
    {
        cam = Camera.main;
        spaceLayer = 1 << LayerMask.NameToLayer("Space");
        unitLayer = 1 << LayerMask.NameToLayer("Unit");

    }
    private void Update()
    {


        Debug.Log(CastRay(spaceLayer).collider);


        if (Input.GetMouseButtonDown(0))
        {
            if(selectedObject == null)
            {
                CastRay(unitLayer);
                if (CastRay(unitLayer).collider != null)
                {
                    selectedObject = CastRay(unitLayer).collider.gameObject;
                    beforePoint = CastRay(spaceLayer).collider.gameObject.transform.position;
                }
            }
            //Drop
            else
            {
                if (CastRay(spaceLayer).collider != null)
                {
                    Vector3 worldPosition = CastRay(spaceLayer).collider.gameObject.transform.position;
                    selectedObject.transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);

                    selectedObject = null;
                }
                else
                {
                    selectedObject.transform.position = new Vector3(beforePoint.x , 0.5f, beforePoint.z) ;
                    selectedObject = null;

                }
               
            }
        }
        if (selectedObject != null)
        {

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = cam.ScreenToWorldPoint(position);

            selectedObject.transform.position = new Vector3(worldPosition.x, 1f, worldPosition.z);
        }
    }


    RaycastHit CastRay(int Layer)
    {
        Vector3 screenMousePosFar = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));
        Vector3 screenMousePosNear = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        Physics.Raycast(screenMousePosNear, screenMousePosFar - screenMousePosNear, out RaycastHit hit, Vector3.Magnitude(screenMousePosFar - screenMousePosNear), Layer);
        Debug.DrawRay(screenMousePosNear, screenMousePosFar - screenMousePosNear, Color.red);

        return hit;

    }




}
