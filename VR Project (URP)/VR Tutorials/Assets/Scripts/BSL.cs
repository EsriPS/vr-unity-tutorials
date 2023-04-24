using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Layers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSL : MonoBehaviour
{
    [SerializeField] private ArcGISMapComponent _agsMap;

    void Start()
    {
        var totalLayers = _agsMap.Layers.Count;

        //for (var i = 0; i < totalLayers; i++)
        //{
        //    var layer = _agsMap.View.Map.Layers[i];
        //}
    }
}
