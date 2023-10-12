using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Security.Principal;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;

public class Grid_Manager : MonoBehaviour 
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTestArray;



    //grid manager constructor used to setup the grid at the beginning of each level
    public Grid_Manager(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        

        gridArray = new int[width, height];
        debugTestArray = new TextMesh[width, height];
        

        for(int i = 0; i < gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridArray.GetLength(1); j++)
            {
                //adding text value to each cell
                debugTestArray[i, j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    
                //generating grid lines to make it easier to visulise the gird
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
                
            }
        }

        //adding horizontal and vertical lines to the grid to make it look like a proper grid
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);


    }

    //gets the position of the grid cell in the game world
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //gets the x and y values of a grid cell from the world position
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        
    }

   
    /// <summary>
    /// sets value for specific grid cell
    /// to be used to set values so that the game manager can
    /// identify which cells can be walked on by enemy and which
    /// cells are valid to place towers on
    /// for example: value = 0 means empty grid cell
    /// value = 1 means not empty grid cell
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetValue(int x, int y, int value)
    {
        //***TODO: add conditional statement to check for valid and invalid values
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTestArray[x, y].text = value.ToString();
        }
        
    }

    //Sets a specific value to a certain cell using world position
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    //returns the value of the grid cell
    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x <= width && y <= height)
        {
            return gridArray[x, y];
        }
        else
        {
            //x and y values are invalid hence it return -1
            //just an option to return a certain value if x and y values are invalid
            return -1;
        }
    }

    //returns x and y values of grid from world position
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
