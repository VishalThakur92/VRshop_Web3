using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    string productsDataURL = "https://kings-guardians.com/KingsGaurdiansAndroidAssets/test/products.json";


    [SerializeField]
    ProductRoot productsData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadProductsDataFromCloud(productsDataURL));
    }



    //TODO replace it with async-op
    IEnumerator LoadProductsDataFromCloud(string url) {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.error == null)
        {
            Debug.Log("Successfully loaded products JSON : " + request.downloadHandler.text);
            productsData = ProductRoot.CreateFromJSON(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error loading products JSON : " + request.error);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
