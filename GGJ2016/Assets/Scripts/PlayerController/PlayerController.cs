using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Public Data
    #endregion

    #region Private Data

    [SerializeField] private float _moveSpeed = 0.25f;

    #endregion

    void Update()
    {
        DetectInputs();
    }

    void DetectInputs()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0f, _moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0f, 0f, -_moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-_moveSpeed, 0f, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(_moveSpeed, 0f, 0);
        }
    }
}
