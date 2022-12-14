using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace VRshop_Web3
{
    public class ShopManager : MonoBehaviour, IInteractable
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
        Text productNameText, productDescriptionText, productPriceText, currentFilterText;

        [SerializeField]
        Button PurchaseButton;

        [SerializeField]
        Image productImage;


        //The Array of Products as loaded from Cloud JSON
        [SerializeField]
        ProductRoot productsData;

        [SerializeField]
        string currentFilter;


        //----Related to Product UI--------------
        //Container holding all product UI elements
        [Space(10), SerializeField]
        GameObject productsUIContainer;

        //product UI element prefab
        [SerializeField]
        ProductUIElement productUIPrefab;

        bool productsLoadedSuccessfully = false;
        
        //All Product UI elements in shop
        List<ProductUIElement> allProducts = new List<ProductUIElement>();

        #endregion

        #region Core


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            Data.Events.OnProductRepositionStart += OnProductMoveStart;
            Data.Events.OnProductRepositionEnd += OnProductMoveEnd;

            Data.Events.OnProductPlaySpecialStart += OnProductMoveStart;
            Data.Events.OnProductPlaySpecialEnd += OnProductMoveEnd;

            Data.Events.OnProductFilterSelected += OnProductFilterSelected;


        }


        void Start()
        {
            StartCoroutine(RefreshProducts());
        }

        void OnDestroy()
        {
            Data.Events.OnProductRepositionStart -= OnProductMoveStart;
            Data.Events.OnProductRepositionEnd -= OnProductMoveEnd;

            Data.Events.OnProductPlaySpecialStart -= OnProductMoveStart;
            Data.Events.OnProductPlaySpecialEnd -= OnProductMoveEnd;

            Data.Events.OnProductFilterSelected -= OnProductFilterSelected;

        }



        IEnumerator RefreshProducts()
        {
            //Download Products data from JSON located in cloud
            StartCoroutine(LoadProductsDataFromCloud(Data.productsDataURL));

            yield return new WaitWhile(() => !productsLoadedSuccessfully);

            //Hide Loading UI and show products over the UI
            StartCoroutine(ShowProductsOverUI());
        }

        //TODO replace it with async-op
        IEnumerator LoadProductsDataFromCloud(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.error == null)
            {
                Debug.Log("Successfully loaded products JSON : \n\n" + request.downloadHandler.text);
                productsData = ProductRoot.CreateFromJSON(request.downloadHandler.text);
                productsLoadedSuccessfully = true;
            }
            else
            {
                productsLoadedSuccessfully = false;
                Debug.LogError("Error loading products JSON : " + request.error);
            }
        }

        IEnumerator ShowProductsOverUI()
        {
            int nbChildren = productsUIContainer.transform.childCount;

            for (int i = nbChildren - 1; i >= 0; i--)
            {
                yield return null;
                DestroyImmediate(productsUIContainer.transform.GetChild(i).gameObject);
            }

            allProducts.Clear();

            for (int i = 0; i < productsData.products.Length; i++)
            {
                ProductUIElement newProductUIElement = Instantiate(productUIPrefab, productsUIContainer.transform);
                newProductUIElement.Initialize(productsData.products[i]);
                allProducts.Add(newProductUIElement);
                yield return null;
            }
        }


        public void OnProductFilterSelected(string category)
        {
            //avoid selection of same filter
            if (currentFilter == category)
                return;

            currentFilter = category;
            currentFilterText.text = category;
            ProductUIElement firstProductOfNewCategory = null;

            for (int i = 0; i < allProducts.Count; i++) {
                //If this product belongs to the selected category, show it
                if (string.Equals(allProducts[i].productInfo.category, category))
                {
                    allProducts[i].gameObject.SetActive(true);
                    if (firstProductOfNewCategory == null)
                        firstProductOfNewCategory = allProducts[i];
                }
                else//If this product does not belongs to the selected category, hide it
                    allProducts[i].gameObject.SetActive(false);
            }

            //Show the newly selected category Product
            firstProductOfNewCategory.OnSelected();
        }
        public void OnPointerEnter()
        {
            //hover start behaviour
            shopMesh.transform.localScale = new Vector3(.9f, .9f, .9f);
        }

        public void OnPointerExit()
        {
            //hover end behaviour
            shopMesh.transform.localScale = new Vector3(.6f, .6f, .6f);

        }

        public void OnPointerClick()
        {
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

        public void OnProductSelected(Product selectedProduct)
        {

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
            else
            {
                productNameText.text = currentProductInfo.name;
                productPriceText.text = "$" + currentProductInfo.price;
            }

            PurchaseButton.gameObject.SetActive(!currentProductInfo.isPurchased);
            productDescriptionText.text = currentProductInfo.description;
        }


        public void OnProductPurchased()
        {
            currentProductInfo.isPurchased = true;
            productNameText.text = currentProductInfo.name + " (Downloading)";
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
            productNameText.text = currentProductInfo.name + " (Purchased)";

            await Task.Delay(500);
            OnShopExit();
            Data.Events.OnProductPurchased.Invoke();
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
}
