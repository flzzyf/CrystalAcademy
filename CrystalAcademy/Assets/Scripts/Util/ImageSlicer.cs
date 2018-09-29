using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSlicer
{
    public static Texture2D[,] Slice(Texture2D _texture, int _sizeX, int _sizeY)
    {
        int imageSize = Mathf.Min(_texture.width, _texture.height);
        int blockSize = imageSize / Mathf.Min(_sizeX, _sizeY);

        Texture2D[,] blocks = new Texture2D[_sizeX, _sizeY];

        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                Texture2D block = new Texture2D(blockSize, blockSize);

                block.wrapMode = TextureWrapMode.Clamp;
                block.filterMode = FilterMode.Point;
                block.SetPixels(_texture.GetPixels(x * blockSize, y * blockSize, blockSize, blockSize));
                block.Apply();
                blocks[x, y] = block;
            }
        }

        return blocks;
    }
}
