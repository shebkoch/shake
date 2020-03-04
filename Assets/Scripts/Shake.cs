using System.Collections;
using System.Collections.Generic;
using TMPro;
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
	public float speed;
	public float speedCorrectFactor;
	public TextMeshProUGUI speedText;

	public AudioSource slowShake;
	public AudioSource fastShake;
	public float speedToFast;
	private bool isSlowPlaying = true;
	private bool isPlaying = false;

	public GameObject menuButton;
	
//	public bool shaking;
	public static Shake Instance { get; private set; }
	public void Awake() {
		Instance = this;
	}

	void Start()
	{
		slowShake.loop = true;
		fastShake.loop = true;
		swap = Swap.Instance;
		economicsControl = EconomicsControl.Instance;
		audioControl = AudioControl.Instance;
		acceleration = new Acceleration();
	}
	public void StartGame() {
		menuButton.SetActive(false);
		endMenu.SetActive(false);
		startMenu.SetActive(true); 
//		scoreMap.SetActive(false);//TODO
		slider.gameObject.SetActive(true);
		slider.fillAmount = 0;
		score = 0;
		isEnd = false;
		swap.CapAtStartPosition();
		swap.EndFoam();
		isInGame = false;
		isPlaying = false;
	}
	public float GetCorrectSpeed()
	{
		int time = (int) (Time.time - speed);
		time = time <= 0 ? 1 : time;
		return (score / time * speedCorrectFactor);
	}
	public void EndGame()
	{
		if (!isEnd)
		{
			swap.Play();
			menuButton.SetActive(false);
			slowShake.Stop();
			fastShake.Stop();
			endMenu.SetActive(true);
//			scoreMap.SetActive(true);    //TODO
			slider.gameObject.SetActive(false);
			economicsControl.RewardGain();
			audioControl.PlayRandomAplous();
			isEnd = true;
			speedText.text = $"Your Speed is {GetCorrectSpeed():F1}Sh/m";
			economicsControl.End();
		}
	}
	
	float ScoreInTimeReduce()
	{
		if (score <= 0) return 0;
		else return scoreReduceSpeed * Time.deltaTime;
	}
	void Update()
	{
		bool shaking = acceleration.IsShaking();
		if (shaking && swap.isCurrentOpen())
		{
			if (!isEnd)
			{
				menuButton.SetActive(true);
				if (!isPlaying || (isSlowPlaying && GetCorrectSpeed() > speedToFast))
				{
					isSlowPlaying = false;
					isPlaying = true;
					fastShake.Play();
					slowShake.Stop();
				}

				if (!isPlaying || (!isSlowPlaying && GetCorrectSpeed() < speedToFast))
				{
					isSlowPlaying = true;
					isPlaying = true;
					slowShake.Play();

					fastShake.Stop();
				}
			}
			if (!isInGame)
			{
				isInGame = true;
				startMenu.SetActive(false);
				speed = Time.time;
			}			
			score += acceleration.Magnitude;
		}
		else
		{
			slowShake.Pause();
			fastShake.Pause();
			isPlaying = false;
		}
		//TODO: add ui
		score -= ScoreInTimeReduce();
		slider.fillAmount = score / maxScore;
		if (slider.fillAmount >= 1)
		{
			EndGame();
			swap.CapForce();
			swap.Foam();
		}
	}
}
