using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTest : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
    struct MyHitInfo
    {
        public bool bHit;
        public Vector3 coordinate;
    };

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

            Quaternion[] rotations =
            {
                Quaternion.AngleAxis(0, Vector3.up),
                Quaternion.AngleAxis(90, Vector3.up),
                Quaternion.AngleAxis(180, Vector3.up),
                Quaternion.AngleAxis(270, Vector3.up),
                Quaternion.AngleAxis(90, Vector3.right),
                Quaternion.AngleAxis(270, Vector3.right)
            };

            Vector3[][] tris = { new Vector3[]{ new Vector3(-1, 1), new Vector3(1, 1), new Vector3(1, -1)},
            new Vector3[]{ new Vector3(1, -1), new Vector3(-1, -1), new Vector3(-1, 1)}};

            BoxCollider[] colliders = staticObj.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider box in colliders)
            {
                foreach(Quaternion rot in rotations)
                {
                    foreach (Vector3[] tri in tris)
                    {
                        Vector3 n = rot * Vector3.forward;


                        Vector3 v0 = rot * tri[0];
                        Vector3 v1 = rot * tri[1];
                        Vector3 v2 = rot * tri[2];
                        //make triangle and collide


                        int a = 0;
                        //trPlaneSpace.

                    }
                }
            }
        }

       Debug.DrawLine(start, end);


    }

    //http://geomalgorithms.com/a06-_intersect-2.html#Segment-Triangle
    MyHitInfo TriangleRayCast(Vector3 p0, Vector3 p1, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        //TODO: can we return a tuple or is there a way to define default constructor for struct
        MyHitInfo newInfo = new MyHitInfo();
        newInfo.bHit = false;


        return newInfo; 
    }
}
