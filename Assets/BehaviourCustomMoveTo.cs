using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourCustomMoveTo : BehaviourCustom {

	Vector3 target;
    NavMeshAgent nma = null;

    //simple stupid timeout
    float endTime = 0.0f;

    // Update is called once per frame
    public void GoTo(Vector3 t)
	{
		target = t;
        nma = this.GetGo().transform.GetComponentInChildren<NavMeshAgent>();
        nma.SetDestination(target);

        endTime = Time.time + 3000.0f;
    }

    public override void Update () 
	{
    }

	public override bool IsDone()
	{
		if(nma.remainingDistance < 0.2f)
		{
			return true;
		}
        if (endTime < Time.time)
        { 
            return true; 
        }

		return false;
	}

}
