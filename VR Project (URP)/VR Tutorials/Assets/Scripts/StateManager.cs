using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Newtonsoft.Json.Linq;
using TMPro;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    [Header("General Inputs")]
    public Unity.XR.CoreUtils.XROrigin PlayerXRO;

    [Header("Feature Service Inputs")]
    [SerializeField] private string _artServiceURL;
    [SerializeField] private GameObject _artPrefab;
    [SerializeField] private string _treeServiceURL;
    [SerializeField] private GameObject _treePrefab;
    private GameObject _fsContainer;

    private GameObject _agsMap;
    private ArcGISLocationComponent _playerLocation;
    private ActionBasedContinuousTurnProvider _cTurnProvider;

    private void Start()
    {
        // Ensure XR Origin has Location that can be modified from the UI
        _playerLocation = PlayerXRO.gameObject.AddComponent<ArcGISLocationComponent>();

        // Get Access to the Turn Provider to help with FBX manipluation
        var lsGo = GameObject.Find("Locomotion System");
        _cTurnProvider = lsGo.GetComponent<ActionBasedContinuousTurnProvider>();

        // Find ArcGIS Map Game Object for Parenting Purposes
        _agsMap = GameObject.Find("ArcGISMap");

        // Get Reference to Feature Service Game Object
        _fsContainer = new GameObject("Feature Services");
        _fsContainer.transform.SetParent(_agsMap.transform);

        // Begin Loading Art POIs
        FeatureService artService = new FeatureService(_artServiceURL);
        StartCoroutine(artService.RequestFeatures("OBJECTID in (21, 36, 107, 57, 126, 54, 98, 97)", CreateArtFeatures, _artPrefab));
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void HandleGrabEnter(SelectEnterEventArgs args)
    {
        _cTurnProvider.enabled = false;
    }

    public void HandleGrabExit(SelectExitEventArgs args)
    {
        _cTurnProvider.enabled = true;

        var rb = args.interactableObject.transform.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    public void SetPlayerLocation(float lon, float lat, float alt)
    {
        _playerLocation.Position = new ArcGISPoint(lon, lat, alt, new ArcGISSpatialReference(4326));
    }

    public GameObject CreateMarker(string name, float lat, float lon, float alt, GameObject prefab)
    {
        GameObject locationMarker = Instantiate(prefab, _fsContainer.transform);

        locationMarker.name = name;

        ArcGISLocationComponent location = locationMarker.AddComponent<ArcGISLocationComponent>();
        location.Position = new ArcGISPoint(lon, lat, alt, ArcGISSpatialReference.WGS84());
        location.Rotation = new ArcGISRotation(0f, 90f, 0f);

        return locationMarker;
    }

    IEnumerator CreateArtFeatures(string data, GameObject prefab)
    {
        var results = JObject.Parse(data);
        var features = results["features"].Children();

        foreach (var feature in features)
        {
            var attributes = feature.SelectToken("attributes");
            var geometry = feature.SelectToken("geometry");

            var lat = (float)geometry.SelectToken("y");
            var lon = (float)geometry.SelectToken("x");

            var artist = attributes.SelectToken("ARTIST").ToString();
            var title = attributes.SelectToken("WORK_TITLE").ToString();

            var marker = CreateMarker(artist, lat, lon, 10, prefab);

            var text = marker.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText(title);

            yield return null;
        }
    }
}
