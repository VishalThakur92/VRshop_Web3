using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoralisUnity.Kits.AuthenticationKit;
using UnityEngine.SceneManagement;
using WalletConnectSharp.Unity;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    Image fadeOutImage;

    [SerializeField]
    GameObject ProjectNamePanel;

    [SerializeField]
    Text statusText , qrCodeStatusText;

    [SerializeField]
    Button connectWalletButton, guestLoginButton;

    public void OnGuestLogin() {
        //Show Fadeout and load Main scene
        StartCoroutine(FadeOutToMainScene());
    }

    public void OnWalletConnected()
    {
        Debug.LogError("Wallet Connected");
        //Hide Project Name Panel
        ProjectNamePanel.SetActive(true);
        //Save player info to Globals
        if(!string.IsNullOrEmpty(WalletConnect.Instance.Session.Accounts[0]))
            Data.userWalletAddress = WalletConnect.Instance.Session.Accounts[0];

        //Show Fadeout and load Main scene
        StartCoroutine(FadeOutToMainScene());
    }

    public void OnWalletDisconnected()
    {
        //Reset Login to intial phase
        ProjectNamePanel.SetActive(true);
        guestLoginButton.gameObject.SetActive(true);
        connectWalletButton.gameObject.SetActive(true);
    }

    public void OnMoralisStatusChanged(AuthenticationKitState state) {
        Debug.LogError(state);
        statusText.text = state.ToString();
        //show status in Status text
        switch (state)
        {

            case AuthenticationKitState.WalletConnecting:
                ProjectNamePanel.SetActive(false);
                guestLoginButton.gameObject.SetActive(false);
                break;
            case AuthenticationKitState.WalletSigning:
                qrCodeStatusText.text = "Confirm your wallet";
                ProjectNamePanel.SetActive(true);
                break;
            case AuthenticationKitState.MoralisLoggingIn:
                OnWalletConnected();
                break;
        }
    }

    IEnumerator FadeOutToMainScene() {

        Debug.LogError("Fade out");
        //Fade out 
        float startTime = Time.time;
        while (Time.time - startTime < 2f)
        {
            fadeOutImage.color = Color.Lerp(fadeOutImage.color , Color.white, Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();
        }

        //Load Main Menu scene
        Debug.LogError("Load Main Scene");
        SceneManager.LoadScene("Main");
    }
}
