using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Grid_Manager grid;
    public static List<Vector3> path;


    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid_Manager(5, 3, 10f, new Vector3(20, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
