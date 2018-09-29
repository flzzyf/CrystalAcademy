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
            Texture2D[,] imageSlices = ImageSlicer.Slice(blockPrefabs[i].image,
                blockPrefabs[i].sliceCount.x, blockPrefabs[i].sliceCount.y);
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
        //复制一个槽点列表
        List<Slot> tempSlots = new List<Slot>(slots);
        List<Slot> list_open = new List<Slot>();

        while (tempSlots.Count > 0)
        {
            if (!tempSlots[0].blockExists)
            {
                tempSlots.Remove(tempSlots[0]);
                continue;
            }

            list_open.Add(tempSlots[0]);
            bool fail = false;

            while (list_open.Count > 0)
            {
                Slot slot = list_open[0];
                tempSlots.Remove(slot);
                list_open.Remove(slot);
                List<Slot> list = GetRequiredSlot(slot);
                if (list != null)
                {
                    foreach (Slot s in list)
                    {
                        if (tempSlots.Contains(s))
                        {
                            list_open.Add(s);
                        }
                    }
                }
                else
                {
                    fail = true;
                    break;
                }
            }

            if (!fail)
            {
                print("召唤成功");
            }
        }
    }

    List<Slot> GetRequiredSlot(Slot _slot)
    {
        List<Slot> list = new List<Slot>();
        //判定右侧
        if (_slot.x < circleSize - 1 && _slot.block.rightID != -1)
        {
            Slot s = slotArray[_slot.x + 1, _slot.y];
            if (s.blockExists && s.block.id == _slot.block.rightID)
            {
                list.Add(s);
            }
            else return null;
        }
        //判定左侧
        if (_slot.x > 0 && _slot.block.leftID != -1)
        {
            Slot s = slotArray[_slot.x - 1, _slot.y];
            if (s.blockExists && s.block.id == _slot.block.leftID)
            {
                list.Add(s);
            }
            else return null;
        }
        //判定下方
        if (_slot.y < circleSize - 1 && _slot.block.downID != -1)
        {
            Slot s = slotArray[_slot.x, _slot.y + 1];
            if (s.blockExists && s.block.id == _slot.block.downID)
            {
                list.Add(s);
            }
            else return null;
        }
        //判定上方
        if (_slot.y > 0 && _slot.block.upID != -1)
        {
            Slot s = slotArray[_slot.x, _slot.y - 1];
            if (s.blockExists && s.block.id == _slot.block.upID)
            {
                list.Add(s);
            }
            else return null;
        }

        return list;
    }
}

[System.Serializable]
public class BlockPrefab
{
    public Texture2D image;
    public Vector2Int sliceCount = new Vector2Int(1, 1);
    public GameObject prefab_unit;

}
