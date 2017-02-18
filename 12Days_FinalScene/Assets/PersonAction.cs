using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin.Vive;

public class PersonAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Physics.OverlapBox ();		
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Sword")
		{
			Debug.Log ("DIED");
		}
	}




}
