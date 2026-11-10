using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float checkDistance = 0.1f;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent <Rigidbody>();

        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPosition) <= checkDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
