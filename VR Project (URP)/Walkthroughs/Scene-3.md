# Scene 3 - Interaction

There are a few changes to this Scene that are worth reviewing before we dive into how interaction can be useful for our VR experience.

## Scene Updates

* Management - This Game Object has a Script Component (StateManager.cs) attached. Take a moment to review this script. 
* ArcGISMap - We have a new Game Object that is acting as a "parent" to the "XR Rig" set up in the previous Scene.
* RightHand Contoller (XR Ray Interactor) - Force Grab was turned off to give us a different experience when grabbing a building.
* Elevation Controller - New Input Action to handle moving up/down using the right controller.

## Walkthrough

* On the ArcGISMap Component, ensure "Mesh Colliders Enabled" is checked.
* Add the UrgentCareFull FBX from the Assets/Resources/Models folder under the ArcGISMap Game Object.
* Create a new Game Object under UrgentCareFull and named it "GrabTarget".
* Add an ArcGIS Location Component to UrgentCareFull Game Object.
    * Update the values as follows:
        * Latitude: 41.3260946
        * Longitude: -72.9108982
        * Altitude: 0
        * Spatial Reference WKID: 4326
    * Continue adjusting the location of the building so that it is on the ground and a bit way from the Environment Game Object.
* Add a Box Collider to UrgentCareFull.
    * Edit Vertices to match general shape of the UrgentCareFull building.
* Add Rigid Body
    * The default options will have gravity enabled by default. This should cause the building to fall as soon as the Scene starts.
* Start the Scene and confirm the building falls away.
* Remove Gravity by unchecking "Use Gravity" on the Rigid Body.
    * The building will now fly away whenver the terrain underneath is rendered with a Mesh Collider.
* Start the Scene and confirm the building is "bumped" upward.
* Set "Is Kinematic" value on the RigidBody to True; i.e. check it.
    * This building should not fly away now. You may start the Scene to confirm this is indeed what happens.
* Add the XR Grab Interactable
    * Connect the GrabTarget Game Object to the Attach Transform property.
    * Turn off "Throw on Detach". Or don't and have a bit of fun before moving forward.
    * Ensure "Force Gravity on Detach" is enabled.
* Start the Scene and observe the behavior by selecting the bulding with the trigger button on your controller.
    * There are several issues with our current setup:
        * Rotation - As we move the right joystrick to rotate the building, the XR rig is also rotating because of the Continuouus Turn Providers.
        * The building "shifts" to an unfortunate location (i.e. above the point where the thought we were selecting) each time we grab.
        * Gravity - Since we are using Kinematic to ensrue the building does not fly away when the Scene starts, the building is stuck where we leave it. 
* Fix the Rotation
    * Hook up the HandleGrabEnter method from the StateManager to the XR Grab Interactable (under Interactable Events) Select Entered section.
    * Drag the StateManager to the open slot created by hitting the "+" icon and then select the StateManager option to find the HandleGrabEnter method.
* Start the Scene and observe the changes.
    * Disable "Track Rotation" to observe another (potentially better) approach to this problem.
* Fix Gravity
    * Hook up the HandleGrabExit method from the StateManager to the XR Grab Interactable (under Interactive Events) Select Exited section.
* Start the Scene and observe the changes.

