using Core.IAP;
using UnityEngine;
using UnityEngine.Purchasing;

public class NoAds : MonoBehaviour
{
    [SerializeField] private Ads ads;
    [SerializeField] private GameManager GameManager;

    public void BuyNoAds()
    {
        IAPManager.Instance.BuyNoAds();
    }

    public void BuyUnlockAll()
    {
        IAPManager.Instance.BuyUnlockAll();
    }


    public void OnPurchaseCompltete(Product product)
    {
        if (product.definition.id == "no_ads")
        {
            PlayerPrefs.SetInt("NoAds", 1);
            //ads.HideBanner();
        }
        else if (product.definition.id == "unlock_all")
        {
            PlayerPrefs.SetInt("UnlockedThemes", 9);
            PlayerPrefs.SetInt("UnlockedTubes", 2);
            PlayerPrefs.SetInt("NoAds", 1);
        }

        GameManager.DisablePurchaseButtons();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        if (product != null && product.definition != null)
        {
            Debug.Log(product.definition.id + " failed to buy: " + reason);
        }
    }
}