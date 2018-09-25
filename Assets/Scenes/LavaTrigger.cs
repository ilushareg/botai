using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("something has entered the volume");

		try
		{
			SimpleAI ai = other.gameObject.GetComponentInChildren<SimpleAI> ();
			if (ai != null)
			{
				ai.Kill();
			}
		}
		catch(System.Exception e) 
		{
			Debug.Log ("Something went wrong" + e.Message);
		}
	}


}
