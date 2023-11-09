using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gridCell;
using CodeMonkey.Utils;
using System;

public class Game_Manager : MonoBehaviour
{
    
    //public Grid_Manager gridManager;
    public Grid_Cell[,] grid;
    public static List<Vector3> path;
    public Grid_Cell spawn;
    public Grid_Cell home;

    public GameObject enemy;
    private GameObject spawnedEnemy;


    // Start is called before the first frame update
    void Start()
    {

        grid = buildGrid(8, 5, 5f, new Vector3(15, 5));
        spawn = grid[6, 4];
        home = grid[1, 0];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Grid_Cell[,] buildGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        Grid_Cell[,] gridArray = new Grid_Cell[width, height];
        TextMesh[,] debugTestArray = new TextMesh[width, height];
        

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                //adding text value to each cell
                //debugTestArray[i, j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, new Vector3(i, j) * cellSize + originPosition + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                gridArray[i, j] = new Grid_Cell(i, j, 0, new Vector3(i, j) * cellSize + originPosition + new Vector3(cellSize, cellSize) * 0.5f, (i * 10) + j);

                
                //generating grid lines to make it easier to visulise the gird
                Debug.DrawLine(new Vector3(i, j) * cellSize + originPosition, new Vector3(i, j + 1) * cellSize + originPosition, Color.white, 100f);
                Debug.DrawLine(new Vector3(i, j) * cellSize + originPosition, new Vector3(i + 1, j) * cellSize + originPosition, Color.white, 100f);

            }
        }

        //finding neighbours
        for(int i = 0;i < gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridArray.GetLength(1);j++)
            {
                List<Grid_Cell> neighbors = new List<Grid_Cell>();

                int numRows = gridArray.GetLength(0);
                int numCols = gridArray.GetLength(1);

                if (i > 0)  //check and add above
                {
                    neighbors.Add(gridArray[i - 1, j]);
                }

                if (j > 0)  //check and add left
                {
                    neighbors.Add(gridArray[i, j - 1]);
                }

                if (i < numRows - 1)    //check and add below
                {
                    neighbors.Add(gridArray[i + 1, j]);
                }

                if (j < numCols - 1)    //check and add right
                {
                    neighbors.Add(gridArray[i, j + 1]);
                }

                foreach (Grid_Cell neighbor in neighbors)
                {
                    if (neighbor.GetValue() != 0) { neighbors.Remove(neighbor); }   //value 0 means the grid cell is empty and can be moved on
                }

                gridArray[i, j].SetNeighbours(neighbors.ToArray());
            }
        }

        //adding horizontal and vertical lines to the grid to make it look like a proper grid
        Debug.DrawLine(new Vector3(0, height) * cellSize + originPosition, new Vector3(width, height) * cellSize + originPosition, Color.white, 100f);
        Debug.DrawLine(new Vector3(width, 0) * cellSize + originPosition, new Vector3(width, height) * cellSize + originPosition, Color.white, 100f);

        return gridArray;
    }


    public void getPath()
    {
        Debug.Log("calling find path in A*");
        path = A_Star.findPath(grid, spawn, home);
        if(path == null ) { Debug.Log("Null path"); }
        spawnedEnemy = Instantiate(enemy);
        spawnedEnemy.transform.position = spawn.GetPosition();
        Debug.Log("drawing path");
        Vector3 start = path[0];
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 end = path[i];
            if(start != null && end != null)
            {
                Debug.DrawLine(start, end, Color.red, 100f);
            }
            start = path[i];
        }
        Debug.Log("line drawn");
        spawnedEnemy.GetComponent<Enemy>().setup(path);




    }
}
