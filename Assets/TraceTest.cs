using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        {

            //MyHitInfo i;

            //i = AABBRayCast(new Vector3(0, 0, -1.4f), new Vector3(0, 0, 2), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            //Debug.Log(i.bHit);
            //Debug.Log(i.coordinate);

         }

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
                Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 2);
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


            //the idea is to convert start/end to plane coordinates and see if it collides 

            BoxCollider[] colliders = staticObj.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider box in colliders)
            {
                Matrix4x4 trmat = box.transform.worldToLocalMatrix;

                Vector3 lstart = box.transform.InverseTransformPoint(start);
                Vector3 lend = box.transform.InverseTransformPoint(end);


                Debug.Log("==============");
                Debug.Log(lstart + "   " + lend);
                Debug.Log(start + "   " + end);

                MyHitInfo i;
                i = AABBRayCast(lstart, lend, box.center, box.size);

                if (i.bHit)
                {
                    Vector3 realcoord = i.coordinate;
                    realcoord = box.transform.TransformPoint(realcoord);

                    //uncomment to check if the transform works correctly
                    float frac = (i.coordinate - lstart).magnitude / (lend - lstart).magnitude;
                    Vector3 coord = start + (end - start) * frac;

                    Debug.Log(realcoord);
                    Debug.DrawLine(realcoord, realcoord + Vector3.up * 3, new Color(0xff, 0, 0));
                    Debug.DrawLine(coord, coord + Vector3.up * 3, new Color(0, 0xff, 0));

                }

            }
        }

       Debug.DrawLine(start, end);


    }

    //https://github.com/erich666/GraphicsGems/blob/master/gems/RayBox.c
    MyHitInfo AABBRayCast(Vector3 p0, Vector3 p1, Vector3 center, Vector3 size)
    {
        //TODO: can we return a tuple or is there a way to define default constructor for struct
        MyHitInfo newInfo = new MyHitInfo();
        newInfo.bHit = false;

        //double minB[NUMDIM], maxB[NUMDIM];		/*box */
        //double origin[NUMDIM], dir[NUMDIM];     /*ray */
        //double coord[NUMDIM];				/* hit point */

        float[] minB = { center.x - size.x / 2, center.y - size.y / 2, center.z - size.z / 2 };
        float[] maxB = { center.x + size.x / 2, center.y + size.y / 2, center.z + size.z / 2 };
        float[] origin = { p0.x, p0.y, p0.z };
//        float[] dir = { (p1 - p0).normalized.x, (p1 - p0).normalized.y, (p1 - p0).normalized.z };
        float[] dir = { (p1 - p0).normalized.x, (p1 - p0).normalized.y, (p1 - p0).normalized.z };
        float[] coord = { 0.0f, 0.0f, 0.0f };

        int NUMDIM = 3;
        int RIGHT = 0;
        int LEFT = 1;
        int MIDDLE = 2;

        {
            bool inside = true;
            
            //was char
            int [] quadrant = new int[NUMDIM];
            int i;
            int whichPlane;
            float []maxT = { 0, 0, 0 };
            float []candidatePlane = { 0, 0, 0 };

            /* Find candidate planes; this loop can be avoided if
            rays cast all from the eye(assume perpsective view) */
            for (i = 0; i < NUMDIM; i++)
            { 
                if (origin[i] < minB[i])
                {
                    quadrant[i] = LEFT;
                    candidatePlane[i] = minB[i];
                    inside = false;
                }
                else if (origin[i] > maxB[i])
                {
                    quadrant[i] = RIGHT;
                    candidatePlane[i] = maxB[i];
                    inside = false;
                }
                else
                {
                    quadrant[i] = MIDDLE;
                }
            }

            /* Ray origin inside bounding box */
            if (inside)
            {
//                coord = origin;
                newInfo.coordinate = new Vector3(origin[0], origin[1], origin[2]);
                newInfo.bHit = true;
                return (newInfo);
            }


            /* Calculate T distances to candidate planes */
            for (i = 0; i < NUMDIM; i++)
                if (quadrant[i] != MIDDLE && dir[i] != 0.0f)
                    maxT[i] = (candidatePlane[i] - origin[i]) / dir[i];
                else
                    maxT[i] = -1.0f;

            /* Get largest of the maxT's for final choice of intersection */
            whichPlane = 0;
            for (i = 1; i < NUMDIM; i++)
                if (maxT[whichPlane] < maxT[i])
                    whichPlane = i;

            /* Check final candidate actually inside box */
            if (maxT[whichPlane] < 0.0f) return (newInfo);
            for (i = 0; i < NUMDIM; i++)
            {
                if (whichPlane != i)
                {
                    coord[i] = origin[i] + maxT[whichPlane] * dir[i];
                    if (coord[i] < minB[i] || coord[i] > maxB[i])
                        return (newInfo);
                }
                else
                {
                    coord[i] = candidatePlane[i];
                }
            }
            newInfo.coordinate = new Vector3(coord[0], coord[1], coord[2]);
            newInfo.bHit = true;
            return (newInfo);
        }

        return newInfo; 
    }
}
