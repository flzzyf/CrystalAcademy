using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSlicer
{
    public static Texture2D[,] Slice(Texture2D _texture, int _size)
    {
        int imageSize = Mathf.Min(_texture.width, _texture.height);
        int blockSize = imageSize / _size;

        Texture2D[,] blocks = new Texture2D[_size, _size];

        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
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
