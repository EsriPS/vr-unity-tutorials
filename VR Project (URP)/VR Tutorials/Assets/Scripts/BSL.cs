using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Layers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Esri.Unity;
using Esri.GameEngine;
using System;

public class BSL : MonoBehaviour
{
    private ArcGISBuildingSceneLayer _bsl;

    void Start()
    {
        var agsGO  = GameObject.Find("ArcGISMap");
        var agsMap = agsGO.GetComponent<ArcGISMapComponent>();

        _bsl = (ArcGISBuildingSceneLayer)agsMap.View.Map.Layers.At(0);

        _bsl.DoneLoading += ToggleLayers;         
    }

    private void ToggleLayers(Exception loadError)
    {
        var size = _bsl.Sublayers.GetSize();

        Debug.Log($"Target BSL: {_bsl.Name}");
        Debug.Log($"Done Loading Size: {size}");

        for (ulong i = 0; i < size; i++) {

            var sublayer = _bsl.Sublayers.At(i);

            sublayer.IsVisible = true;

            if (!sublayer.Name.Contains("Overview")){
                Debug.Log($"{sublayer.Name} - {sublayer.Sublayers.GetSize()}");
                for (ulong j = 0; j < sublayer.Sublayers.GetSize(); j++)
                {

                    var subsublayer = sublayer.Sublayers.At(j);

                    subsublayer.IsVisible = true;

                    Debug.Log($"{j} - {sublayer.Sublayers.At(j).Name}");

                }
            }
        }  
    }
}
