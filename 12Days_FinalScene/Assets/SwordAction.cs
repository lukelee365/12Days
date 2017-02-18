using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : MonoBehaviour {

	public GameObject model;
	public LayerMask layer;
	public GameObject humanExplodeEffect;
	public float explodeForce;
	// Use this for initialization
	void Start () {
		humanExplodeEffect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() 
	{
		Vector3 fwd =gameObject.transform.forward;
		RaycastHit hit;
		if (Physics.Raycast (model.transform.position, fwd, out hit, 1f, layer))
		{
			if (hit.collider.gameObject.name == "Person")
			{
				Debug.Log (hit.collider.gameObject.name);
				Destroy (hit.collider.gameObject);
				humanExplodeEffect.SetActive (true);
				Vector3 explodePos = hit.point;
				//collection in raduis
				Collider[] colliders = Physics.OverlapSphere (explodePos,2f);
				foreach(Collider coll in colliders){
					Rigidbody rb = coll.GetComponent <Rigidbody> ();
					if (rb != null)
						//Force, explodeRaduis, Modifer
						rb.AddExplosionForce (explodeForce, explodePos, 2f, 3f);
				}



			}
		}
	}

}
