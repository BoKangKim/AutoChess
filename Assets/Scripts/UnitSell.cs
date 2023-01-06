using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using ZoneSystem;


public class UnitSell : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    DragAndDrop dragAndDrop;
    private void Awake()
    {
        dragAndDrop = FindObjectOfType<DragAndDrop>();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(dragAndDrop.selectedObject != null)
        {
            UIManager.Inst.unitInstButton.image.color = Color.black;
            UIManager.Inst.unitInstButton.enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragAndDrop.selectedObject != null)
        {
            UIManager.Inst.unitInstButton.image.color = Color.white;
            UIManager.Inst.unitInstButton.enabled = true;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
