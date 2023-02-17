# Adding a World-space Player Menu
TODO:
- Review CustomButton and CameraFollower code

## Resources
- `UI/Fonts` for SDFs of some open source fonts - TODO: find tutorial link
- `UI/Sprites` for icons from calcite-meridian library, + panel background

## (Unsorted) Tips
- 2D mode in the editor window
- Optimization: disable RaycastTarget
- Optimization: Canvas Renderer / Cull Transparent Mesh

## Structure
- CustomButton
   - Back vs front panels - satisfying to take advantage of 3D ui even for flat design, skeumorphism
   - Text
      - Overflow: overflow
      - Wrap: disabled - we want the button to grow horizontally instead of vertically, this is just personal preference and depends on how wide the total UI is
      - ContentSizeFitter: this is somewhat of a bad practice and you'll see the warning, but we need to set it to Preferred Size or the bounding box won't resize to fit the overflow text
      - It's ok because we aren't using the HLGs to control sizing of child elements, so no undefined fighting behavior
   - Backplate 
      - ignores auto layout and uses rect transform stretch
   - Front plate
      - needs a content size fitter also because of the text
      - Layout Group
      - Raycast target
   - Prefab parent
      - Size Fitter
      - Layout Group, only contains one element but we need it for the size fitter to calculate desired min size based on the size of the child elements
      - Layout Element, to lock the height in
      - XR Interactable hooks into CustomButton
      - CustomButton script, used to animate front plane position + color changes.
- ModifiedPanel uses the same principles

## Steps
1. Add `MinimalMenu` prefab to the Hierarchy in Scene 4
2. Change Render Mode on Canvas Component to World Space + connect the ArcGISCamera
3. RectTransform values will be huge- change pos to (0, -0.6, 0.5) and rot to (12,180,0)
4. Add `TrackedDeviceGraphicRaycaster` and `CameraFollower` components
5. On CameraFollower add input actions: XRI LH Locomotion/Move and XRI RightHand Locomotion/Turn, and camera transform