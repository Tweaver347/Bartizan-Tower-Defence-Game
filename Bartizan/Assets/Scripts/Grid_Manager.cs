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
using gridCell;

public class Grid_Manager : MonoBehaviour
{
    /*private static Grid_Manager instance;

    public static Grid_Manager Instance
    {
        get
        {
            if (instance == null) { instance = (Instantiate(Resources.Load("Grid Manager")) as GameObject).GetComponent<Grid_Manager>(); }
            return instance;
        }
    }*/

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private Grid_Cell[,] gridArray;
    private TextMesh[,] debugTestArray;


    //grid manager constructor used to setup the grid at the beginning of each level
    public Grid_Cell[,] BuildGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;


        gridArray = new Grid_Cell[width, height];
        debugTestArray = new TextMesh[width, height];


        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                //adding text value to each cell
                debugTestArray[i, j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                gridArray[i, j] = new Grid_Cell(i, j, 0, GetWorldPosition(i, j), (i * 10) + j);
                //generating grid lines to make it easier to visulise the gird
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);

            }
        }

        //adding horizontal and vertical lines to the grid to make it look like a proper grid
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        return gridArray;
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

        //get grid cell from XY value
        public Grid_Cell GetCell(int x, int y)
        {
            return gridArray[x, y];
        }

        //get grid cell from world position
        public Grid_Cell GetCell(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return gridArray[x, y];
        }

        //returns the value of the grid cell using XY
        public int GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x <= width && y <= height)
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

        //returns an array of all the valid cells the enemy can move on
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

            foreach (Grid_Cell neighbor in neighbors)
            {
                if (neighbor.GetValue() != 0) { neighbors.Remove(neighbor); }   //value 0 means the grid cell is empty and can be moved on
            }
            return neighbors.ToArray();
        }


    }

namespace gridCell
{
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

    public class Grid_Cell
    {
        int x, y;
        int value = 0;
        int number;
        Vector3 position;
        int distTo;
        int distFrom;
        Grid_Cell previous;
        Grid_Cell[] neighbours;
        public Grid_Cell(int x, int y, int value, Vector3 position, int number)
        {
            this.x = x;
            this.y = y;
            this.value = value;
            this.position = position;
            this.number = number;
        }

        public int GetNumber() { return number; }

        public void SetNeighbours(Grid_Cell[] neighbours) { this.neighbours = neighbours; }

        public Grid_Cell[] GetNeighbours() { return this.neighbours; }

        public void SetValue(int value) { this.value = value; }

        public int GetValue() { return this.value; }

        public void SetPrevious(Grid_Cell previous) { this.previous = previous; }

        public Grid_Cell GetPrevious() { return this.previous; }

        public void SetDistTo(int distTo) { this.distTo = distTo; }

        public int GetDistTo() { return this.distTo; }
        public void SetDistFrom(int distFrom) { this.distFrom = distFrom; }

        public int GetDistFrom() { return this.distFrom; }

        public Vector3 GetPosition() { return this.position; }

        public void getXY(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }


    }
}