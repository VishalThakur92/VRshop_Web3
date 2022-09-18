using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Utility
{
    public static async Task<Texture2D> DownloadTexture(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri, true))
        {
            AsyncOperation asyncOp = uwr.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                if (!Application.isPlaying) return null;
                // Debug.Log($"{asyncOp.progress*100}% {uri}");
                await Task.Delay(1000 / 60);
            }

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{uwr.error}: {uri}");
                return Texture2D.redTexture;
            }
            else
            {
                // Debug.Log($"Texture Downloaded {uri}");
                var tex = DownloadHandlerTexture.GetContent(uwr);
                if (tex == null) tex = Texture2D.redTexture;
                return tex;
            }
        }
    }


    public static async Task<AssetBundle> DownloadAssetBundle(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(uri))
        {
            AsyncOperation asyncOp = uwr.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                if (!Application.isPlaying) return null;
                // Debug.Log($"{asyncOp.progress*100}% {uri}");
                await Task.Delay(1000 / 60);
            }

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{uwr.error}: {uri}");
                return null;
            }
            else
            {
                return DownloadHandlerAssetBundle.GetContent(uwr);
            }
        }
    }
}
