using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdControl : MonoBehaviour 
{
    private BannerView bannerView;
    public void Start()
    {
		MobileAds.Initialize("ca-app-pub-1099518655144958~3803324274");
        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-1099518655144958/5920256074";
		// string adUnitId = "ca-app-pub-1099518655144958/1149240778";
#else
            string adUnitId = "unexpected_platform";
#endif
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
   }
}
