# Scene 1 - XR Interaction Toolkit

This Scene illustrates how you can turn a basic Scene in Unity into a VR experience with the XR Interaction Toolkit.

## Initial Configuration

Most of the setup for the XR Interaction Toolkit has already been configured with this project. If you want to explore where a number of the settings adjusted during the initial configuration, you can look at the following places. 

* Package Manager - XR Interaction Toolkit
* Projecting Settings
    * XR Plugin-in Management
        * Interaction Profiles - Oculus Touch Controller Profile
    * Preset Manager
* Assets/Samples/XR Interaction Toolkit/2.2.0/Starter Assets

You can also visit the [XR Interaction Toolkit documentation](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/general-setup.html) for information on basic setup.

## Building the Scene

* Right-click within the Hierarchy and select XR -> XR Origin (VR).
    * Note that a Game Object named XR Interaction Manager is created automatically.
* Expand the XR Origin Game Object that was created and explore the children, as well as the various Comnponents assigned to those children.
* Ensure that the Tracking Origin Mode on the XR Origin Component (on the XR Origin Game Object) is set to Floor. 
* Adjust the XR Origin so that the Main Camera (under the XR Origin Game Object) is looking at the Environment Game Object.
* Start the Scene. 
    * You should be able to look around the Scene and notice that there are basic XR Ray Interactors coming out of your "hands".
* Right-click in the Hierarchy and select XR -> Locomotion System (Action Based). 
    * With the Locomotion System Game Object selected, go the Inspector and do the following:
        * Turn off Teleportation Provider
        * Turn off Snap Turn Provider (Action-based)
        * Add the Continuous Move Provider (Action-based)
            * Disable Use Reference for Right Hand Move Action.
        * Add the Continuous Turn Provider (Action-based)
            * Disable Use Referene for Left Hand Turn Action.
* Expand the XR Origin Game Object in the Hierarchy and find the LeftHand Controller Game Object. 
    * Go to Assets/Resources/Oculus and locate quest2_controllers_dv0.
    * Drag quest2_controllers_dv0 underneath the LeftHand Controller Game Object.
    * Expand quest2_controllers_dv0 and then select both of the children starting with "right_quest_*" and disable them in the Inspector.
    * Repeat this process on the RightHand Controller and unselect the "left_quest2_*" children. 
* Start the Scene. 
    * You should now have controller models associated with each hand and the ability to move around the Scene using the controllers.
