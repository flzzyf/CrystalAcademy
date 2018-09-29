using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int id;
    public int rightID, leftID, upID, downID;
    public GameObject prefab_unit;
    public Texture2D image;

    public Block()
    {
        rightID = -1;
        leftID = -1;
        upID = -1;
        downID = -1;
    }
}
