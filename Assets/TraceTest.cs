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

        //test with Unity RayCast
        if(true)
        { 
            RaycastHit[] hit = Physics.RaycastAll(start + dir.normalized, dir.normalized, dir.magnitude-1.0f, 0xffff);

            foreach (RaycastHit hitInfo in hit)
            {
                Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 10);
            }
        }

        //test against planes
        GameObject staticObj = GameObject.Find("Static");
        if (true && staticObj != null)
        {
            Vector3[] sides = { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1),
            new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 0, -1)};

            Vector3[][] tris = { new Vector3[]{ new Vector3(-1, 1), new Vector3(1, 1), new Vector3(1, -1)},
            new Vector3[]{ new Vector3(1, -1), new Vector3(-1, -1), new Vector3(-1, 1)}};

            BoxCollider[] colliders = staticObj.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider box in colliders)
            {
                foreach(Vector3 side in sides)
                {
                    foreach (Vector3[] tri in tris)
                    {
                        Matrix4x4 mat = new Matrix4x4(
                            new Vector4(1, 0, 0, 0),
                            new Vector4(0, 1, 0, 0),
                            new Vector4(0, 0, 1, 0),
                            new Vector4(0, 0, 0, 0));

                        Vector3 v0 = mat.MultiplyPoint(tri[0]);
                        Vector3 v1 = mat.MultiplyPoint(tri[1]);
                        Vector3 v2 = mat.MultiplyPoint(tri[2]);
                        //make triangle and collide
                        
                        
                        //trPlaneSpace.

                    }
                }
            }
        }

       Debug.DrawLine(start, end);


    }
}
