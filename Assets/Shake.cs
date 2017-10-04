using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour
{
	public AudioClip aplo; 
	public GameObject startMenu;
	public GameObject endMenu;
	Swap swap;
	Transform cap;
	public Image slider;
	public float maxScore;
	bool isEnd = false;
	float magnitude = 0;
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	float lowPassKernelWidthInSeconds = 1.0f;
	float shakeDetectionThreshold = 1.0f;
	float lowPassFilterFactor;
	Vector3 lowPassValue;
	AudioSource audioSource;
	public static Shake Instance { get; private set; }
	public void Awake() {
		Instance = this;
	}

	void Start() {
		swap = Swap.Instance;
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
		audioSource = GetComponent<AudioSource>();
	}
	public void StartGame() {
		endMenu.SetActive(false);
		startMenu.SetActive(true);
		slider.fillAmount = 0;
		maxScore = 0;
		isEnd = false;
		swap.CapAtStartPosition();
	}
	void Update() {
		cap = swap.GetCap();
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;
		float magnitude = deltaAcceleration.sqrMagnitude;
		if (magnitude >= shakeDetectionThreshold && swap.IsBought()) {
			startMenu.SetActive(false);
			maxScore += magnitude * Time.deltaTime;
			slider.fillAmount = maxScore / 1000;
		} //TODO: add ui
		slider.fillAmount -= 0.1f;
		if (maxScore >= 1000) {
			if (!isEnd) {
				endMenu.SetActive(true);
				EconomicsControl.Instance.RewardGain();
				audioSource.PlayOneShot(aplo);
				isEnd = true;
			}
			cap.position = new Vector3(cap.position.x, cap.position.y + 0.5f, 0);
		}
	}
}
