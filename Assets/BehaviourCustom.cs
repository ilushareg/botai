using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCustom{

	private GameObject gameObject = null;

	// Update is called once per frame
	public virtual void Update () {}
	public virtual void SetGo(GameObject g)
	{
		this.gameObject = g;
	}

	public virtual GameObject GetGo()
	{
		return this.gameObject;
	}

    public virtual bool IsDone(){ return false;}

}
