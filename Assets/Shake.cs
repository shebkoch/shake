using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shake : MonoBehaviour {

	public GameObject startMenu;
	public GameObject endMenu;
	Swap swap;
	Transform cap;
	public Image slider;
	public float maxScore;
	bool isEnd = false;
	float magnitude = 0;
	public static Shake Instance { get; private set; }
	public void Awake() {
		Instance = this;
	}
	
	void Start()
	{
		swap = Swap.Instance;
	}
	void Update () {
		magnitude = Mathf.Abs(Input.acceleration.y - magnitude);
		cap = swap.GetCap();
		if (magnitude >= 0.5 && swap.IsBought()) { //TODO: add ui
			startMenu.SetActive(false);
			maxScore += magnitude;
			slider.fillAmount = maxScore / 250;
		}
		if (maxScore >= 250)
		{
			if (!isEnd) {
				endMenu.SetActive(true);
				EconomicsControl.Instance.RewardGain();
				isEnd = true;
			}
			cap.position = new Vector3(cap.position.x, cap.position.y + 0.5f, 0);
		}
	}
}
