using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acceleration
{
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	float lowPassKernelWidthInSeconds = 1.0f;
	float shakeDetectionThreshold = 1.0f;
	float lowPassFilterFactor;
	public float Magnitude { get; set; }
	Vector3 lowPassValue;
	public Acceleration()
	{
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}
	public bool IsShaking()
	{
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;
		Magnitude = deltaAcceleration.sqrMagnitude;
		return Magnitude >= shakeDetectionThreshold;
	}
}
public class Shake : MonoBehaviour
{
	Swap swap;
	Acceleration acceleration;
	AudioControl audioControl;
	EconomicsControl economicsControl;
	Transform cap;

	public GameObject startMenu;
	public GameObject endMenu;
	public GameObject scoreMap;
	public Image slider;

	public float score;
	public float maxScore = 5000;
	public float scoreReduceSpeed = 100;
	public bool isEnd = false;
	bool isInGame = false;
	//TODO: надо ли?
	public float speed;
	public float speedCorrectFactor;
	public Text speedText;

	public static Shake Instance { get; private set; }
	public void Awake() {
		Instance = this;
	}

	void Start() {
		swap = Swap.Instance;
		economicsControl = EconomicsControl.Instance;
		audioControl = AudioControl.Instance;
		acceleration = new Acceleration();
	}
	public void StartGame() {
		endMenu.SetActive(false);
		startMenu.SetActive(true); 
		scoreMap.SetActive(false);//TODO
		slider.gameObject.SetActive(true);
		slider.fillAmount = 0;
		score = 0;
		isEnd = false;
		swap.CapAtStartPosition();
		isInGame = false;
	}
	public string GetCorrectSpeed()
	{
		return ((int)((Time.time - speed) / speedCorrectFactor)).ToString();
	}
	public void EndGame()
	{
		if (!isEnd)
		{
			endMenu.SetActive(true);
			scoreMap.SetActive(true);    //TODO
			slider.gameObject.SetActive(false);
			economicsControl.RewardGain();
			audioControl.PlayRandomAplous();
			isEnd = true;
			speedText.text = "Your speed is: " + GetCorrectSpeed();
		}
	}
	
	float ScoreInTimeReduce()
	{
		if (score <= 0) return 0;
		else return scoreReduceSpeed * Time.deltaTime;
	}
	void Update()
	{
		if (acceleration.IsShaking() && swap.isCurrentOpen())
		{
			if (!isInGame)
			{
				isInGame = true;
				startMenu.SetActive(false);
				speed = Time.time;
			}			
			score += acceleration.Magnitude;
		} //TODO: add ui
		score -= ScoreInTimeReduce();
		slider.fillAmount = score / maxScore;
		if (slider.fillAmount >= 1)
		{
			EndGame();
			swap.CapForce();
		}
	}
}
