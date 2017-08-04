using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public GameObject startMenu;
	public GameObject cork;
	public Image slider;
	public float maxScore;
	float magnitude;
	void Update () {
		magnitude = Input.acceleration.magnitude;
		Vector3 corkPos = cork.transform.position;
		if (magnitude >= 0.5) {
			startMenu.SetActive(false);
			maxScore += magnitude;
			slider.fillAmount = maxScore / 100;
		}
		//if (maxScore >= 100)
			cork.transform.position = new Vector3(corkPos.x, corkPos.y+0.5f,0);
	}
}
