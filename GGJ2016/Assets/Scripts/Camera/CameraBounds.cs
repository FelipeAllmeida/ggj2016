using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour
{
    public Transform playerTransform;
    private Transform cTransform;
    private Vector3 target;
    private const float zOffset = -6;
    public bool moveCamera = true;
    public Vector2 topLeft;
    public Vector2 bottomRight;

    // Use this for initialization
    void Start()
    {
        cTransform = transform;
        target.y = cTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform || !moveCamera)
            return;

        target.x = playerTransform.position.x;
        target.z = playerTransform.position.z + zOffset;

        target.x = Mathf.Clamp(target.x, topLeft.x, bottomRight.x);
        target.z = Mathf.Clamp(target.z, topLeft.y, bottomRight.y);

        cTransform.position = target;
    }
}

