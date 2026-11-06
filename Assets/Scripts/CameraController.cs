using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followThisGameObject;
    public Vector3 offset;

    void LateUpdate()
    {
        if (followThisGameObject == null) return;
        var target = followThisGameObject.transform;

        // The camera is placed at the position of the ball + the offset
        transform.position = target.position + offset;
    }
}