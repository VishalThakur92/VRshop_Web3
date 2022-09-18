using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    #region Parameters
    //URL to JSON located in Cloud
    string productsDataURL = "https://kings-guardians.com/KingsGaurdiansAndroidAssets/test/products.json";


    //The Array of Products as loaded from Cloud JSON
    [SerializeField]
    ProductRoot productsData;

    [SerializeField]
    bool productsLoadedSuccessfully= false;


    //----Related to Product UI--------------
    //Container holding all product UI elements
    [Space(10),SerializeField]
    GameObject productsUIContainer;


    //product UI element prefab
    [SerializeField]
    ProductUIElement productUIPrefab;
    #endregion


    #region Core
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Show Loading UI


        //Download Products data from JSON located in cloud
        StartCoroutine(LoadProductsDataFromCloud(productsDataURL));

        yield return new WaitWhile(() => !productsLoadedSuccessfully);

        //Hide Loading UI and show products over the UI
        StartCoroutine(ShowProductsOverUI());

    }



    //TODO replace it with async-op
    IEnumerator LoadProductsDataFromCloud(string url) {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.error == null)
        {
            Debug.Log("Successfully loaded products JSON : " + request.downloadHandler.text);
            productsData = ProductRoot.CreateFromJSON(request.downloadHandler.text);
            productsLoadedSuccessfully = true;
        }
        else
        {
            productsLoadedSuccessfully = false;
            Debug.LogError("Error loading products JSON : " + request.error);
        }
    }

    IEnumerator ShowProductsOverUI() {

        for (int i = 0; i < productsData.products.Length; i++)
        {
            ProductUIElement newProductUIElement = Instantiate(productUIPrefab , productsUIContainer.transform);
            newProductUIElement.Initialize(productsData.products[i]);
            yield return null;
        }
    }
    #endregion


}