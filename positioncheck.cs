using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positioncheck : MonoBehaviour {

	public GameObject root;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = root.transform.position;
		this.transform.rotation = root.transform.rotation;
	}
}
