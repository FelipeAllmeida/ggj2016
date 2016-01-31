using UnityEngine;
using System.Collections;

public class RotateAroundSelf : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 2f, 0));
    }
}
