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
		//magnitude = Input.acceleration.magnitude;
		magnitude = Input.acceleration.y;
		Vector3 corkPos = cork.transform.position;
		if (magnitude >= 0.5) {
			startMenu.SetActive(false);
			maxScore += magnitude;
			//maxScore += 0.5f;
			slider.fillAmount = maxScore / 500;
		}
		if (maxScore >= 500)
			cork.transform.position = new Vector3(corkPos.x, corkPos.y+0.5f,0);
		//Debug.Log(maxScore);
		//Debug.Log("sli" + slider.fillAmount);
	}
}
