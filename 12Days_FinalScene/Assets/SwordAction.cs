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

    //    private GameObject[] target;

    //public GameObject humanExplodeEffect;

    private void Awake()
    {
        //      indexOfparticles = 0;
        hurtSound = GetComponents<AudioSource>();
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    private void Start()
    {
        UI1.SetActive(false);
        UI2.SetActive(false);
        //		humanExplodeEffect.SetActive (false);
    }

    // Update is called once per frame
    private void Update()
    {
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
                    hurtSound[(int)Random.Range(0, 4)].Play();
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
                if (CheckHowMuchPieceLeft() < 100)
                {
                    //Vic Live
                    StartCoroutine(VicDied());
                }
            }
            if (hit.collider.gameObject.tag == "ChainPart")
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
                        StartCoroutine(VicEscape());
                        //Force, explodeRaduis, Modifer
                        //  rb.AddExplosionForce(explodeForce, explodePos, radius, 3f);
                    }
                }
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
        for (int i = 0; i < pieces.Length; i++)
        {
            Rigidbody rb = pieces[i].GetComponent<Rigidbody>();
            if (rb.isKinematic == true)
            {
                num++;
            }
        }
        return num;
    }

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

    private IEnumerator VicDied()
    {
        UI1.SetActive(true);
        EverythingFall();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        // Application.Quit();
    }

    private IEnumerator VicEscape()
    {
        UI2.SetActive(true);
        EverythingFall();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
        // Application.Quit();
    }
}