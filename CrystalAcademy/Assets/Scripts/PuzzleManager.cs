using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public int circleSize = 3;

    CircleNode[] nodes;

    public void Init()
    {
        nodes = new CircleNode[circleSize * circleSize];
        for (int i = 0; i < circleSize * circleSize; i++)
        {
            nodes[i] = new CircleNode(i % circleSize, i / circleSize);
            print(nodes[i].x + ", " + nodes[i].y);
        }
    }

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

//拼图块
public class Block
{
    int id;
    int rightID, leftID, upID, downID;
    GameObject prefab_unit;
}
