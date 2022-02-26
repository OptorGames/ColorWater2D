using UnityEngine;
using UnityEngine.Purchasing;

public class NoAds : MonoBehaviour
{
    public void OnPurchaseCompltete(Product product)
    {
        if (product.definition.id == "no_ads")
        {
            PlayerPrefs.SetInt("NoAds", 1);
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed to buy: " + reason);
    }
}
