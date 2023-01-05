using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Core.IAP
{


    public class IAPManager : MonoBehaviour, IStoreListener
    {

        public static IAPManager Instance;
        [SerializeField] private string _noAdsCatalogName = "no_ads";
        [SerializeField] private string _unlockAllCatalogName = "unlock_all";

        private IStoreController _storeController;

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            _storeController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"In-App Purchasing initialize failed: {error}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            //Retrieve the purchased product
            var product = purchaseEvent.purchasedProduct;

            if (product.definition.id == _noAdsCatalogName)
            {
                ApplyOnNoAds();
            }
            else if (product.definition.id == _unlockAllCatalogName)
            {
                ApplyUnlockAll();
            }

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");
            return PurchaseProcessingResult.Complete;
        }

        public void BuyNoAds()
        {
            _storeController.InitiatePurchase(_noAdsCatalogName);
        }

        public void BuyUnlockAll()
        {
            _storeController.InitiatePurchase(_unlockAllCatalogName);
        }

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            InitializeCatalogPurchase();
        }

        private void InitializeCatalogPurchase()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //Add products that will be purchasable and indicate its type.
            builder.AddProduct(_noAdsCatalogName, ProductType.NonConsumable);
            builder.AddProduct(_unlockAllCatalogName, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        private void ApplyOnNoAds()
        {
            PlayerPrefs.SetInt("NoAds", 1);
            GameManager.Instance.DisablePurchaseButtons();
        }

        private void ApplyUnlockAll()
        {
            PlayerPrefs.SetInt("UnlockedThemes", 9);
            PlayerPrefs.SetInt("UnlockedTubes", 2);
            PlayerPrefs.SetInt("NoAds", 1);

            GameManager.Instance.DisablePurchaseButtons();
        }
    }
}