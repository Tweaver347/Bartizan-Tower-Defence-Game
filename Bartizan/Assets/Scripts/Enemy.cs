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


    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //moveOnPath(path);
        
    }

    public void setup(List<Vector3> path)
    {
        this.path = path;
        moveOnPath(this.path);
    }

    private void moveOnPath(List<Vector3> path)
    {
        for(int i  = 0; i < path.Count; i++)
        {
            Debug.Log("Moving to: " + path[i]);
            Vector3 target = path[i];
            KinematicArrive(target); 
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
