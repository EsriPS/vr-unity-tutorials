using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Unity.Mathematics;
using Esri.HPFramework;
using Esri.GameEngine.Geometry;

public class Minimap : MonoBehaviour
{

    /* origin coords for testing
     * 
     * -72.9108982
     * 41.3260946
     * 10
     */

    public ArcGISMapComponent map; //to get origin coordinates
    /* Adjust for demo purposes */
    public int scale = 1;

    public List<double3> locations; //starts with locs from FS
    private List<GameObject> markers; //starts empty

    public GameObject markerPrefab;

    void Start()
    {
        locations.Add(new double3(-72.9108982, 41.3260946, 10));
        locations.Add(new double3(-72.9115539701574, 41.3254874554075, 20));
        CreateMinimap();
    }

    #region Math

    Vector3 WorldToMinimap(double3 loc)
    {
        /*
         * Translation matrix - treats the lat/lon/alt position of map origin as (0,0,0),
         * will transform other locations into that local space
         * double4x4 object necessary for the ArcGISPoint properties
         */
        double4x4 DMinimapMatrix = new double4x4
            (1, 0, 0, (map.OriginPosition.X * 1000 * -1),
            0, 1, 0, (map.OriginPosition.Z * -0.1 ),
            0, 0, 1, (map.OriginPosition.Y * 1000 * -1),
            0, 0, 0, 1);
        Matrix4x4 MinimapMatrix = DMinimapMatrix.ToMatrix4x4();

        double3 DViewpointPos = new double3(loc.x * 1000, loc.z * 0.1, loc.y * 1000);
        //double3 DViewpointPos = loc;
        //DViewpointPos *= scale;
        Vector3 ViewpointPos = DViewpointPos.ToVector3();

        /*
         * Returns scaled position relative to map origin
         */
        return MinimapMatrix.MultiplyPoint(ViewpointPos);
    }

    #endregion

    #region Minimap Methods
    public void CreateMinimap()
    {
        for(int i = 0; i < locations.Count; i+= 1)
        {
            AddMarker(i, false);
        }
    }



    public void AddMarker(int index, bool addedByUser)
    {
        double3 loc = locations[index];
        Vector3 markerPos = WorldToMinimap(loc);
        Debug.Log(markerPos);
        GameObject newMarker = Instantiate(markerPrefab, this.transform);

        newMarker.transform.position = markerPos;
        newMarker.GetComponent<MinimapMarker>().addedByUser = addedByUser;
        //markers.Add(newMarker);
    }

    public void RemoveMarker()
    {

    }

    #endregion


    //called from markers
    public void OnSelectMarker(int index)
    {
        StateManager.Instance.SetPlayerLocation(Convert.ToSingle(locations[index].x), Convert.ToSingle(locations[index].y), Convert.ToSingle(locations[index].z));
    }
}
