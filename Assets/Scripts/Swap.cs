using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Swap : MonoBehaviour {
	public static Swap Instance { get; private set; }
	public Transform canvas;
	public GameObject buyButton;
	int bottleCount;
	public int bottlePointer = 0;
	public List<Bottle> bottles = new List<Bottle>();
	public TextMeshProUGUI needScoreText;
	private int startReward;

	public AudioSource audioSource;
	

	public void Awake() {
		Instance = this;
	}
	void Start () {
		foreach (var bottle in bottles)
		{
			bottle.bottle = Instantiate(bottle.bottle);
			bottle.shadowBottle = Instantiate(bottle.shadowBottle);
			bottle.cap = bottle.bottle.transform.GetChild(0);
			bottle.foam = bottle.bottle.transform.GetChild(1).gameObject;
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
		int scoreToNext = bottles[bottlePointer].scoreToNext;
		Shake.Instance.maxScore = MaxScoreFunc(scoreToNext);
		Shake.Instance.scoreReduceSpeed = ScoreReduceFunc(scoreToNext);
		startReward = 40;
		EconomicsControl.Instance.reward = scoreToNext/bottles[bottlePointer].estimatedCount;
		
		if (bottles[bottlePointer].scoreToNext <= EconomicsControl.score) {
			bottles[bottlePointer].bottle.SetActive(true);
			bottles[bottlePointer].shadowBottle.SetActive(false);
			needScoreText.text = null;
		}
		else
		{

			needScoreText.text = scoreToNext.ToString(); 
			bottles[bottlePointer].shadowBottle.SetActive(true);
			bottles[bottlePointer].bottle.SetActive(false);
		}
	}

	private float ScoreReduceFunc(float x)
	{
		return 0.013f * x + 500;
	}

	private float MaxScoreFunc(float x)
	{
		return 4000*Mathf.Log(x/100, 2)+4000;
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

	public void Foam()
	{
		bottles[bottlePointer].foam.SetActive(true);
	}

	public void EndFoam()
	{
		bottles[bottlePointer].foam.SetActive(false);
	}

	public void Play()
	{
		audioSource.PlayOneShot(bottles[bottlePointer].audio);
	}
}
