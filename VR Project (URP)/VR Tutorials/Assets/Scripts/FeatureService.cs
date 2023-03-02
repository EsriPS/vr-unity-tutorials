using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Mathematics;
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

    public IEnumerator WriteFeature(Dictionary<string, Dictionary<string, object>> feature)
    {
        WWWForm form = new WWWForm();
        form.AddField("adds", JsonConvert.SerializeObject(feature));
        //form.AddField("adds", $"{{\"geometry\": {{\"x\": {feature.x}, \"y\": {feature.y}}}}}");

        form.AddField("f", "json");

        var applyEditsURL = $"{_serviceURL}/applyEdits";

        using (UnityWebRequest www = UnityWebRequest.Post(applyEditsURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var response = JObject.Parse(www.downloadHandler.text);
                var results = response["addResults"].Children();

                foreach (var result in results)
                {
                    var success = bool.Parse(result.SelectToken("success").ToString());
                    var oid = long.Parse(result.SelectToken("objectId").ToString());

                    if (success)
                        Debug.Log("Success");
                }

                yield return null;
            }
        }
    }

    public delegate IEnumerator ResponseHandler(string responseText, GameObject prefab);
}