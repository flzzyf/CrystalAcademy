using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Transform block
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0);

            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (block == null)
        {
            PuzzleManager.blockBeingDragged.SetParent(transform);
        }
    }
}
