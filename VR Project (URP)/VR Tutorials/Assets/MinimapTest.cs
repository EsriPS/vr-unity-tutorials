using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Esri.ArcGISMapsSDK.Components;
using Unity.Mathematics;
using Esri.HPFramework;

public class MinimapTest : MonoBehaviour
{
    public ArcGISMapComponent map; //to get origin coordinates

    /* 
     * Test assumes "viewpoint" already exists in the scene as a GO.
     * Full implementation will read marker coordinates in from a FS instead of 
     * grabbing position from location components.
     */
    public ArcGISLocationComponent viewpoint;

    /* Adjust for demo purposes */
    public int scale = 1000;

    Vector3 WorldToMinimap()
    {
        /*
         * Translation matrix - treats the lat/lon/alt position of map origin as (0,0,0),
         * will transform other locations into that local space
         * double4x4 object necessary for the ArcGISPoint properties
         */
        double4x4 DMinimapMatrix = new double4x4
            (1, 0, 0, (map.OriginPosition.X * scale * -1),
            0, 1, 0, (map.OriginPosition.Y * scale * -1),
            0, 0, 1, (map.OriginPosition.Z * scale * -1),
            0, 0, 0, 1);
        Matrix4x4 MinimapMatrix = DMinimapMatrix.ToMatrix4x4();

        double3 DViewpointPos = new double3(viewpoint.Position.X, viewpoint.Position.Y, viewpoint.Position.Z);
        DViewpointPos *= scale;
        Vector3 ViewpointPos = DViewpointPos.ToVector3();

        /*
         * Returns scaled position relative to map origin
         */
        return MinimapMatrix.MultiplyPoint(ViewpointPos);
    }

    void Update()
    {
        Debug.Log(WorldToMinimap());
    }
}
