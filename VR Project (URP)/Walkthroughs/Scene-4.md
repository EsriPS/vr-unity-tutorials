# Adding a Camera-centric Menu

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
   - Back has no components
   - Front has HLayoutGroup for icon and text, needs to be a raycast target
   - Parent has Simple Interactable / CustomButton script to hydrate icon, text and animate front plate, + XR Interactable hooked up to those functions
