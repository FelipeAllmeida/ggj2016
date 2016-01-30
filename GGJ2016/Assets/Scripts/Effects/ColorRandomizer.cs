using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ColorRandomizer : MonoBehaviour
{
    public Color[] colors;
    public Light lights;

    void Start()
    {
        int __value = Random.Range(0, colors.Length);
        lights.color = colors[__value];
        GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
        GetComponent<Renderer>().material.color = colors[__value];
    }
}

