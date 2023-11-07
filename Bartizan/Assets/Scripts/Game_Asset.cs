using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Asset : MonoBehaviour
{
    private static Game_Asset instance;

    public static Game_Asset Instance
    {
        get
        {
            if(instance == null) instance = (Instantiate(Resources.Load("GameAsset")) as GameObject).GetComponent<Game_Asset>();
            return instance;
        }
    }
}
