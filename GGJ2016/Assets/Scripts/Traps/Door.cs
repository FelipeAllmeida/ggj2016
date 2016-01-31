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
        this.transform.localPosition = new Vector3(0f, -10f, 0f);
    }

    public void Reset()
    {
        this.transform.localPosition = transform.localPosition;
    }
}
