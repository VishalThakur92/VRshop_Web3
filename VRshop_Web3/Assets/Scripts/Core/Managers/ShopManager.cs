using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour,IInteractable
{
    #region Parameters
    [SerializeField]
    GameObject shopMesh;

    [SerializeField]
    GameObject shopCanvas;

    [SerializeField]
    Text currentProductCategoryText;
    #endregion

    #region Core
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

    public void OnProductPurchased() { 
    
    }
    #endregion
}
