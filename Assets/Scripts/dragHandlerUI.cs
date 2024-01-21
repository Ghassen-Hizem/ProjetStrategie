using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragHandlerUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool onDragActive = false;

    public InstantiatePlayer instantiateScript;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        onDragActive = true;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        onDragActive = false;
        instantiateScript.InstantiateAPlayer();
    }

    
}
