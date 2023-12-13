using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
        gameManager = GM.GetComponent<GameManager>();
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
        //gameManager.addGold(gold_Amount);
        Destroy(gameObject);

    }

    public void takeDamage(int amount)
    {
        health = health - amount;

        if (health <= 0)
        {
            dead();
        }
    }


}
