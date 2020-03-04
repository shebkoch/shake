using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;


public class AdsController : MonoBehaviour
{
	private const String APP_KEY = "e5684c3fbe47b4b5f35d5877460c3e0972d2f4a3f469b9c8";
	public bool isTesting = true;
	public bool isDebug = true;

	public static int countToShow = 4;
	public int currentCountToShow = countToShow;
	public static bool isRewardedShown = false;
	public static AdsController instance;

	private readonly string[] DISABLED_NETWORKS = {"facebook", "flurry", "pubnative", "inmobi"};

	private void Awake()
	{
		instance = this;
		if (isDebug) return;
		Initialize();
		Appodeal.setRewardedVideoCallbacks(new RewardedAdListener());
	}

	private void Initialize()
	{
		foreach (string nw in DISABLED_NETWORKS)
		{
			Appodeal.disableNetwork(nw);
		}

		Appodeal.muteVideosIfCallsMuted(true);
		Appodeal.setTesting(isTesting);
		Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL);
		Appodeal.initialize(APP_KEY, Appodeal.BANNER_BOTTOM);
		Appodeal.initialize(APP_KEY, Appodeal.REWARDED_VIDEO);
	}

	public void ShowInterstitialIfNotRewarded()
	{
		if (isRewardedShown)
		{
			isRewardedShown = false;
			currentCountToShow = countToShow;
		}
		
		ShowInterstitial();
	}
	public void ShowInterstitial()
	{
		currentCountToShow--;
		if (isDebug || currentCountToShow > 0) return;
		if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
		{
			Appodeal.show(Appodeal.INTERSTITIAL);
		}
	}

	private bool isShow = false;

	public void ShowBanner()
	{
		if (!isShow && Appodeal.isLoaded(Appodeal.BANNER_BOTTOM))
		{
			isShow = true;
			Appodeal.show(Appodeal.BANNER_BOTTOM);
		}
	}

	public void ShowRewarded()
	{
		Appodeal.show(Appodeal.REWARDED_VIDEO);
	}
	
	public class RewardedAdListener : IRewardedVideoAdListener
	{
		public RewardedAdListener()
		{
			
		}
		#region Rewarded Video callback handlers
		public void onRewardedVideoLoaded(bool isPrecache) { print("Video loaded"); } 
		public void onRewardedVideoFailedToLoad() { print("Video failed"); } // Вызывается, когда видео с наградой за просмотр не загрузилось
		public void onRewardedVideoShowFailed() { print ("RewardedVideo show failed"); } // Вызывается, когда видео с наградой загрузилось, но не может быть показано (внутренние ошибки сети, настройки плейсментов или неверный креатив)
		public void onRewardedVideoShown() { print("Video shown"); } // Вызывается после показа видео с наградой за просмотр
		public void onRewardedVideoClicked() { print("Video clicked"); } // Вызывается при клике на видео с наградой за просмотр
		public void onRewardedVideoClosed(bool finished) { print("Video closed"); } // Вызывается при закрытии видео с наградой за просмотр

		public void onRewardedVideoFinished(double amount, string name)
		{
			isRewardedShown = true;
			amount = amount <= 0 ?  1 : amount;
			EconomicsControl.Instance.RewardGain(amount);
		} // Вызывается, если видео с наградой за просмотр просмотрено полностью
		public void onRewardedVideoExpired() { print("Video expired"); } // Вызывается, когда видео с наградой за просмотр больше не доступно.
		#endregion
	}
}

