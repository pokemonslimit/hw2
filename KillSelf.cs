using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour {

	float aliveTime =5;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", aliveTime);
	}
	
	public void DestroySelf()
	{
		GameObject.Destroy (this.gameObject);
	}
}
