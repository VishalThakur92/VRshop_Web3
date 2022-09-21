using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Data
{
    #region Moralis
    public static string userWalletAddress = "Guest";
    #endregion


    #region Product Interaction Events
    public struct DataEvents
    {

        public static UnityAction<GameObject> OnProductRepositionStart;
        public static UnityAction OnProductRepositionEnd;
        public static UnityAction OnProductPurchased;
    }
    #endregion
}
