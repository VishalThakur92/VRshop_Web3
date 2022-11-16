using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace VRshop_Web3
{
    public class LoadABInScene : MonoBehaviour
    {

        [SerializeField]
        string assetBundleURL, abName;

        [SerializeField]
        Vector3 spawnLocation;


        //string url = "http://dl.dropboxusercontent.com/s/62ofroh0ke91m07/chair?dl=0", tableURL = "http://dl.dropboxusercontent.com/s/d1ojas2kj0zbp3f/table?dl=0";
        //chair - https://www.dropbox.com/s/62ofroh0ke91m07/chair?dl=0
        //table - https://www.dropbox.com/s/d1ojas2kj0zbp3f/table?dl=0

        private void Start()
        {
            _ =LoadAssetBundleAsync(assetBundleURL, abName);
        }

        //Download and show this product's AssetBundle
        async Task LoadAssetBundleAsync(string url , string abName)
        {
            //Download AB and show it in the scene
            AssetBundle remoteAB = await Utility.DownloadAssetBundle(url);
            GameObject spawnedABObj = Instantiate(remoteAB.LoadAsset(abName)) as GameObject;
            spawnedABObj.transform.position = spawnLocation;
            remoteAB.Unload(false);
        }

    }
}
