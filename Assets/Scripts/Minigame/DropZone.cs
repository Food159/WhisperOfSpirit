using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private string zoneTag;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            DragDrop drag = eventData.pointerDrag.GetComponent<DragDrop>();
            if(drag != null) 
            {
                drag.icemanager.CheckAnswer(zoneTag, drag.gameObject);
            }
        }
    }
}
