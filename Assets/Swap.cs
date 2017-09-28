using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Swap : MonoBehaviour {
	public static Swap Instance { get; private set; }
	public Transform canvas;
	public GameObject buyButton;
	const int bottleCount = 4;
	int bottlePointer = 0;
	public Bottle[] bottles = new Bottle[bottleCount];
	public void Awake() {
		Instance = this;
	}
	void Start () {
		for(int i = 0; i < bottleCount; i++) {
			bottles[i].bottle = Instantiate(bottles[i].bottle);
			bottles[i].bottle.transform.SetParent(canvas, false);
		}
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
	public bool IsBought(bool isChange = false) {
		if (isChange) bottles[bottlePointer].isOpen = true;
		return bottles[bottlePointer].isOpen;
	}
	
	public int GetCost() {
		return bottles[bottlePointer].cost;
	}
	public void ShowBottle() {
		foreach (var bottle in bottles) {
			bottle.bottle.SetActive(false);
		}
		bottles[bottlePointer].bottle.SetActive(true);
		if (!bottles[bottlePointer].isOpen) {
			bottles[bottlePointer].bottle.GetComponentInChildren<Text>().text = bottles[bottlePointer].cost.ToString();
			buyButton.SetActive(true);
		}else {
			buyButton.SetActive(false);
			bottles[bottlePointer].bottle.GetComponentInChildren<Text>().text = "";

		}
	}
	public Transform GetCap() {
		return bottles[bottlePointer].bottle.transform.GetChild(0);
	}
	void Update () {
		
	}
}
