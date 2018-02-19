using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class EconomicsControl : MonoBehaviour {

	Swap swap;
	public Text moneyText;
	public static int money = 0;
	public int reward;
	public static EconomicsControl Instance { get; private set; }
	public void Awake() {
		Instance = this;
		swap = Swap.Instance;
		MoneyChange();
	}
	public void Buy() {
		if (money >= swap.GetCost()) {
			swap.IsBought(true);
			money -= swap.GetCost();
			MoneyChange();
		}//TODO: Message
	}
	public void RewardGain() {
		money += reward;
		MoneyChange();
	}
	public void ShowRewardedVideo() {
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		Advertisement.Show("rewardedVideo", options);
	}

	void HandleShowResult(ShowResult result) {
		if (result == ShowResult.Finished) {
			RewardGain();
		}//TODO: unity man
	}
	public void MoneyChange() {
		moneyText.text = money.ToString();
	}
}
