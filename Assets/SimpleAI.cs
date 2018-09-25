using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

	private BehaviourCustom beh = null;

    // Use this for initialization
    void Awake()
    {

        NewBestGoal();
    }

    void Start()
    {

        NewBestGoal();
    }

    public void SetBehaviour(BehaviourCustom b)
	{
        Debug.Log("New Beh set");
		this.beh = b;
		this.beh.SetGo(this.gameObject);
	}

	//New Goal
	void NewBestGoal()
	{
		BehaviourCustomMoveTo b = new BehaviourCustomMoveTo();
		SetBehaviour (b);
        b.GoTo(new Vector3(Random.Range(-5.0f, 5.0f), 2.0f, Random.Range(-5.0f, 5.0f)));

    }

    // Update is called once per frame
    void Update () {

        this.beh.Update();

		if (this.beh.IsDone ()) {
			NewBestGoal ();
		}

	}

	//Allow others to command this entity to die (triggers etc.)
	public void Kill()
	{
		transform.position = new Vector3 (0.0f, 2.0f, 0.0f);
		NewBestGoal ();
	}

}
