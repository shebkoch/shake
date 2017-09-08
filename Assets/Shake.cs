using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public GameObject startMenu;
	public GameObject cork;
	public Image slider;
	public float maxScore;
	public bool isClicked = false;
	float magnitude;
	void ClickTest()
	{
		isClicked = true;
	}
	void Update () {
		//magnitude = Input.acceleration.magnitude;
		magnitude = Input.acceleration.y;
		Vector3 corkPos = cork.transform.position;
		//if (magnitude >= 0.5) {
			startMenu.SetActive(false);
			maxScore += magnitude;
		if(isClicked) maxScore += 0.5f;
			slider.fillAmount = maxScore / 250;
		//}
		if (maxScore >= 250)
			cork.transform.position = new Vector3(corkPos.x, corkPos.y+0.5f,0);
		//Debug.Log(maxScore);
		//Debug.Log("sli" + slider.fillAmount);
	}
}
