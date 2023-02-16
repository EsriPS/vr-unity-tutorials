# Scene 3 - Interaction

There are a few changes to this Scene that are worth reviewing before we dive into how interaction can be useful for experience.

## Scene Updates

* Management - This Game Object has a Script Component (StateManager.cs) attached. Take a moment to review this script. 
* ArcGISMap - We have a new Game Object that is acting as a "parent" to the XR rig set up in the previous Scene.
* RightHand Contoller (XR Ray Interactor) - Force Grab was turned off to give us a different experience when grabbing a building.
* Elevation Controller - New Input Action to handle moving up/down using the right controller.

## Walkthrough

* On the ArcGISMap Component, ensure "Mesh Colliders Enabled" is checked.
* Add the UrgentCare FBX from the Assets/Resources/Models folder under the ArcGISMap Game Object.
* Add an ArcGIS Location Component to this new Game Object.
    * Update the values as follows:
        * Latitude: 41.3260946
        * Longitude: -72.9108982
        * Altitude: 0
        * Spatial Reference WKID: 4326
* Add a Box Collider to the Urgent Care.
    * Edit Vertices to match general shape of the Urgent Care building.
* Add Rigid Body
    * The default options will have gravity enabled by default. This should cause the building to fall as soon as the Scene starts.
* Remove Gravity
    * The building will now fly away whenver the terrain is rendered with a Mesh Collider.
* Set "Is Kinematic" value on the RigidBody to True; i.e. check it.
    * This building should not fly away now.
* Add the XR Grab Interactable
    * Note the GrabTarget set for the Attach Transform property.
    * There are 2 main issues with our default setup:
        * Rotation - As we move the right joystrick to rotate the building, the XR rig is also rotating because of the Continuouus Turn Providers.
        * Gravity - Since we are using Kinematic to ensrue the building does not fly away when the Scene starts, the building is stuck where we leave it. 
* Fix the Rotation
    * Hook up the HandleGrabEnter method from the StateManager to the XR Grab Interactable.
* Fix Gravity
    * Hook up the HandleGrabExit method from the StateManager to the XR Grab Interactable.

