using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    int id;
    int rightID, leftID, upID, downID;
    GameObject prefab_unit;

    public void Init(Texture2D _image)
    {
        GetComponent<MeshRenderer>().material.mainTexture = _image;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Transparent");
    }
}
