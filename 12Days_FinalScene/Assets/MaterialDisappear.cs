using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDisappear : MonoBehaviour {
	public MeshRenderer[] renders;
	public GameObject[] disableObjs;
	public MeshRenderer humanRender;
	public GameObject humanPoly;
	private float recoverRate;
	private float appearRate;
	public bool  startWorldFade;
	public bool startHumanApear;
	public GameObject light;
	// Use this for initialization
	void Start () {
		light.SetActive (false);
		recoverRate = 0f;
		appearRate = 1;
		for (int i = 0; i < renders.Length; i++)
		{
			renders[i].material.shader = Shader.Find ("Dissolving");
		}

		humanRender.material.shader = Shader.Find ("Dissolving");

	}
	
	// Update is called once per frame
	void Update () {
		//float SliceAmount = Mathf.PingPong(Time.time, 1.0F);
		if(startWorldFade){
		recoverRate = recoverRate + 0.001f;
		foreach (GameObject obj in disableObjs)
		{
			obj.SetActive (false);
		}
		for (int i = 0; i < renders.Length; i++)
		{
			renders[i].material.SetFloat("_SliceAmount",recoverRate);

		}
		//mat.SetFloat("SliceAmount", 0.6f);
		}

		if(startHumanApear){
			//speed
			appearRate = appearRate - 0.003f;
			if (appearRate >= -1f)
			{
				light.SetActive (true);
				humanPoly.SetActive (false);
				humanRender.material.SetFloat ("_SliceAmount", appearRate);
			} else
			{
				

			
			}
			//mat.SetFloat("SliceAmount", 0.6f);
		}
	}

	void StartAnimWorld(){
		startWorldFade = true;
	}

	void StartAnimHuman(){
		startHumanApear = true;
	}

}
