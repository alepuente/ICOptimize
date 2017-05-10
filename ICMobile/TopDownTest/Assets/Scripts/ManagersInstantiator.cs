using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersInstantiator : MonoBehaviour
{
	public List<GameObject> managers = new List<GameObject>();

	// Use this for initialization
	public void Awake () 
	{
		for (int i = 0; i < managers.Count; i++) 
		{
			GameObject go = GameObject.Instantiate (managers [i]);
			go.transform.SetParent (this.transform, false);
		}
	}
}
