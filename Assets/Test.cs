using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public List<GameObject> toInit = new List<GameObject>();
	void Start()
	{
		foreach (var i in toInit) Instantiate(i);
	}

	
}
