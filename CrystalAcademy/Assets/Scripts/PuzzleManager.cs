using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public List<Slot> slots;
    const int circleSize = 3;

    public GameObject prefab_block;

    public BlockPrefab[] blockPrefabs;

    public static Transform blockBeingDragged;

    List<Block> blocks;
    int GetBlockID
    {
        get { return blocks.Count; }
    }

    public Transform blockParent;

    public void Init()
    {
        //初始化法阵槽点
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].blockID = i;
            slots[i].x = i % circleSize;
            slots[i].y = i / circleSize;
        }

        blocks = new List<Block>();
        //初始裁剪处理预设，生成裁剪好的拼图
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            Texture2D[,] imageSlices = ImageSlicer.Slice(blockPrefabs[i].image, blockPrefabs[i].sliceCount.x);
            for (int y = 0; y < blockPrefabs[i].sliceCount.y; y++)
            {
                for (int x = 0; x < blockPrefabs[i].sliceCount.x; x++)
                {
                    Block block = new Block();
                    block.id = GetBlockID;
                    block.image = imageSlices[x, y];
                    block.prefab_unit = blockPrefabs[i].prefab_unit;
                    blocks.Add(block);

                    //设定相邻块
                    if (x > 0) { block.leftID = x - 1; }
                    if (x < blockPrefabs[i].sliceCount.x - 1) { block.rightID = x + 1; }
                    if (y > 0) { block.upID = y - 1; }
                    if (y < blockPrefabs[i].sliceCount.y - 1) { block.downID = y + 1; }
                }
            }
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            GameObject block = Instantiate(prefab_block, blockParent);
            block.GetComponent<BlockItem>().block = blocks[i];
            block.GetComponent<BlockItem>().Init();
        }
    }
}

[System.Serializable]
public class BlockPrefab
{
    public Texture2D image;
    public Vector2Int sliceCount = new Vector2Int(1, 1);
    public GameObject prefab_unit;

}
