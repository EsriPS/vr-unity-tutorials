using System.Collections;


using UnityEngine;
using UnityEngine.Networking;

public class FeatureService
{
    private string _serviceURL;

    public FeatureService(string serviceURL)
    {
        _serviceURL = serviceURL;
    }

    public IEnumerator RequestFeatures(string whereClause, ResponseHandler respHandler, GameObject prefab)
    {
        WWWForm form = new WWWForm();
        form.AddField("where", whereClause);
        form.AddField("outFields", "*");
        form.AddField("outSR", "4326");
        form.AddField("f", "json");

        var queryURL = $"{_serviceURL}/query";

        using (UnityWebRequest www = UnityWebRequest.Post(queryURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                yield return respHandler(www.downloadHandler.text, prefab);
            }
        }
    }

    public delegate IEnumerator ResponseHandler(string responseText, GameObject prefab);
}