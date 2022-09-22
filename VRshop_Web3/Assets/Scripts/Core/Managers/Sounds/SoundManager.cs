using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRshop_Web3
{
    public class SoundManager : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
