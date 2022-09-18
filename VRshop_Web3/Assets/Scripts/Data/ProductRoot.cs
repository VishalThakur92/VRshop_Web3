// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;
using UnityEngine;

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
        catch (UnityException e) {
            Debug.LogError("Error parsing JSON " + e);
            return null;
        }
    }
}

[System.Serializable]
public struct Product
{
    public string uniqueId;
    public string name;
    public string description;
    public string iconImageURL;
    public string assetBundleURL;
}



//public class Contact
//{
//    public int Id;
//    public string Name;
//    //public DateTime BirthDate;
//    public string Phone;
//    public Address Address;
//}