using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Tile Attributes")]
    [SerializeField] private int xLoc, yLoc;
    [SerializeField] private bool isPathable = true;
    [SerializeField] private GameObject tower;
    public GameObject spawnedTower;

    [Header("A* Attributes")]
    [SerializeField] private int distTo, distFrom;
    private levelGridManager levelGridManager;
    private Tile[] neighbours;
    private Tile previous;

    [Header("References")]
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject Tower;
    [SerializeField] private GameObject GameManager;
    public void init(bool isOffset, int x, int y, bool pathable)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
        xLoc = x;
        yLoc = y;
        isPathable = pathable;
        GameManager = GameObject.Find("GameManager");
        levelGridManager = GameObject.FindGameObjectWithTag("Grid Printer").GetComponent<levelGridManager>();
    }

    public void setTower(GameObject tower)
    {
        spawnedTower = Instantiate(tower, this.transform.position, Quaternion.identity);
        isPathable = false;
        levelGridManager.updatePath();
    }

    public void setPathable(bool pathable) { isPathable = pathable; }

    public void setPrevious(Tile previous) { this.previous = previous; }

    public Tile getPrevious() { return previous; }

    public void setNeighbours(Tile[] neighbours) { this.neighbours = neighbours; }

    public Tile[] getNeighbours() { return this.neighbours; }

    public void setDistTo(int dist) { distTo = dist; }

    public int getDistTo() { return distTo; }

    public void setDistFrom(int dist) { distFrom = dist; }

    public int getDistFrom() { return distFrom; }

    public bool isEmpty() { return isPathable; }
    public void getTilePosition()
    {
        Debug.Log("Tile position is: (" + xLoc + ", " + yLoc + ")");
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        int gold = GameManager.GetComponent<GameManager>().getGold();
        // if left mouse button is clicled then buy tower
        if (Input.GetMouseButtonDown(0))
        {
            if (gold >= 100 && isPathable)
            {
                setTower(Tower);
                GameManager.GetComponent<GameManager>().setGold(gold - 100);

                // Update the grid
            }
            else
            {
                Debug.Log("Not enough gold to buy tower");
            }
        }
        // if right mouse button is clicked then sell tower
        if (Input.GetMouseButtonDown(1))
        {
            if (spawnedTower != null)
            {
                Destroy(spawnedTower);
                isPathable = true;
                GameManager.GetComponent<GameManager>().setGold(gold + 75);

                // Update the grid
            }
            else
            {
                Debug.Log("No tower to sell");
            }
        }
    }

}
