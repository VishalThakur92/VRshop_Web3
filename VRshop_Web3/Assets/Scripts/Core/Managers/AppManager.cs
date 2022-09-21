using MoralisUnity.Kits.AuthenticationKit;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{

    #region Parameters
    [SerializeField]
    string state;
    //Singleton instance
    public static AppManager Instance { get; private set; }

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
    // Start is called before the first frame update
    void Start()
    {
        //Show user ID/wallet address
        UserIDText.text = Data.userWalletAddress;
    }

    public void OnStateChange(AuthenticationKitState val)
    {
        state = val.ToString();

    }
    #endregion


}
