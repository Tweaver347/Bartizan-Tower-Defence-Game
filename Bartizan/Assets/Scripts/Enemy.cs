using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform myTransform;
    private List<Vector3> path;
    private float targetRadius = 0.5f;
    private float moveSpeed = 0.1f;

    private int currentWayPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //moveOnPath(path);
        if(path != null && currentWayPointIndex < path.Count)
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
        //moveOnPath(this.path);
    }

    private void move()
    {
        Vector3 targetPosition = path[currentWayPointIndex];
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < targetRadius)
        {
            currentWayPointIndex++;

            if(currentWayPointIndex >= path.Count)
            {
                currentWayPointIndex = 0;
            }
        }

    }

    private void KinematicArrive(Vector3 targetPosition)
    {
        Vector3 towardsTarget = targetPosition - myTransform.position;
        Debug.Log("Check RoS");
        if (towardsTarget.magnitude <= targetRadius)
        {
            Debug.Log("Reached. Return. Next target now.");
            return;
        }

        towardsTarget = towardsTarget.normalized;

        //Quaternion targetRotation = Quaternion.LookRotation(towardsTarget, targetPosition);
        //myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRotation, 0.1f);

        Vector3 newPosition = myTransform.position;
        newPosition += myTransform.forward * moveSpeed * Time.deltaTime;

        Debug.Log("Changing transform position");
        myTransform.position = newPosition;

    }
}
