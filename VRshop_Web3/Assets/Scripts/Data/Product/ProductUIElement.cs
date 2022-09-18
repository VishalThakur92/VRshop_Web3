using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProductUIElement : MonoBehaviour
{
    [SerializeField]
    Text name_Text;
    [SerializeField]
    Text description_Text;

    [SerializeField]
    Image iconImage;

    [SerializeField]
    string iconImageURL;
    [SerializeField]
    string assetBundleURL;

    Product productInfo;


    public void Initialize(Product product) {
        //store product info for ref
        productInfo = product;

        //load all Text elements with product info we got
        name_Text.text = productInfo.name;
        //description_Text.text = productInfo.description;
        _ = LoadIconAsync(productInfo.iconImageURL);
    }


    //Download and show this product's Icon Image
    async Task LoadIconAsync(string url)
    {
        Texture2D texture2D = await Utility.DownloadTexture(url);
        Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);
        iconImage.sprite = Sprite.Create(texture2D, rec, new Vector2(0.5f, 0.5f), 100);
    }


    //Download Asset Bundle from Cloud
    IEnumerator LoadAssetBundle(string url)
    {
        yield return null;
    }

    public static implicit operator ProductUIElement(Type v)
    {
        throw new NotImplementedException();
    }

    public void OnSelected() { 
        //Show this product's info on the panel
    }

    public void OnPurchased() { 
        //Download AB and show it in the scene
    }
}
