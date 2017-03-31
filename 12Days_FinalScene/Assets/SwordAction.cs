using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordAction : MonoBehaviour
{
    public GameObject model;
    public LayerMask layer;
    public float radius;
    public GameObject controllerRight;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController controller;
    private AudioSource[] hurtSound;
    public GameObject hitEffect;
    private List<GameObject> particlesTable;
    private int indexOfparticles;
    public float explodeForce;
    public GameObject UI1;
    public GameObject UI2;
    public GameObject[] pieces;
	public GameObject portalEffect;
	private GameObject[] chainPieces1;
	private GameObject[] chainPieces2;
	private GameObject[] chainPieces3;
	private GameObject[] chainPieces4;
	public AudioSource speech;
	private MaterialDisappear materialDisappear;
	private bool stopCheck;
	private bool chain1GetCutted;
	private bool chain2GetCutted;
	private bool chain3GetCutted;
	private bool chain4GetCutted;
	public AudioSource killSound;
	public AudioSource freeSound;
	public AudioSource ThankPlayer;
	private bool killonce;
	private bool releaseonce;
    //    private GameObject[] target;

    //public GameObject humanExplodeEffect;

    private void Awake()
    {
        //      indexOfparticles = 0;
        hurtSound = GetComponents<AudioSource>();
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
		materialDisappear = GameObject.Find ("EnvironmentManager").GetComponent <MaterialDisappear> ();

    }

    // Use this for initialization
    private void Start()
    {
		chain1GetCutted = false;
		chain2GetCutted = false;
		chain3GetCutted = false;
		chain4GetCutted = false;
		killonce = true;
		releaseonce = true;
		stopCheck = false;
        UI1.SetActive(false);
        UI2.SetActive(false);
		portalEffect.SetActive (false);
		chainPieces1 = GameObject.FindGameObjectsWithTag ("ChainPart1");
		chainPieces2 = GameObject.FindGameObjectsWithTag ("ChainPart2");
		chainPieces3 = GameObject.FindGameObjectsWithTag ("ChainPart3");
		chainPieces4 = GameObject.FindGameObjectsWithTag ("ChainPart4");
        //		humanExplodeEffect.SetActive (false);
	
    }

    // Update is called once per frame
    private void Update()
    {
		if (chain1GetCutted && chain2GetCutted && chain3GetCutted && chain4GetCutted)
		{
			StartCoroutine (VicEscape ());
		}
	
    }

    private void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        Vector3 fwd = gameObject.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(model.transform.position, fwd, out hit, 0.4f, layer))
        {
            if (hit.collider.gameObject.tag == "BodyPart")
            {
                device.TriggerHapticPulse(3000);
                //check the sound
                int soundCheck = 0;
                for (int i = 0; i < hurtSound.Length; i++)
                {
                    if (hurtSound[i].isPlaying)
                    {
                        soundCheck++;
                    }
                }
				if (soundCheck <= 0)
				{
					speech.Stop ();
					hurtSound [(int)Random.Range (1, 5)].Play ();
				}
                // SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(500);
                // Destroy(hit.collider.gameObject);
                //				humanExplodeEffect.SetActive (true);
                Vector3 explodePos = hit.point;

                //   target[indexOfparticles] =
                GameObject target = Instantiate(hitEffect, explodePos, Quaternion.identity) as GameObject;
                StartCoroutine(DestroyObjInDelay(target));
                Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
                foreach (Collider coll in colliders)
                {
                    Rigidbody rb = coll.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        //Force, explodeRaduis, Modifer
                        //   rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
                    }
                }
                //numner of pieces left will trigger the event ok kill him
                if (CheckHowMuchPieceLeft() < 180)
                {
                    //Vic Live
                    StartCoroutine(VicDied());
                }
            }
            if (hit.collider.gameObject.tag == "ChainPart1")
            {
                // Destroy(hit.collider.gameObject);
                //				humanExplodeEffect.SetActive (true);
                Vector3 explodePos = hit.point;
                //collection in raduis
                Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
                foreach (Collider coll in colliders)
                {
                    Rigidbody rb = coll.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        //Vic Escape
						ChainFall1 ();
						chain1GetCutted = true;
                        //Force, explodeRaduis, Modifer
                        //  rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
                    }
                }
				//numner of pieces left will trigger the event ok kill him

            }
			if (hit.collider.gameObject.tag == "ChainPart2")
			{
				// Destroy(hit.collider.gameObject);
				//				humanExplodeEffect.SetActive (true);
				Vector3 explodePos = hit.point;
				//collection in raduis
				Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
				foreach (Collider coll in colliders)
				{
					Rigidbody rb = coll.GetComponent<Rigidbody>();
					if (rb != null)
					{
						rb.isKinematic = false;
						//Vic Escape
						ChainFall2 ();
						chain2GetCutted = true;
						//Force, explodeRaduis, Modifer
						//  rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
					}
				}
				//numner of pieces left will trigger the event ok kill him

			}
			if (hit.collider.gameObject.tag == "ChainPart3")
			{
				// Destroy(hit.collider.gameObject);
				//				humanExplodeEffect.SetActive (true);
				Vector3 explodePos = hit.point;
				//collection in raduis
				Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
				foreach (Collider coll in colliders)
				{
					Rigidbody rb = coll.GetComponent<Rigidbody>();
					if (rb != null)
					{
						rb.isKinematic = false;
						//Vic Escape
						ChainFall3 ();
						chain3GetCutted = true;
						//Force, explodeRaduis, Modifer
						//  rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
					}
				}
				//numner of pieces left will trigger the event ok kill him

			}
			if (hit.collider.gameObject.tag == "ChainPart4")
			{
				// Destroy(hit.collider.gameObject);
				//				humanExplodeEffect.SetActive (true);
				Vector3 explodePos = hit.point;
				//collection in raduis
				Collider[] colliders = Physics.OverlapSphere(explodePos, radius);
				foreach (Collider coll in colliders)
				{
					Rigidbody rb = coll.GetComponent<Rigidbody>();
					if (rb != null)
					{
						rb.isKinematic = false;
						//Vic Escape
						ChainFall4 ();
						chain4GetCutted = true;
						//Force, explodeRaduis, Modifer
						//  rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
					}
				}
				//numner of pieces left will trigger the event ok kill him

			}
        }
    }

    private IEnumerator DestroyObjInDelay(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(obj);
    }

    private int CheckHowMuchPieceLeft()
    {		
        int num = 0;
		if (!stopCheck)
		{
			for (int i = 0; i < pieces.Length; i++)
			{
				Rigidbody rb = pieces [i].GetComponent<Rigidbody> ();
				if (rb.isKinematic == true)
				{
					num++;
				}
			}
		}
        return num;
    }

