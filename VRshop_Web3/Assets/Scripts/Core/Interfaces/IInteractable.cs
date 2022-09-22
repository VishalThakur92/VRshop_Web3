using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRshop_Web3
{
    public interface IInteractable
    {
        public void OnPointerEnter();
        public void OnPointerExit();
        public void OnPointerClick();

    }
}
