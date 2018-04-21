using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Swap : MonoBehaviour {
	public static Swap Instance { get; private set; }
	public Transform canvas;
	public GameObject buyButton;
	int bottleCount;
	public int bottlePointer = 0;
	public List<Bottle> bottles = new List<Bottle>();
	public void Awake() {
		Instance = this;
	}
	void Start () {
		foreach (var bottle in bottles)
		{
			bottle.bottle = Instantiate(bottle.bottle);
			bottle.shadowBottle = Instantiate(bottle.shadowBottle);
			bottle.cap = bottle.bottle.transform.GetChild(0);
			bottle.capStartPosition = bottle.cap.position;
		}
		bottleCount = bottles.Count;
		//for(int i = 0; i < bottleCount; i++) {
		//	bottles[i].bottle = Instantiate(bottles[i].bottle);
		//	//bottles[i].bottle.transform.SetParent(canvas, false);
		//	bottles[i].cap = bottles[i].bottle.transform.GetChild(0);
		//	bottles[i].capStartPosition = bottles[i].cap.position;
		//}
		ShowBottle();
	}
	public void LeftSwap() {
		bottlePointer--;
		if (bottlePointer < 0) bottlePointer = bottleCount - 1;
		ShowBottle();
	}
	public void RightSwap() {
		bottlePointer++;
		if (bottlePointer > bottleCount - 1) bottlePointer = 0;
		ShowBottle();
	}
	
	public int GetCost() {
		return 0;//TODO
	}
	public bool isCurrentOpen(){
		return bottles[bottlePointer].scoreToNext <= EconomicsControl.score;
	}
	public void ShowBottle() {
		foreach (var bottle in bottles) {
			bottle.bottle.SetActive(false);
			bottle.shadowBottle.SetActive(false);

		}

		if (bottles[bottlePointer].scoreToNext <= EconomicsControl.score) {
			bottles[bottlePointer].bottle.SetActive(true);
			bottles[bottlePointer].shadowBottle.SetActive(false);
		}
		else {
			bottles[bottlePointer].shadowBottle.SetActive(true);
			bottles[bottlePointer].bottle.SetActive(false);
		}
	}
	public void CapForce()
	{
		bottles[bottlePointer].cap.transform.Translate(Vector3.up * Time.deltaTime * 10); //TODO: change
	}
	//public Transform GetCap() {
	//	return bottles[bottlePointer].cap;
	//}
	public void CapAtStartPosition() {
		bottles[bottlePointer].cap.position = bottles[bottlePointer].capStartPosition;
	}
	void Update () {
		
	}
}
