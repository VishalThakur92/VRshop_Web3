using UnityEngine;
using UnityEngine.UI;

namespace VRshop_Web3
{
    public class AppManager : MonoBehaviour{

        #region Parameters
        //Singleton instance
        public static AppManager Instance { get; private set; }

        //Moralis Wallet ID of logged in User
        [SerializeField]
        Text UserIDText;
        #endregion


        #region Core
        void Awake()
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        void Start()
        {
            //Show user ID/wallet address
            UserIDText.text = Data.userWalletAddress;
        }
        #endregion
    }
}
