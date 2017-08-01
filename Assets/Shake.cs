using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public float magnitude;
	public float time;
	public Text score;
	int sc = 0;
	void Update () 
		if (Input.acceleration.magnitude < 1f) sc++;
		score.text = sc.ToString(); 
	}
}
