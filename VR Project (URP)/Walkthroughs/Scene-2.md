# Scene 2 - ArcGIS Maps SDKs for Game Engines

This Scene will illustrate how you can use the ArcGIS Maps SDK for Unity to bring GIS context (e.g. elevation or spatial data) into your VR application.

## Building the Scene

* Open the Map Creator option under the ArcGIS Maps SDK tab on the Toolbar. 
* Go to the Auth tab and ensure there is an API Key present.
* Go to the Map tab and input the following:
    * Map Type: Local
    * Origin Position
        * Longitude: -72.9008807
        * Latitude: 41.3258219
        * Altitude: 95
        * Spatial Reference: 4326
* Select Create Map on the Map tab.
* Go to the Camera tab and copy the Longitude, Latitude, and Altitude values from the Map tab.
* Select Create Camnera on the Camera tab and close the ArcGIS Maps SDK plugin. 
* Notice how the Main Camera under the XR Origin was renamed to "ArcGISCamera" and moved under the ArcGISMap Game Object. Some functionality (e.g. ArcGIS Location Component) in the ArcGIS Maps SDK for Unity relies on content being a child of a Game Object with the ArcGIS Map Component. As a result, we will want reorganize the content in our Scene. 
* We will combine the XR Origin (and the associated children) Game Object and this new ArcGISMap Game Object. Reorganize the content in the Hierarchy to match the following:
```
ArcGISMap
    XR Origin
        Camera Offset
            ArcGISCamera
            LeftHand Controller
            RightHand Controller
```
* Start the Scene. 
* Open the ArcGIS Maps SDK tab again and go to the Basemap tab. 
    * Explore the other default Basemaps provided in the plugin.
    * Close the ArcGIS Maps SDK plugin.
* Go to the ArcGIS Map Component on the ArcGIS Map Game Object and find the Elevation section.
    * Copy the URL to the World Elevation Service. 
    * Delete the URL and navigate around the Scene to observe the differences. 
    * Add the URL back. 
* Go to the ArcGIS Map Component on the ArcGIS Map Game Object and find the Layers section. 
    * It should say "List is Empty".
    * Hit the "+" button, expand the Element that is generated, and then enter the following information:
        * Name: Building
        * Type: ArcGIS 3D Object Scene Layer
        * Source: https://tiles.arcgis.com/tiles/BteRGjYsGtVEXzaX/arcgis/rest/services/NewHaven/SceneServer
        * Opacity: 1
        * Is Visible: (Checked)
        * Authenticaiton: None
* Although this tutorial is not focused on data creation within a GIS, it is worth understanding how desktop software like ArcGIS Pro can be used to generate the building data we are consuming via services.
    * Consult the ArcGIS Pro [documentation](https://pro.arcgis.com/en/pro-app/latest/help/data/3d-objects/what-is-a-3d-object-feature-class-.htm) if you would like to learn more.  
* Repeat the process above with another 3D Object Scene Layer that comes from a completely different source. 
    * Source: https://tiles.arcgis.com/tiles/wQnFk5ouCfPzTlPw/arcgis/rest/services/NewBuilding/SceneServer

## Further Reading

We only covered some of the content that is supported in the ArcGIS Maps SDKs for Game Engines. Explore the following resources for more information. 

* [Supported Layer Types](https://developers.arcgis.com/unity/layers/#data-layers)
