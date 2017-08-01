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
		if (magnitude <= 0.5) {
			startMenu.SetActive(false);
		}
		maxScore +=  magnitude;
		slider.fillAmount = maxScore / 100;
		if (maxScore >= 100) cork.transform.position = new Vector3(transform.position.x, transform.position.y-0.1f,0);
	}
}
