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
    [SerializeField] private GameObject trashcan;
    private int currentWayPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        trashcan = GameObject.Find("TrashCan");
        myTransform = this.gameObject.transform;
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
        //add 1 gold to the total gold count
    }


}
