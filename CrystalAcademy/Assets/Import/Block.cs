using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{
    public Vector2Int pos;

	public void Init(Vector2Int _pos, Texture2D _image)
    {
        pos = _pos;

        GetComponent<MeshRenderer>().material.mainTexture = _image;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Transparent");
    }
}
