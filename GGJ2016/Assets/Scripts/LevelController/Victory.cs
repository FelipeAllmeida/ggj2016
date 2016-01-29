using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour
{

    private float lightOffTime;
    public Light pLight;

    // Use this for initialization
    void Start()
    {
        lightOffTime = Time.time + 0.10f;

        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            body.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f)) * 30f, ForceMode.Impulse);
            body.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 100f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pLight && Time.time > lightOffTime)
        {
            Destroy(pLight);
        }
    }
}
