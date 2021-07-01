using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleMobileAdsDemo : MonoBehaviour {


    private BannerView bannerView;

    private int tryLoadAgainCount = 5;



    public void Start() {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        DontDestroyOnLoad(this.gameObject);

        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    }


    public void ShowAd() {
        bannerView.Show();
    }


    private void RequestBanner() {
        //test: ca-app-pub-3940256099942544/6300978111
        
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
        .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }



    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        if (tryLoadAgainCount > 0) {
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            tryLoadAgainCount--;
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args) {
        tryLoadAgainCount = 5;
    }


}