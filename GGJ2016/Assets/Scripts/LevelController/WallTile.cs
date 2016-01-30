using UnityEngine;
using System.Collections;

public class WallTile : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		float __value = Random.Range(3f, 5f);
		transform.localScale = new Vector3(2f, __value, 2f);
		transform.Translate(0, (__value * 0.5f) - 2f, 0);

		float __colorValue = Random.Range(0.6f, 0.95f);
		GetComponent<Renderer>().material.color = new Color(__colorValue, __colorValue, __colorValue);
	}
}
