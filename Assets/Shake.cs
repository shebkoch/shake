using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public GameObject startMenu;
	Transform cap;
	public Image slider;
	public float maxScore;
	const int bottleCount = 4;
	public GameObject[] bottles = new GameObject[bottleCount];
	int bottlePointer = 0;
	float magnitude;
	public void LeftSwap()
	{
		bottlePointer--;
		if (bottlePointer < 0) bottlePointer = bottles.Length- 1;
		ShowBottle();
	}
	public void RightSwap()
	{
		bottlePointer++;
		if (bottlePointer > bottles.Length - 1) bottlePointer = 0;
		ShowBottle();
	}
	void ShowBottle()
	{
		foreach (var bottle in bottles)
		{
			Debug.Log(1);
			bottle.SetActive(false);
		}
		
		bottles[bottlePointer].SetActive(true);
	}
	void Start()
	{
		bottles = GameObject.FindGameObjectsWithTag("bottle");
		ShowBottle();
	}
	void Update () {
		//magnitude = Input.acceleration.magnitude;
		magnitude = Input.acceleration.y;
		cap = bottles[bottlePointer].transform.GetChild(0);
		if (magnitude >= 0.5) {
			startMenu.SetActive(false);
			maxScore += magnitude;
			slider.fillAmount = maxScore / 250;
		}
		if (maxScore >= 250)
		{
			
			cap.position = new Vector3(cap.position.x, cap.position.y + 0.5f, 0);
		}
		//Debug.Log(maxScore);
		//Debug.Log("sli" + slider.fillAmount);
	}
}
