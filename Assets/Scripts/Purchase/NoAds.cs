using UnityEngine;
using UnityEngine.Purchasing;

public class NoAds : MonoBehaviour
{
    [SerializeField] private Ads ads;
    [SerializeField] private GameManager GameManager;

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
        }

        GameManager.DisablePurchaseButtons();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed to buy: " + reason);
    }
}
