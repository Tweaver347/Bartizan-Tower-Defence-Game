using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    private Transform myTransform;
    private List<Vector3> path;
    private float targetRadius = 0.5f;
    [SerializeField] private float moveSpeed = 10f;
    private int currentWayPointIndex = 0;

    [SerializeField] GameObject GM;
    private GameManager gameManager;

    [SerializeField] private int gold_Amount;
    [SerializeField] private int health = 10;


    // Variables for the Health Bar
    public Image healthBar;
    [SerializeField] private float startHealth;
    [SerializeField] private float currHealth;
    [SerializeField] private float fillAmt;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
        GM = GameObject.Find("GameManager");
        gameManager = GM.GetComponent<GameManager>();

        startHealth = health;
        currHealth = startHealth;
        fillAmt = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null && currentWayPointIndex < path.Count)
        {
            move();
        }

    }

    public void setup(List<Vector3> path)
    {
        if (path == null)
        {
            Debug.LogError("Path is null. Make sure to call setup() before moveOnPath().");
            return;
        }
        this.path = path;
    }

    private void move()
    {
        Vector3 targetPosition = path[currentWayPointIndex];
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        if (Vector3.Distance(transform.position, targetPosition) < targetRadius)
        {
            // follow path
            currentWayPointIndex++;
        }

    }

    private void dead()
    {
        Debug.Log("Enemy Dead adding gold" + gold_Amount);
        gameManager.setGold(gold_Amount + gameManager.getGold());
        EnemySpawnManager.onEnemyDestroy.Invoke();
        Destroy(gameObject);

    }

    public void takeDamage(int amount)
    {
        health = health - amount;
        currHealth = currHealth - amount;


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
        healthBar.fillAmount = (currHealth / startHealth);


        if (health <= 0)
        {
            dead();
        }
    }


}
