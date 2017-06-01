using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosties : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
          gameObject.transform.Rotate(new Vector3(0f, 0f, 30f * Time.deltaTime));
	}
}
