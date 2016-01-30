using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TextureRandomizer : MonoBehaviour
{
    public Texture[] textures;

    void Start()
    {
        int __value = Random.Range(0, textures.Length);
        GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));
        GetComponent<Renderer>().material.mainTexture = textures[__value];
    }
}

