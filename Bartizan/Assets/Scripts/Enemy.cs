using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform myTransform;
    private List<Vector3> path;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
