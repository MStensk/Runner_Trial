using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform target;

    public Vector3 offset = new Vector3(0f, 5f, -8f);
    public float positionSmoothSpeed = 8f;
    public float rotationSmoothSpeed = 8f;
    void LateUpdate()
    {
         if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            positionSmoothSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }
}
