# Scene 2 - xxxxxx

There are a few ways that content is created for use in the Maps SDK. In this section, we'll explore supported layer types, adding layers to scenes, and creating new content layers for scenes.

## Supported Layer Types
There are three formats that the Maps SDK currently supports:
* Image Tile Layer - Data layers that can access and display raster (continuous) information, typically used for basemaps.
* Vector Tile Layer - Data layers that store a vector representation of data instead of raster.
* Scene Layer - Data layers that are optimized for displaying large amounts of 3D data in a scene (I3S format compliant).
    * 3D Object Scene Layer - Contains features such as exterior shells od buildings that are modeled in 3D, created from multipatch, and visualized with textures.
    * Integrated Mesh Scene Layer - Textured continuous mesh collected from site scans or drone imagery containing 3D objects and elevation.

## Scene Updates

* Management - This Game Object has a Script Component (StateManager.cs) attached. Take a moment to review this script. 
* ArcGISMap - We have a new Game Object that is acting as a "parent" to the XR rig set up in the previous Scene.
* RightHand Contoller (XR Ray Interactor) - Force Grab was turned off to give us a different experience when grabbing a building.
* Elevation Controller - New Input Action to handle moving up/down using the right controller.

## Walkthrough
### Adding a Scene Layer
* On the ArcGISMap Component, inspect the Layers section.
* 
* Add the UrgentCare FBX from the Assets/Resources/Models folder under the ArcGISMap Game Object.
* Add an ArcGIS Location Component to this new Game Object.
    * Update the values as follows:
        * Latitude: 41.3260946
        * Longitude: -72.9108982
        * Altitude: 0
        * Spatial Reference WKID: 4326
* Add a Box Collider to the Urgent Care.
    * Edit Vertices to match general shape of the Urgent Care building.
### Changing the Basemap

### A Note on Elevation Layers

While changing the elevation layer service isn't a step in our walkthrough, it is worth mentioning for future interest. 

The elevation component in the Maps SDK can point to custom elevation layer services. As an example, elevation information stored in drone-collected data can be extracted and published as a hosted elevation service. By grabbing the REST url as we've done for scene layers, we can insert the url in the Maps SDK inspector to have a custom elevation layer in our application.
