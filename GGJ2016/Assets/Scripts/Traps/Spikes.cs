using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{

    public Transform spikes;
    public bool isActive;
    private bool originalValue;

    public void Toggle()
    {
        isActive = !isActive;
        SetSpikes();
    }

    private void SetSpikes()
    {
        if (isActive)
        {
            spikes.localPosition = Vector3.zero;
        }
        else
        {
            spikes.localPosition = new Vector3(0, -10, 0);
        }
    }

    public void Reset()
    {
        isActive = originalValue;
        SetSpikes();
    }

    void Start()
    {
        originalValue = isActive;
        SetSpikes();
    }
}

