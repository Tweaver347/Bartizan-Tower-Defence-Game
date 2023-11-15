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
                //grid[x, y] = spawnedTile;

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.GetComponent<Tile>().init(isOffset, x, y);
            }
        }
        mainCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
