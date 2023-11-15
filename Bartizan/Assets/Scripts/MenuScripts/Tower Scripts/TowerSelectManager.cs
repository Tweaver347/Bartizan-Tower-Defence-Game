using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TowerSelectManager : MonoBehaviour
{


    [SerializeField] private GameObject buyMenuPanel;
    [SerializeField] private GameObject upgradeMenuPanel;

    private bool isBuyOpen;
    private bool isUpgradeOpen;

    private Animator buyMenuAnim;
    private Animator upgradeMenuAnim;

    private void Awake()
    {
        buyMenuAnim = buyMenuPanel.GetComponent<Animator>();
        upgradeMenuAnim = upgradeMenuPanel.GetComponent<Animator>();

        isBuyOpen = false;
        isUpgradeOpen = false;


    }

    /* 
     * if the player clicks on an EMPTY towercontainer while the BUY MENU is In the BUY MENU slides out
     * if the player clicks on an EMPTY towercontainer while the BUY MENU is OUT the BUY MENU does nothing
     * if the player clicks on a FULL towercontainer while the UPGRADE MENU is IN the UPGRADE MENU slides out
     * if the player clicks ona  FULL towercontainer while the UPGRADE MENU is OUT the UPGRADE MENU does nothing
     */

    public void handleGridClicked(bool tower)
    {
        if (!tower)
        {
            if (isBuyOpen)
            {
                CloseBuyMenu();
            }
            else
            {
                OpenBuyMenu();
            }
        }
        else
        {
            if (isUpgradeOpen)
            {
                CloseUpgradeMenu();
            }
            else
            {
                OpenUpgradeMenu();

            }
        }
    }

    public void OpenBuyMenu()
    {
        isBuyOpen = true;
        // if the upgrade menu is out, close it
        if (isUpgradeOpen)
        {
            CloseUpgradeMenu();
        }
        //play opening animation
        buyMenuAnim.Play("OpenMenu");


    }

    public void CloseBuyMenu()
    {
        // play closing animation
        isBuyOpen = false;
        buyMenuAnim.Play("CloseMenu");


    }

    public void OpenUpgradeMenu()
    {
        isUpgradeOpen = true;
        // if the buy menu is out, close it
        if (isBuyOpen)
        {
            CloseBuyMenu();
        }
        // play opening animation

        upgradeMenuAnim.Play("OpenMenu");


    }

    public void CloseUpgradeMenu()
    {
        // play closing animation
        isUpgradeOpen = false;
        upgradeMenuAnim.Play("CloseMenu");


    }


}
