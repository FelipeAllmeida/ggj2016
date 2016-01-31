using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;
    }

	public void Open()
    {
        this.transform.position = new Vector3(0f, -20f, 0f);
    }

    public void Reset()
    {
        this.transform.position = transform.localPosition;
    }
}
