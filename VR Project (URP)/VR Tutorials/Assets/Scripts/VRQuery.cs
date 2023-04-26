using System.Collections;
using System.Collections.Generic;
using Esri.ArcGISMapsSDK.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRQuery : MonoBehaviour
{
    [SerializeField] private InputActionReference _triggerAction;

    [SerializeField] private ArcGISMapComponent _agsMap;

    private XRRayInteractor _rayInteractor;

    void Start() 
    {
        // Get Access to XR Ray Interactor to Improve Grab Events
        var rhGo = GameObject.Find("RightHand Controller");
        _rayInteractor = rhGo.GetComponent<XRRayInteractor>();
    }


    void OnEnable()
    {
        _triggerAction.action.performed += TriggerPressed;
    }

    void TriggerPressed(InputAction.CallbackContext obj)
    {
        if (_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log($"Hit: {hit.transform.gameObject.name}");

            var arcGISRaycastHit = _agsMap.GetArcGISRaycastHit(hit);

            Debug.Log($"ArcGISRaycastHit: {arcGISRaycastHit}");

            var layer      = arcGISRaycastHit.layer;
			var featureId  = arcGISRaycastHit.featureId;
            var featureIdx = arcGISRaycastHit.featureIndex;

            Debug.Log($"Layer: {layer} - Index {featureIdx} - FeatureId: {featureId}");
        }
    }
}
