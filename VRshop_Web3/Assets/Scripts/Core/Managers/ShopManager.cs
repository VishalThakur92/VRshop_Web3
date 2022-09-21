using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour,IInteractable
{
    #region Parameters
    public static ShopManager Instance { get; private set; }
    [SerializeField]
    GameObject shopMesh;

    [SerializeField]
    GameObject shopCanvas;

    [SerializeField]
    Text currentProductCategoryText;

    //Currently selected Product
    Product currentProductInfo;


    [SerializeField]
    Text productNameText, productDescriptionText, productPriceText;

    [SerializeField]
    Button PurchaseButton;

    [SerializeField]
    Image productImage;
    #endregion

    #region Core
    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        Data.DataEvents.OnProductRepositionStart += OnProductMoveStart;
        Data.DataEvents.OnProductRepositionEnd += OnProductMoveEnd;
    }

    void OnDestroy() {
        Data.DataEvents.OnProductRepositionStart -= OnProductMoveStart;
        Data.DataEvents.OnProductRepositionEnd -= OnProductMoveEnd;
    }

    public void OnPointerEnter() {
        //hover start behaviour
        shopMesh.transform.localScale = new Vector3(.9f , .9f, .9f);
    }

    public void OnPointerExit(){
        //hover end behaviour
        shopMesh.transform.localScale = new Vector3(.6f, .6f, .6f);

    }

    public void OnPointerClick() {
        //On Selected Behaviour
        OnShopEnter();
    }

    void OnShopEnter()
    {
        GetComponent<BoxCollider>().enabled = false;
        shopMesh.SetActive(false);
        shopCanvas.SetActive(true);
    }
    public void OnShopExit()
    {
        GetComponent<BoxCollider>().enabled = true;
        shopMesh.SetActive(true);
        shopCanvas.SetActive(false);
    }

    public void OnProductSelected(Product selectedProduct) {

        //do nothing if same product is selected
        if (currentProductInfo?.uniqueId == selectedProduct.uniqueId)
            return;

        //A new product is selected save it's info 
        currentProductInfo = selectedProduct;

        //show its info
        _ = LoadIconAsync(currentProductInfo.iconImageURL);


        //Show product as purchased
        if (currentProductInfo.isPurchased)
        {
            productNameText.text = currentProductInfo.name + " (Purchased)";
            productPriceText.text = string.Empty;
        }
        //show buying info
        else {
            productNameText.text = currentProductInfo.name;
            productPriceText.text = "$" + currentProductInfo.price;
        }

        PurchaseButton.gameObject.SetActive(!currentProductInfo.isPurchased);
        productDescriptionText.text= currentProductInfo.description;
    }


    public void OnProductPurchased()
    {
        currentProductInfo.isPurchased = true;
        productNameText.text = currentProductInfo.name + " (Downloading..)";
        productPriceText.text = string.Empty;
        PurchaseButton.gameObject.SetActive(!currentProductInfo.isPurchased);
        _ = LoadAssetBundleAsync(currentProductInfo.assetBundleURL);
    }


    //Download and show this product's AssetBundle
    async Task LoadAssetBundleAsync(string url)
    {
        //Download AB and show it in the scene
        AssetBundle remoteAB = await Utility.DownloadAssetBundle(currentProductInfo.assetBundleURL);
        GameObject spawnedABObj = Instantiate(remoteAB.LoadAsset(currentProductInfo.name)) as GameObject;
        spawnedABObj.transform.position = new Vector3(0, 0.1f, 2.19f);
        remoteAB.Unload(false);

        await Task.Delay(1000);

        productNameText.text = currentProductInfo.name + " (Purchased)";

        await Task.Delay(1000);
        OnShopExit();
        Data.DataEvents.OnProductPurchased.Invoke();
    }

    //Download and show this product's Icon Image
    async Task LoadIconAsync(string url)
    {
        Texture2D texture2D = await Utility.DownloadTexture(url);
        Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);
        productImage.sprite = Sprite.Create(texture2D, rec, new Vector2(0.5f, 0.5f), 100);
    }

    void OnProductMoveStart(GameObject product)
    {
        OnShopExit();
        DisableShop();
    }
    void OnProductMoveEnd()
    {
        EnableShop();
    }

    void DisableShop()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    void EnableShop()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    #endregion
}
