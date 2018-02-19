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
	
	public GameObject startMenu;
	public GameObject endMenu;
	Swap swap;
	Acceleration acceleration;
	AudioControl audioControl;
	EconomicsControl economicsControl;
	Transform cap;
	public Image slider;

	public float score;
	public float maxScore = 5000;
	public float scoreReduceSpeed = 100;
	//TODO: надо ли?
	//public float timer;		
	//public Text timerText;
	bool isEnd = false;
	//float magnitude = 0;
	//float accelerometerUpdateInterval = 1.0f / 60.0f;
	//float lowPassKernelWidthInSeconds = 1.0f;
	//float shakeDetectionThreshold = 1.0f;
	//float lowPassFilterFactor;
	//Vector3 lowPassValue;
	public static Shake Instance { get; private set; }
	public void Awake() {
		Instance = this;
	}

	void Start() {
		swap = Swap.Instance;
		economicsControl = EconomicsControl.Instance;
		audioControl = AudioControl.Instance;
		acceleration = new Acceleration();
		//lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;		//выделить отдельно
		//shakeDetectionThreshold *= shakeDetectionThreshold;
		//lowPassValue = Input.acceleration;
	}
	public void StartGame() {
		endMenu.SetActive(false);
		startMenu.SetActive(true);
		slider.fillAmount = 0;
		score = 0;
		isEnd = false;
		swap.CapAtStartPosition();
	}
	
	float ScoreInTimeReduce()
	{
		if (score <= 0) return 0;
		else return scoreReduceSpeed * Time.deltaTime;
	}
	void Update() {
		cap = swap.GetCap(); //TODO: Поменять
		//Vector3 acceleration = Input.acceleration;
		//lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		//Vector3 deltaAcceleration = acceleration - lowPassValue;
		//float magnitude = deltaAcceleration.sqrMagnitude;
		if (acceleration.IsShaking() && swap.IsBought())
		{
			startMenu.SetActive(false);
			score += acceleration.Magnitude;
		} //TODO: add ui
		score -= ScoreInTimeReduce();
		slider.fillAmount = score / maxScore;
		if (slider.fillAmount >= 1) {
			if (!isEnd) {
				endMenu.SetActive(true);
				economicsControl.RewardGain();
				audioControl.PlayRandomAplous();
				isEnd = true;
			}
			cap.position = new Vector3(cap.position.x, cap.position.y + 0.5f, 0); //TODO: сто пудово поменять
		}
	}
}
