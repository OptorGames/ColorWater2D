using UnityEngine;
using UnityEngine.Purchasing;

public class NoAds : MonoBehaviour
{
    [SerializeField] private Ads ads;

    public void OnPurchaseCompltete(Product product)
    {
        if (product.definition.id == "no_ads")
        {
            PlayerPrefs.SetInt("NoAds", 1);
            ads.HideBanner();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed to buy: " + reason);
    }
}
