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
    private Grid_Cell[,] gridArray;
    private TextMesh[,] debugTestArray;
    private int distTo;
    private int distFrom;



    //grid manager constructor used to setup the grid at the beginning of each level
    public Grid_Manager(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        

        gridArray = new Grid_Cell[width, height];
        debugTestArray = new TextMesh[width, height];
        

        for(int i = 0; i < gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridArray.GetLength(1); j++)
            {
                //adding text value to each cell
                debugTestArray[i, j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                gridArray[i, j] = new Grid_Cell(i, j, 0, GetWorldPosition(i, j));
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
    public Vector3 GetWorldPosition(int x, int y)
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
            gridArray[x, y].SetValue(value);
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
            return gridArray[x, y].GetValue();
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

    public Grid_Cell[] getNeighbors(Grid_Cell current)
    {
        int x, y;
        List<Grid_Cell> neighbors = new List<Grid_Cell>();
        current.getXY(out x, out y);
        
        int numRows = gridArray.GetLength(0);
        int numCols = gridArray.GetLength(1);

        if (x > 0)  //check and add above
        {
            neighbors.Add(gridArray[x - 1, y]);
        }

        if (y > 0)  //check and add left
        {
            neighbors.Add(gridArray[x, y - 1]);
        }
        
        if (x < numRows - 1)    //check and add below
        {
            neighbors.Add(gridArray[x + 1, y]);
        }

        if (y < numCols - 1)    //check and add right
        {
            neighbors.Add(gridArray[x, y + 1]);
        }

        return neighbors.ToArray();
    }

    
}

public class Grid_Cell
{
    int x, y;
    int value;
    int distTo;
    int distFrom;
    Grid_Cell previous;
    Vector3 position;
    public Grid_Cell(int x, int y, int value, Vector3 position)
    {
        this.x = x;
        this.y = y;
        this.value = value;
        this.position = position;
    }

    public void SetValue(int value) {  this.value = value;  }

    public int GetValue() {  return this.value;  }

    public void SetPrevious(Grid_Cell previous) {  this.previous = previous;  }

    public Grid_Cell GetPrevious() {  return this.previous;  }

    public void SetDistTo(int distTo) {  this.distTo = distTo;  }

    public int GetDistTo() {  return this.distTo;  }
    public void SetDistFrom(int distFrom) {  this.distFrom = distFrom;  }

    public int GetDistFrom() { return this.distFrom; }

    public Vector3 GetPosition() { return this.position; }

    public void getXY(out int x, out int y)
    {
        x = this.x;
        y = this.y;
    }


}