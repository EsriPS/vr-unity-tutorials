using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Layers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Esri.Unity;

public class BSL : MonoBehaviour
{
    void Start()
    {
        var agsGO  = GameObject.Find("ArcGISMap");
        var agsMap = agsGO.GetComponent<ArcGISMapComponent>();

        var bsl = (ArcGISBuildingSceneLayer)agsMap.View.Map.Layers.At(0);

        for (ulong i = 0; i < bsl.Sublayers.GetSize(); i++) {
            bsl.Sublayers.At(i).IsVisible = true;
        }                                 
    }
}
