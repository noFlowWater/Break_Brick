using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544/6300978111";

#else
            string appId = "unexpected_platform";
#endif
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //
        });

        LoadAd(appId);
    }

    public void CreateBannerView(string appId)
    {

        // 배너가 존재할시 배너 삭제
        if (bannerView != null)
        {
            DestroyAd();
        }

        // 320 * 50배너 생성
        bannerView = new BannerView(appId, AdSize.Banner, AdPosition.Bottom);

    }

    public void LoadAd(string appId)
    {
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView(appId);
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    public void DestroyAd()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            bannerView.Destroy();
            bannerView = null;
        }
    }

}