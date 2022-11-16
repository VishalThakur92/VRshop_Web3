using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace VRshop_Web3
{
    public struct Data
    {
        #region Moralis
        //Logged in User's wallet ID
        public static string userWalletAddress = "Guest";

        //Products info JSON Cloud URL
        //oneDrive - http://dl.dropboxusercontent.com
        //public static string productsDataURL = "https://kings-guardians.com/KingsGaurdiansAndroidAssets/test/products.json";
        public static string productsDataURL = "http://dl.dropboxusercontent.com/s/d1ojas2kj0zbp3f/table?dl=0";

        //https://www.dropbox.com/s/d1ojas2kj0zbp3f/table?dl=0
        #endregion


        #region Events
        public struct Events
        {
            public static UnityAction<GameObject> OnProductRepositionStart;
            public static UnityAction OnProductRepositionEnd;
            public static UnityAction OnProductPurchased;
            public static UnityAction<GameObject> OnProductPlaySpecialStart;
            public static UnityAction OnProductPlaySpecialEnd;
            public static UnityAction<string> OnProductFilterSelected;
        }
        #endregion
    }
}