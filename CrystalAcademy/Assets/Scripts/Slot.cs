using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int x, y;

    public Block block
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).GetComponent<BlockItem>().block;

            return null;
        }
    }

    public bool blockExists { get { return transform.childCount > 0; } }

    public void OnDrop(PointerEventData eventData)
    {
        if (!blockExists)
        {
            PuzzleManager.blockBeingDragged.SetParent(transform);
            PuzzleManager.blockBeingDragged.localPosition = Vector2.zero;
        }
    }
}
