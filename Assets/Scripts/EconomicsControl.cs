using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class EconomicsControl : MonoBehaviour {

	Swap swap;
	public Text moneyText;
	public static int score = 0;
	
	public int reward;
	public static EconomicsControl Instance { get; private set; }
	public void Awake() {
		Instance = this;
		swap = Swap.Instance;
		if (PlayerPrefs.HasKey("score"))
		{
			score = PlayerPrefs.GetInt("score");
		}
		
	}
	public void RewardGain()
	{
		score += reward;
		print(score);
		moneyText.text = score.ToString();
		PlayerPrefs.SetInt("score", score);
	}
	public void ShowRewardedVideo() {
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		Advertisement.Show("rewardedVideo", options);
	}

	void HandleShowResult(ShowResult result) {
		if (result == ShowResult.Finished) {
			
		}//TODO: unity man
	}
}