//	private int CheckHowMuchChainPieceLeft()
//	{		
//		
//		int num = 0;
//		if (!stopCheck)
//		{
//			for (int i = 0; i < chainPieces.Length; i++)
//			{
//				Rigidbody rb = chainPieces [i].GetComponent<Rigidbody> ();
//				if (rb.isKinematic == true)
//				{
//					num++;
//				}
//			}
//		}
//		return num;
//	}


    private void EverythingFall()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            Rigidbody rb = pieces[i].GetComponent<Rigidbody>();
            if (rb.isKinematic == true)
            {
                rb.isKinematic = false;
            }
        }
    }

	private void ChainFall1()
	{
		hurtSound [0].Play ();
		for (int i = 0; i < chainPieces1.Length; i++)
		{
			Rigidbody rb = chainPieces1[i].GetComponent<Rigidbody>();
			if (rb.isKinematic == true)
			{
				rb.isKinematic = false;
			}
		}
	}

	private void ChainFall2()
	{hurtSound [0].Play ();
		for (int i = 0; i < chainPieces2.Length; i++)
		{
			Rigidbody rb = chainPieces2[i].GetComponent<Rigidbody>();
			if (rb.isKinematic == true)
			{
				rb.isKinematic = false;
			}
		}
	}
	private void ChainFall3()
	{hurtSound [0].Play ();
		for (int i = 0; i < chainPieces3.Length; i++)
		{
			Rigidbody rb = chainPieces3[i].GetComponent<Rigidbody>();
			if (rb.isKinematic == true)
			{
				rb.isKinematic = false;
			}
		}
	}
	private void ChainFall4()
	{hurtSound [0].Play ();
		for (int i = 0; i < chainPieces4.Length; i++)
		{
			Rigidbody rb = chainPieces4[i].GetComponent<Rigidbody>();
			if (rb.isKinematic == true)
			{
				rb.isKinematic = false;
			}
		}
	}

    private IEnumerator VicDied()
    {
		if (killonce)
		{
			speech.Stop ();
			stopCheck = true;
			EverythingFall ();
			ChainFall1 ();
			ChainFall2 ();
			ChainFall3 ();
			ChainFall4 ();
			materialDisappear.startWorldFade = true;
			killSound.Play ();
			killonce = false;
		}
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(1);

        // Application.Quit();
    }

    private IEnumerator VicEscape()
    {
		if (releaseonce)
		{
			speech.Stop ();
			stopCheck = true;
			EverythingFall ();

			portalEffect.SetActive (true);
			//yield return new WaitForSeconds(2);
			materialDisappear.startHumanApear = true;
			freeSound.Play ();

			Debug.Log ("Heresoundplay");
			releaseonce = false;
			ThankPlayer.Play ();

		}
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(2);
        // Application.Quit();
    }


}