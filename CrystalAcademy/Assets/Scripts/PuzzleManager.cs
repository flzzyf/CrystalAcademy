using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public int circleSize = 3;

    CircleNode[] nodes;

    public static Transform blockBeingDragged;

    public Texture2D image;
    public GameObject prefab_block;


    public void Init()
    {
        nodes = new CircleNode[circleSize * circleSize];
        for (int i = 0; i < circleSize * circleSize; i++)
        {
            nodes[i] = new CircleNode(i % circleSize, i / circleSize);
            print(nodes[i].x + ", " + nodes[i].y);
        }
    }

    // void GeneratePuzzle2()
    // {
    //     Texture2D[,] imageSlices = ImageSlicer.Slice(image, size);
    //     for (int y = 0; y < size; y++)
    //     {
    //         for (int x = 0; x < size; x++)
    //         {
    //             GameObject go = Instantiate(prefab_block, transform);
    //             go.transform.position = -Vector2.one * (size - 1) * .5f + new Vector2(x, y);

    //             Block block = go.AddComponent<Block>();
    //             block.Init(new Vector2Int(x, y), imageSlices[x, y]);
    //         }
    //     }
    // }

    //法阵节点
    class CircleNode
    {
        public int x, y;
        public int blockID;

        public CircleNode(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
