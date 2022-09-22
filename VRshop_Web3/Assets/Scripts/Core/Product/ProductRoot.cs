// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;
using UnityEngine;
namespace VRshop_Web3
{
    [System.Serializable]
    public class ProductRoot
    {
        public Product[] products;

        public static ProductRoot CreateFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<ProductRoot>(jsonString);
            }
            catch (UnityException e)
            {
                Debug.LogError("Error parsing JSON " + e);
                return null;
            }
        }
    }

    [System.Serializable]
    public class Product
    {
        public string uniqueId;
        public string name;
        public string description;
        public string iconImageURL;
        public string assetBundleURL;
        public int price;
        public bool isPurchased;
        public string category;
    }
}