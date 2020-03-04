using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Assets.Scripts;
using UnityEngine;
//using UnityEngine.Monetization;
using UnityEngine.UI;
public class EconomicsControl : MonoBehaviour {

	Swap swap;
	public TextMeshProUGUI moneyText;
	public static int score = 100;
	
	public int reward;
	public static EconomicsControl Instance { get; private set; }

	public int countToBanner = 3;
	public float animationTime;

	private float currentAnimationScore;
	public void Awake() {
		Instance = this;
		swap = Swap.Instance;
		if (PlayerPrefs.HasKey("score"))
		{
			score = PlayerPrefs.GetInt("score");
		}

		currentAnimationScore = score;
		moneyText.text = score.ToString();

	}
	
	private void Update()
	{
		if (currentAnimationScore < score)
		{
			currentAnimationScore += (animationTime * Time.deltaTime)*(score-currentAnimationScore);
		}
		else
		{
			currentAnimationScore = score;
		
		}


		moneyText.text = currentAnimationScore.ToString("F0");
	}

	public void RewardGain()
	{
		score += reward;
		print(score);
		
		PlayerPrefs.SetInt("score", score);
	}

	public void RewardGain(double count)
	{
		score += (int)(count*reward);
		print(score);
		PlayerPrefs.SetInt("score", score);
	}

	public void ShowRewardedVideo()
	{
		AdsController.instance.ShowRewarded();
	}

	public void End()
	{
		if(countToBanner <= 0)
			AdsController.instance.ShowBanner();
		else
			countToBanner--;
		
		AdsController.countToShow = AddShowFunc(reward); 
		AdsController.instance.ShowInterstitialIfNotRewarded();
	}

	private int AddShowFunc(int reward)
	{
		return 4 / (reward / 500 + 1);
	}
}
