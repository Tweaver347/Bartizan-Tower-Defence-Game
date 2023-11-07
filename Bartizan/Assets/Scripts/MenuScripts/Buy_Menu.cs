using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buy_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject buyMenuPrefab;
    private GameObject buyMenu;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        buyMenu = Instantiate(buyMenuPrefab);
        buyMenu.SetActive(isActive);

    }

    // Update is called once per frame
    void Update()
    {
        followMouse();
        if(Input.GetMouseButtonDown(0))
        {
            openBuyMenu();
        }
    }

    private void openBuyMenu()
    {
        isActive = !isActive;
        buyMenu.SetActive(isActive);
        buyMenu.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void followMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        buyMenu.transform.position = mousePos;

    }
}
