using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGridManager : MonoBehaviour
{
    // Grid dimensions
    [Header("Grid Dimensions")]
    [SerializeField] private int width, height;

    // Prefabs
    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject homePrefab;

    // Camera
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    // Grid
    [Header("Grid")]
    private GameObject[,] grid;

    // Start and end tiles
    [Header("Start and End Tiles")]
    [SerializeField] private int startX, startY, endX, endY; // serialized int x and y for start and end tiles
    [SerializeField] private Tile start;
    [SerializeField] private Tile end;

    // Home base
    [Header("Home Base")]
    public GameObject homeBase;

    // Obstacles
    [Header("Obstacles")]
    [SerializeField] Vector2[] obstacles;

    // Enemy spawn manager
    [Header("Enemy Spawn Manager")]
    [SerializeField] private GameObject ESM;
    private EnemySpawnManager enemySpawnManager;



    private void Start()
    {
        // instantiate and populate the grid
        grid = new GameObject[width, height];
        GenerateGrid();

        // set obstacles
        //manageGrid(grid); // redo nehighbours
        foreach (Vector2 obstacle in obstacles)
        {
            Debug.Log("Obstacle: " + obstacle);
            // clamp values of list
            int x = (int)obstacle.x;
            int y = (int)obstacle.y;
            // set tile to false
            grid[x, y].GetComponent<Tile>().setPathable(false);
            // change color of tile to black
            grid[x, y].GetComponent<SpriteRenderer>().color = Color.black;
        }
        manageGrid(grid); // redo nehighbours
        // Clamp Start ints and End ints values within the grid
        Debug.Log("Clamping Start and End values");
        startX = Mathf.Clamp(startX, 0, width - 1);
        startY = Mathf.Clamp(startY, 0, height - 1);

        endX = Mathf.Clamp(endX, 0, width - 1);
        endY = Mathf.Clamp(endY, 0, height - 1);

        if (startX == endX && startY == endY)
        {
            Debug.Log("Start and End are the same. Changing End");
            endX = Mathf.Clamp(endX + 1, 0, width - 1);
            endY = Mathf.Clamp(endY + 1, 0, height - 1);
        }

        // find the start and end tiles
        start = grid[startX, startY].GetComponent<Tile>();
        end = grid[endX, endY].GetComponent<Tile>();

        enemySpawnManager = ESM.GetComponent<EnemySpawnManager>();
        // find the path from start to end

        //instantiate homebase at end
        homeBase = Instantiate(homePrefab, end.transform.position, Quaternion.identity);
        List<Tile> path = A_Star(grid, start, end);

        List<Vector3> enemy_Path = getPath_Vector3(path);

        // provide path to enemy spawner
        enemySpawnManager.setPath(enemy_Path, grid[0, 0]);


    }

    /// <summary>
    /// Generates the grid with dimensions width and height
    /// </summary>
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // instantiate a tile at position (x, y)
                GameObject spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                // organize tiles as a child of the grid manager
                spawnedTile.transform.parent = this.transform;
                // name the tile
                spawnedTile.name = "Tile (" + x + ", " + y + ")";
                // add created tile to the grid
                grid[x, y] = spawnedTile;
                // initialize the tile
                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0); // for offset coloring
                spawnedTile.GetComponent<Tile>().init(isOffset, x, y, true);
            }
        }
        // set the camera position to the center of the grid
        mainCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        manageGrid(grid);
    }

    /// <summary>
    /// Adds neighbors to each tile in the grid
    /// </summary>
    /// <param name="grid"></param>
    public void manageGrid(GameObject[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                List<Tile> neighbors = new List<Tile>();
                int numRows = grid.GetLength(0);
                int numCols = grid.GetLength(1);

                if (i > 0)  //check and add above
                {
                    if (grid[i - 1, j].GetComponent<Tile>().isEmpty())
                    {
                        Debug.Log("adding top neighbour");
                        neighbors.Add(grid[i - 1, j].GetComponent<Tile>());
                    }

                }

                if (j > 0)  //check and add left
                {
                    if (grid[i, j - 1].GetComponent<Tile>().isEmpty())
                    {
                        Debug.Log("adding left neighbour");
                        neighbors.Add(grid[i, j - 1].GetComponent<Tile>());
                    }

                }

                if (i < numRows - 1)    //check and add below
                {
                    if (grid[i + 1, j].GetComponent<Tile>().isEmpty())
                    {
                        Debug.Log("adding bellow neighbour");
                        neighbors.Add(grid[i + 1, j].GetComponent<Tile>());
                    }

                }

                if (j < numCols - 1)    //check and add right
                {
                    if (grid[i, j + 1].GetComponent<Tile>().isEmpty())
                    {
                        Debug.Log("adding right neighbour");
                        neighbors.Add(grid[i, j + 1].GetComponent<Tile>());
                    }

                }

                grid[i, j].GetComponent<Tile>().setNeighbours(neighbors.ToArray());
            }
        }
    }

    /// <summary>
    /// Returns the cell with the lowest cost
    /// </summary>
    /// <param name="frontier"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Calculates the huristic value of a cell
    /// </summary>
    /// <param name="current"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public static int calculateH(Tile current, Tile goal)
    {
        int huristicX = (int)Mathf.Abs(current.GetComponent<Transform>().position.x - goal.GetComponent<Transform>().position.x);
        int huristicY = (int)Mathf.Abs(current.GetComponent<Transform>().position.y - goal.GetComponent<Transform>().position.y);
        int huristic = huristicX + huristicY;
        return huristic;
    }

    /// <summary>
    /// Finds the path from start to end
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
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
            Tile[] neighbours = current.getNeighbours();

            Debug.Log("checking neighbours");
            for (int i = 0; i < neighbours.Length; i++)
            {
                Tile nextCell = neighbours[i];

                if (!visited.Contains(nextCell) && !frontier.Contains(nextCell))
                {
                    Debug.Log("setting prev");
                    nextCell.setPrevious(current);
                    nextCell.setDistTo(current.getDistTo() + 1);
                    nextCell.setDistFrom(calculateH(current, end));

                    frontier.Add(nextCell);
                }
            }
            frontier.Remove(current);
            visited.Add(current);

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


        //Debug.Log("returning path: " + path);
        Debug.Log("path length:" + path.Count);

        // for each tile in path change the tile color to red
        foreach (Tile tile in path)
        {
            Debug.Log("in for loop");
            tile.GetComponent<SpriteRenderer>().color = Color.red;
        }
        // change the start tile color to blue
        start.GetComponent<SpriteRenderer>().color = Color.blue;
        // change the end tile color to blue
        end.GetComponent<SpriteRenderer>().color = Color.blue;

        return path;
    }

    private List<Vector3> getPath_Vector3(List<Tile> path)
    {
        List<Vector3> enemy_path = new List<Vector3>();
        Vector3 tilePosition = Vector3.zero;
        foreach (Tile tile in path)
        {
            tilePosition = tile.GetComponent<Transform>().position;
            enemy_path.Add(tilePosition);
        }

        return enemy_path;
    }
}
