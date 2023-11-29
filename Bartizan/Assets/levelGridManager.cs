using gridCell;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class levelGridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private Camera mainCamera;

    private GameObject[,] grid;


    private void Start()
    {
        grid = new GameObject[width, height];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.transform.parent = this.transform;
                spawnedTile.name = "Tile (" + x + ", " + y + ")";
                grid[x, y] = spawnedTile;

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.GetComponent<Tile>().init(isOffset, x, y);
            }
        }
        mainCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        manageGrid(grid);
    }

    void manageGrid(GameObject[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Tile[] neighbors = new Tile[4];
                int index = 0;
                int numRows = grid.GetLength(0);
                int numCols = grid.GetLength(1);

                if (i > 0)  //check and add above
                {
                    if(grid[i - 1, j].GetComponent<Tile>().isEmpty())
                    {
                        neighbors[index] = (grid[i - 1, j].GetComponent<Tile>());
                        index++;
                    }
                    
                }

                if (j > 0)  //check and add left
                {
                    if(grid[i, j - 1].GetComponent<Tile>().isEmpty())
                    {
                        neighbors[index] = (grid[i, j - 1].GetComponent<Tile>());
                        index++;
                    }
                    
                }

                if (i < numRows - 1)    //check and add below
                {
                    if(grid[i + 1, j].GetComponent<Tile>().isEmpty())
                    {
                        neighbors[index] = (grid[i + 1, j].GetComponent<Tile>());
                        index++;
                    }
                    
                }

                if (j < numCols - 1)    //check and add right
                {
                    if(grid[i, j + 1].GetComponent<Tile>().isEmpty())
                    {
                        neighbors[index] = (grid[i, j + 1].GetComponent<Tile>());
                    }
                    
                }

                grid[i, j].GetComponent<Tile>().setNeighbours(neighbors);
            }
        }
    }

    public static Tile getClosestCell(List<Tile> frontier)
    {
        Tile closestCell = frontier[0];

        for (int i = 1; i < frontier.Count; i++)
        {
            if (closestCell.getDistTo() + closestCell.getDistFrom() > frontier[i].getDistTo() + frontier[i].getDistFrom())
            {
                closestCell = frontier[i];
            }
        }

        return closestCell;
    }

    public static int calculateH(Tile current, Tile goal)
    {
        int huristicX = (int)Mathf.Abs(current.GetComponent<Transform>().position.x - goal.GetComponent<Transform>().position.x);
        int huristicY = (int)Mathf.Abs(current.GetComponent<Transform>().position.y - goal.GetComponent<Transform>().position.y);
        int huristic = huristicX + huristicY;
        return huristic;
    }

    public List<Tile> A_Star(GameObject[,] grid, Tile start, Tile end)
    {
        Debug.Log("In find path");
        List<Tile> path = new List<Tile>();

        List<Tile> frontier = new List<Tile>();
        ArrayList visited = new ArrayList();

        frontier.Add(start);

        Tile startPoint = start;
        startPoint.setDistTo(0);

        Debug.Log("starting while loop");
        while (frontier.Count > 0)
        {
            Tile current = getClosestCell(frontier); //get the cell with lowest cost
            frontier.Remove(current);
            visited.Add(current);
            Tile[] neighbours = current.getNeighbours();
            
            Debug.Log("checking neighbours");
            for (int i = 0; i < neighbours.Length; i++)
            {
                Tile nextCell = neighbours[i];
                
                if (!visited.Contains(nextCell) && !frontier.Contains(nextCell))
                {
                    nextCell.setPrevious(current);
                    nextCell.setDistTo(current.getDistTo() + 1);
                    nextCell.setDistFrom(calculateH(current, end));

                    frontier.Add(nextCell);
                }
            }


            if (current == end)
            {
                Debug.Log("end found. Path:");

                while (current.getPrevious() != null)
                {
                    
                    path.Add(current.getPrevious());
                    current = current.getPrevious();
                }
                //path.Add(current.GetPosition());      //adding the spawn position. Not needed in the Path for the enemy.
                break;
            }
        }
        Debug.Log("path found. revercing it.");
        path.Reverse();


        Debug.Log("returning path: " + path);
        return path;
    }
}
