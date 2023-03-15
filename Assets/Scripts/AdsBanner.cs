using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsBanner : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID

        // Test용
        string bannerId = "ca-app-pub-3940256099942544/6300978111";
        // 출시용
        // string bannerId = "ca-app-pub-2128363078502034/2953803498";

#else
            string bannerId = "unexpected_platform";
#endif
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //
        });

        LoadBannerAd(bannerId);
    }

    public void CreateBannerView(string bannerId)
    {

        // 배너가 존재할시 배너 삭제
        if (bannerView != null)
        {
            DestroyAd();
        }

        // 320 * 50배너 생성
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

    }

    public void LoadBannerAd(string bannerId)
    {
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView(bannerId);
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