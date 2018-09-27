using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PuzzleManager.instance.Init();
        }
    }
}
