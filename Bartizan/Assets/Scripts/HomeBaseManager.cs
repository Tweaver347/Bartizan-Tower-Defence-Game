using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBaseManager : MonoBehaviour
{
    public Image healthBar;

    [SerializeField] private float startHealth;
    [SerializeField] private float currHealth;
    [SerializeField] private float fillAmt;

    private void Start()
    {
        startHealth = 100;
        currHealth = startHealth;
        fillAmt = 1;
    }
    // when an enemy hits the base the base takes damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            // get the health of the enemy and subtract it from the health of the HomeBase
            currHealth = currHealth - 10; // temp dmg
            Debug.Log("An Enemy has Attacked your home base! The Current health of your base is: " + currHealth);
            if (currHealth == 0)
            {
                Debug.Log("Your Base has been Destroyed!");
                // GameManager.GameOver();
            }

            if (fillAmt >= .8)
            {
                healthBar.color = Color.green;
            }
            else if (fillAmt >= .5)
            {
                healthBar.color = new Color32(0xFF, 0xA5, 0x00, 0xFF); // orange
            }
            else
            {
                healthBar.color = Color.red;
            }

            fillAmt = currHealth / startHealth;
            // change health bar color based on how much damage has been taken


            // update health bar of home base
            healthBar.fillAmount = (currHealth/startHealth);

            //Tell the Spawner that an Enemy has been destoryed
            EnemySpawnManager.onEnemyDestroy.Invoke();
 
            Destroy(collision.gameObject); // Destory the Enemy
        }
    }


}
