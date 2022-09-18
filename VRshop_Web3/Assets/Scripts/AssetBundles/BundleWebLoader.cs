using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BundleWebLoader : MonoBehaviour
{
    public string bundleUrlLocal;
    //public string bundleUrlCloud = Application.streamingAssetsPath + "";
    public string assetName = "BundledObject";
    GameObject spawnedABObj;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        bundleUrlLocal = Application.streamingAssetsPath + "/"  + assetName;

        using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrlLocal))
        {
            yield return web.SendWebRequest();
            AssetBundle remoteAssetBundle = DownloadHandlerAssetBundle.GetContent(web);
            if (remoteAssetBundle == null)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }
            spawnedABObj = Instantiate(remoteAssetBundle.LoadAsset(assetName))as GameObject;
            spawnedABObj.transform.position = new Vector3(-0.186f, 0, 3.04f);
            remoteAssetBundle.Unload(false);
        }
    }


}