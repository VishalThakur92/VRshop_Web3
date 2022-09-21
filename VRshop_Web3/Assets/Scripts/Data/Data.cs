using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Data
{
    #region Moralis
    public static string userWalletAddress = "Guest";

    //URL to JSON located in Cloud
    public static string productsDataURL = "https://kings-guardians.com/KingsGaurdiansAndroidAssets/test/products.json";
    #endregion


    #region Product Interaction Events
    public struct DataEvents
    {

        public static UnityAction<GameObject> OnProductRepositionStart;
        public static UnityAction OnProductRepositionEnd;
        public static UnityAction OnProductPurchased;
        public static UnityAction<GameObject> OnProductPlaySpecialStart;
        public static UnityAction OnProductPlaySpecialEnd;
    }
    #endregion
}
