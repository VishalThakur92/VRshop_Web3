using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRshop_Web3 {
    public class ProductFilter : MonoBehaviour
    {
        //filer type
        public string category;

        public void OnSelected() {
            Data.Events.OnProductFilterSelected.Invoke(category);
        }
    }
}