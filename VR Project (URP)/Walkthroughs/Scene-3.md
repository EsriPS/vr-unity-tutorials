# Scene 3 - Interaction

This file will discuss how Scene 3 was built. 

* On the ArcGISMap Component, ensure "Mesh Colliders Enabled" is checked.
* Add the UrgentCare FBX from the Assets/Resources/Models folder under the ArcGISMap Game Object.
* Add an ***ArcGIS Location Component*** to this new Game Object.
    * Update the values as follows:
        * Latitude: 41.3260946
        * Longitude: -72.9108982
        * Altitude: 0
        * Spatial Reference WKID: 4326
* Add a Box Collider to the Urgent Care.
    * Edit Vertices to match general shape of the Urgent Care.
* Add Rigid Body
    * The default will have gravity enabled by default. This will cause the building to fall as soon as the Scene starts.
* Remove Gravity
    * The building will now fly away.
