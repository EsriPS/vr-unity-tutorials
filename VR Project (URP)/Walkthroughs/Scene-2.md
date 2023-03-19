# Scene 2 - xxxxxx

There are a few ways that content is created for use in the Maps SDK. In this section, we'll explore supported layer types, adding layers to scenes, and creating new content layers for scenes.

## Supported Layer Types
There are three formats that the Maps SDK currently supports:
* Image Tile Layer - Data layers that can access and display raster (continuous) information, typically used for basemaps.
* Vector Tile Layer - Data layers that store a vector representation of data instead of raster.
* Scene Layer - Data layers that are optimized for displaying large amounts of 3D data in a scene (I3S format compliant).
    * 3D Object Scene Layer - Contains features such as exterior shells od buildings that are modeled in 3D, created from multipatch, and visualized with textures.
    * Integrated Mesh Scene Layer - Textured continuous mesh collected from site scans or drone imagery containing 3D objects and elevation.

## Scene Updates (TO-DO)

* Management - This Game Object has a Script Component (StateManager.cs) attached. Take a moment to review this script. 
* ArcGISMap - We have a new Game Object that is acting as a "parent" to the XR rig set up in the previous Scene.
* RightHand Contoller (XR Ray Interactor) - Force Grab was turned off to give us a different experience when grabbing a building.
* Elevation Controller - New Input Action to handle moving up/down using the right controller.

## Walkthrough
### Adding a Scene Layer
* In the ArcGISMap Component inspector, open the Layers section. The list reads '0', meaning we have no GIS data layers in the scene at the moment.
* Click the '+' in the 'List is Empty' box to add a layer element.
* In the 'Element 0' box, update the values as follows:
        * Name: New Haven Buildings
        * Type: ArcGIS 3DObject Scene Layer
        * Source: ' rest url here '
        * Opacity: 0
        * Is Visible: checked
        * Authentication: None

###

### Changing the Basemap

### A Note on Elevation Layers

While changing the elevation layer service isn't a step in our walkthrough, it is worth mentioning for future interest. 

The elevation component in the Maps SDK can point to custom elevation layer services. As an example, elevation information stored in drone-collected data can be extracted and published as a hosted elevation service. By grabbing the REST url as we've done for scene layers, we can insert the url in the Maps SDK inspector to have a custom elevation layer in our application.
