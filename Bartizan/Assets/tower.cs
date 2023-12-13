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
    [SerializeField] private int damage_Amount;

    private float targetingRange = 3f;

    [SerializeField] private GameObject target;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTower();
        attack();

    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
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

    // draw gizmos to show the range of the tower  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
    private void attack()
    {
        target.GetComponent<Enemy>().takeDamage(damage_Amount);
    }
}

