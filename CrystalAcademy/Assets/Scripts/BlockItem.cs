using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockItem : MonoBehaviour, IDragHandler
{
    public Block block;

    bool mouseDown;

    public void Init()
    {
        //设置图案为block
    }

    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
