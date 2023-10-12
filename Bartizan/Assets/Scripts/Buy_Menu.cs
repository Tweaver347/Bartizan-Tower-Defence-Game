using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buy_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject buyMenuPrefab;
    private GameObject buyMenu;

    // Start is called before the first frame update
    void Start()
    {
        buyMenu = Instantiate(buyMenuPrefab);
        buyMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            openBuyMenu();
        }
    }

    private void openBuyMenu()
    {
        buyMenu.transform.position = grid
    }
}
