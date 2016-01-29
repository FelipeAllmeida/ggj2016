using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

    private float disappearTime;
    private float lightOffTime;
    public Light pLight;

    // Use this for initialization
    void Start()
    {
        disappearTime = Time.time + 60f;
        lightOffTime = Time.time + 0.10f;

        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            body.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0.25f, 0.75f), Random.Range(-1f, 1f)) * Random.Range(10f, 50f), ForceMode.Impulse);
            body.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(0f, 50f));
        }

        PerlinShake ps = FindObjectOfType<PerlinShake>();
        if (ps)
        {
            ps.PlayShake();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (pLight && Time.time > lightOffTime)
        {
            Destroy(pLight);
        }

        if (Time.time > disappearTime)
        {
            Destroy(gameObject);
        }
    }
}
