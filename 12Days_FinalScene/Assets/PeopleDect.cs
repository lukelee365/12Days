using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleDect : MonoBehaviour {
	public AudioSource speech;
	public GameObject target;
	private bool playonece;
	// Use this for initialization
	void Start () {
		playonece = true;

	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (transform.position, target.transform.position);
	
		if (dist < 1.7f&&playonece)
		{
			
			speech.Play ();
			playonece = false;
		}
	}


}
