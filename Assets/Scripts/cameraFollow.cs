using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    // what we want to follow
    [SerializeField] private Transform target;
    // speed to follow 0-1
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float zOffset = -5;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;

    private void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, transform.position.y, zOffset);
        Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, zOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Limit the Cameras transform 
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }

    // add gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit)); //top
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit)); //bottom
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit)); //left
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit)); //right   
    }
}
