using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public Text score;
	void Update () {
		int sc = 0;
		if (Input.acceleration.magnitude < 1f) sc++;
		score.text = sc.ToString(); 
	}
}
