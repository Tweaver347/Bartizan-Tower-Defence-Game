using UnityEngine;

public class TempBatMovement : MonoBehaviour
{
    [SerializeField] private Transform goal;
    private float speed = 2f;
    private float rotationSpeed = 5f;

    private void Update()
    {
        Vector2 direction = goal.position - transform.position;
        float distance = direction.magnitude;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector2 velocity = direction.normalized * speed;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

}
