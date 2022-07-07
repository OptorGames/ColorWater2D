using UnityEngine;
using UnityEngine.Purchasing;

//[RequireComponent(typeof(GameManager))]
public class BuySteps : MonoBehaviour
{
    public void OnPurchaseCompltete(Product product)
    {
        if (product.definition.id == "steps_30")
        {
            int steps = PlayerPrefs.GetInt("Steps") + 30;

            PlayerPrefs.SetInt("Steps", steps);
            GetComponent<GameManager>().UpdateTextSteps();
        }
        else if (product.definition.id == "steps_100")
        {
            int steps = PlayerPrefs.GetInt("Steps") + 100;

            PlayerPrefs.SetInt("Steps", steps);
            GetComponent<GameManager>().UpdateTextSteps();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed to buy: " + reason);
    }
}
