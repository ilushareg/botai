using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTest : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        
        GameObject target = GameObject.Find("Sphere_target");
        Vector3 start = transform.position;
        Vector3 end = target.transform.position;
        Vector3 dir = end - start;

        RaycastHit[] hit = Physics.RaycastAll(start + dir.normalized, dir.normalized, dir.magnitude-1.0f, 0xffff);

        foreach (RaycastHit hitInfo in hit)
        {

            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 10);
        }

        Debug.DrawLine(start, end);

    }
}
