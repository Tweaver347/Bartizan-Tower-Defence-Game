using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using Unity.Mathematics;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform TowerRotationPoint;
    [SerializeField] private LayerMask enemyLayer;

    private float targetingRange = 2f;

    [SerializeField] private GameObject target;
    [SerializeField] private int damage_Amount = 1;

    private float fireRate = 0.5f;
    private float fireCountdown = 0f;

    // attack sound effect
    //[SerializeField] private GameObject attackSoundGO;
    //private AudioSource attackSound;

    private void Start()
    {
        //attackSound = attackSoundGO.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            RotateTower();
            if (fireCountdown < 0f)
            {
                attack();
                fireCountdown = 1f / fireRate;
                //Debug.Log("fireCountdown: " + fireCountdown);

            }
            fireCountdown -= Time.deltaTime;
            //Debug.Log("fireCountdown: " + fireCountdown);

        }

    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyLayer);

        if (hits.Length > 0)
        {
            Collider2D hitCollider = hits[0].collider;

            if (hitCollider != null)
            {
                target = hitCollider.gameObject;
            }
        }
    }

    private void RotateTower()
    {
        float angle = Mathf.Atan2(target.transform.position.y - TowerRotationPoint.position.y, target.transform.position.x - TowerRotationPoint.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        TowerRotationPoint.rotation = Quaternion.Slerp(TowerRotationPoint.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void attack()
    {
        //Debug.Log("Attacking");
        //attackSound.Play();
        target.GetComponent<Enemy>().takeDamage(damage_Amount);
    }

    // draw gizmos to show the range of the tower  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}

