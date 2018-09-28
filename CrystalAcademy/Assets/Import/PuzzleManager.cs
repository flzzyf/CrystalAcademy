using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour 
{
    public int size;
    public Texture2D image;
    public GameObject prefab_block;

	void Start () 
	{
        Camera.main.orthographicSize = size * 0.55f;

        GeneratePuzzle();
	}

    void GeneratePuzzle()
    {
        Texture2D[,] imageSlices = ImageSlicer.Slice(image, size);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                GameObject go = Instantiate(prefab_block, transform);
                go.transform.position = -Vector2.one * (size - 1) * .5f + new Vector2(x, y);

                Block block = go.AddComponent<Block>();
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
            }
        }
    }
}
