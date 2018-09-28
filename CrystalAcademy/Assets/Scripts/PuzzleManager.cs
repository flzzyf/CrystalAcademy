using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public List<Slot> slots;
    const int circleSize = 3;
    Slot[,] slotArray;

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
        slotArray = new Slot[circleSize, circleSize];
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].blockID = i;
            slots[i].x = i % circleSize;
            slots[i].y = i / circleSize;

            slotArray[i % circleSize, i / circleSize] = slots[i];
        }

        blocks = new List<Block>();
        //初始裁剪处理预设，生成裁剪好的拼图
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            //设定临时Block数列
            Block[,] tempBlocks = new Block[blockPrefabs[i].sliceCount.x, blockPrefabs[i].sliceCount.y];
            Texture2D[,] imageSlices = ImageSlicer.Slice(blockPrefabs[i].image, blockPrefabs[i].sliceCount.x);
            //创建拼图块
            for (int y = 0; y < blockPrefabs[i].sliceCount.y; y++)
            {
                for (int x = 0; x < blockPrefabs[i].sliceCount.x; x++)
                {
                    Block block = new Block();
                    block.id = GetBlockID;
                    block.image = imageSlices[x, y];
                    block.prefab_unit = blockPrefabs[i].prefab_unit;
                    blocks.Add(block);
                    tempBlocks[x, y] = block;
                }
            }
            //设定相邻块
            for (int y = 0; y < blockPrefabs[i].sliceCount.y; y++)
            {
                for (int x = 0; x < blockPrefabs[i].sliceCount.x; x++)
                {
                    if (x > 0) { tempBlocks[x, y].leftID = tempBlocks[x - 1, y].id; }
                    if (x < blockPrefabs[i].sliceCount.x - 1) { tempBlocks[x, y].rightID = tempBlocks[x + 1, y].id; }
                    if (y > 0) { tempBlocks[x, y].upID = tempBlocks[x, y - 1].id; }
                    if (y < blockPrefabs[i].sliceCount.y - 1) { tempBlocks[x, y].downID = tempBlocks[x, y + 1].id; }
                }
            }
        }
        for (int i = 0; i < blocks.Count; i++)
        {
            GameObject block = Instantiate(prefab_block, blockParent);
            block.GetComponent<RectTransform>().anchoredPosition = new Vector3(150 * i, 0, 0);
            block.GetComponent<BlockItem>().block = blocks[i];
            block.GetComponent<BlockItem>().Init();
        }
    }

    public void Summon()
    {
        //遍历每个槽点
        for (int y = 0; y < circleSize; y++)
        {
            for (int x = 0; x < circleSize; x++)
            {
                Slot slot = slotArray[x, y];
                if (slot.block != null)
                {
                    //选出所有需要图块的槽点
                    List<Slot> list_open = new List<Slot>();
                    //判断周围所需块存在，否则跳过
                    //其实应该把判断过的相邻块去掉，不再遍历

                    //需要右边图块，且右方图块存在
                    if (slot.block.GetComponent<Block>().rightID != -1 && x < circleSize - 1)
                    {
                        //右边图块是所需图块则将其加入list，否则continue
                        if (slotArray[x + 1, y].blockID == slot.block.GetComponent<Block>().rightID)
                        {
                            list_open.Add(slotArray[x + 1, y]);
                        }
                        else
                            continue;
                    }

                    if (x > 0)
                    {
                        if (slot.block.GetComponent<Block>().leftID != -1)
                        {
                            list_open.Add(slotArray[x + 1, y]);

                        }
                    }

                    if (slot.y < circleSize &&
                        slot.block.GetComponent<Block>().upID != -1)
                    {

                    }
                }
            }
        }

        foreach (Slot slot in slots)
        {

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
