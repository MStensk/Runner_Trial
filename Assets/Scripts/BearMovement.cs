using UnityEngine;

public class BearMovement : MonoBehaviour
{
    [SerializeField] private float laneLength = 12f;   // længden den kan bevæge sig
    [SerializeField] private float speed = 4f;         // move speed

    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float halfLane = laneLength / 2f;

        // bevæger sig mellem -halfLane og +halfLane relativt til start pos
        float xOffset = Mathf.PingPong(Time.time * speed, laneLength) - halfLane;

        transform.position = new Vector3(
            startPos.x + xOffset,
            startPos.y,
            startPos.z
        );

        // Detect direction skift, gør brug af float da movement sjældent lander på præcise tal
        if (xOffset >= halfLane - 0.01f)
            {
                FaceLeft();
            }

            if (xOffset <= -halfLane + 0.01f)
            {
                FaceRight();
            }
    }

    void FaceRight()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void FaceLeft()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
