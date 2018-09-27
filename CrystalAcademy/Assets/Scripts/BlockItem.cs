using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Block block;

    Vector2 startPos;
    Transform startParent;

    public void Init()
    {
        //设置图案为block
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PuzzleManager.blockBeingDragged = transform;
        startPos = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PuzzleManager.blockBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == startParent)
        {
            transform.position = startPos;
        }
        else
        {
            transform.localPosition = Vector2.zero;
        }
    }
}
