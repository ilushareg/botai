using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCustom : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BoxCollider[] c = transform.GetComponentsInChildren<BoxCollider>();
        foreach(BoxCollider b in c)
        {

            Debug.Log(b.name);
            Debug.Log(b.transform);
            Debug.Log(b.center);
            Debug.Log(b.size);
            //Generate planes for that cube

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
