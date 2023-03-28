using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Newtonsoft.Json.Linq;
using TMPro;
using Esri.HPFramework;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    [Header("General Inputs")]
    public Unity.XR.CoreUtils.XROrigin PlayerXRO;
    [SerializeField] InputActionReference _rotationInput;

    [Header("Feature Service Inputs")]
    [SerializeField] private string _artServiceURL;
    [SerializeField] private GameObject _artPrefab;
    //todo: sub this for marker prefab
    //[SerializeField] private string _treeServiceURL;
    //[SerializeField] private GameObject _treePrefab;
    private GameObject _fsContainer;

    private GameObject _agsMap;
    private ArcGISLocationComponent _playerLocation;
    private ActionBasedContinuousTurnProvider _cTurnProvider;
    private XRRayInteractor _rayInteractor;
    private bool _grabbing = false;
    private Vector2 _rotation;
    private GameObject _grabTarget;

    private Dictionary<string, List<GameObject>> _scenarios = new Dictionary<string, List<GameObject>>();

    private FeatureService viewpointService;

    private void OnEnable()
    {
        _rotationInput.action.performed += ReadRotation;
    }

    #region Monobehavior Lifecylce
    private void Start()
    {
        // Ensure XR Origin has Location that can be modified from the UI
        _playerLocation = PlayerXRO.gameObject.AddComponent<ArcGISLocationComponent>();

        // Get Access to the Turn Provider to help with FBX manipluation
        var lsGo = GameObject.Find("Locomotion System");
        _cTurnProvider = lsGo.GetComponent<ActionBasedContinuousTurnProvider>();

        // Get Access to XR Ray Interactor to Improve Grab Events
        var rhGo = GameObject.Find("RightHand Controller");
        _rayInteractor = rhGo.GetComponent<XRRayInteractor>();

        // Find ArcGIS Map Game Object for Parenting Purposes
        _agsMap = GameObject.Find("ArcGISMap");

        // Get Reference to Feature Service Game Object
        _fsContainer = new GameObject("Feature Services");
        _fsContainer.transform.SetParent(_agsMap.transform);

        // Begin Loading Art POIs
        FeatureService artService = new FeatureService(_artServiceURL);
        StartCoroutine(artService.RequestFeatures("OBJECTID in (21, 36, 107, 57, 126, 54, 98, 97)", CreateArtFeatures, _artPrefab));

        viewpointService = new FeatureService(Minimap.Instance._viewpointServiceURL);
        StartCoroutine(viewpointService.RequestFeatures("1=1", CreateViewpointFeatures, Minimap.Instance.markerPrefab));
    }

    private void Update()
    {
        if (_grabbing & _rotationInput.action.inProgress)
        {
            var adjustment = _rotation.y > 0 ? 5f : -5f;
            _grabTarget.transform.Rotate(new Vector3(0f, adjustment, 0f));
        }
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

    #endregion

    #region UI Methods
    private void ReadRotation(InputAction.CallbackContext obj)
    {
        _rotation = obj.ReadValue<Vector2>();
    }

    public void HandleGrabEnter(SelectEnterEventArgs args)
    {
        _grabTarget = args.interactableObject.transform.gameObject;
        _grabbing = true;

        _cTurnProvider.rightHandTurnAction.reference.action.Disable();

        if (_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            var grabTarget = hit.transform.Find("GrabTarget").gameObject;
            grabTarget.transform.position = hit.point;
        }
    }

    public void HandleGrabExit(SelectExitEventArgs args)
    {
        _grabbing = false;

        _cTurnProvider.rightHandTurnAction.reference.action.Enable();

        var rb = args.interactableObject.transform.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    public void RegisterScenario(string name, List<GameObject> elements)
    {
        _scenarios.Add(name, elements);
    }

    public void ToggleScenario(string name)
    {
        foreach (var scenario in _scenarios)
        {
            foreach (GameObject obj in scenario.Value)
            {
                obj.SetActive(scenario.Key == name);
            }
        }
    }

    public void SetPlayerLocation(float lon, float lat, float alt)
    {
        _playerLocation.Position = new ArcGISPoint(lon, lat, alt, new ArcGISSpatialReference(4326));
    }

    #endregion

    #region Feature Service Methods
    public GameObject CreateMarker(string name, float lat, float lon, float alt, GameObject prefab)
    {
        GameObject locationMarker = Instantiate(prefab, _fsContainer.transform);

        locationMarker.name = name;

        ArcGISLocationComponent location = locationMarker.AddComponent<ArcGISLocationComponent>();
        location.Position = new ArcGISPoint(lon, lat, alt, ArcGISSpatialReference.WGS84());
        location.Rotation = new ArcGISRotation(0f, 90f, 0f);

        return locationMarker;
    }

    IEnumerator CreateViewpointFeatures(string data, GameObject prefab)
    {
        var results = JObject.Parse(data);
        var features = results["features"].Children();

        foreach (var feature in features)
        {
            var geometry = feature.SelectToken("geometry");

            var lon = (double)geometry.SelectToken("x");
            var lat = (double)geometry.SelectToken("y");
            var alt = 10;

            int newIndex = Minimap.Instance.locations.Count;
            Minimap.Instance.locations.Add(new double3(lon, lat, alt));
            Minimap.Instance.AddMarker(newIndex, false);
            yield return null;
        }
    }

    public void WriteMiniMarker(double3 loc)
    {
        // Set up fields
        var attributes = new Dictionary<string, object>(){ };
        var spatialRef = new JObject();
        spatialRef.Add("wkid", 4326);
        var geometry = new Dictionary<string, object>()
        {
            { "x", loc.x },
            { "y", loc.y },
            { "spatialReference", spatialRef  }
        };

        //Create dictionary for serialization
        var feature = new Dictionary<string, Dictionary<string, object>>()
        {
            { "attributes", attributes},
            {"geometry", geometry }
        };

        // POST
        StartCoroutine(viewpointService.WriteFeature(feature));
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

    #endregion
}
