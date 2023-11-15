using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Tile Attributes")]
    [SerializeField] private int xLoc, yLoc;
    [SerializeField] private bool isPathable;
    [SerializeField] private GameObject tower;

    [Header("A* Attributes")]
    [SerializeField] private int gCost, hCost;

    [Header("References")]
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject TowerSelectorController;
    public void init(bool isOffset, int x, int y)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
        xLoc = x;
        yLoc = y;
        TowerSelectorController = GameObject.Find("TowerSelectorController");
    }

    public void setTower(GameObject tower)
    {
        Instantiate(tower, this.transform.position, Quaternion.identity);
        this.tower = tower;
        isPathable = false;
    }
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
        getTilePosition();
        bool hasTower = tower != null;
        TowerSelectorController.GetComponent<TowerSelectManager>().handleGridClicked(!hasTower);
    }

}
