using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraFollower : MonoBehaviour
{
    [SerializeField] InputActionReference locomotionIA;
    [SerializeField] InputActionReference rotationIA;

    public Transform cameraTransform;
    public float Camera_Min_Delta_Pos = 0.1f; //minimum change in camera pos triggering UI update
    public float Camera_Min_Delta_Rot = 0.1f; //minimum change in camera rot triggering UI update
    public float UI_Offset_Forward = 2f; //radius
    public float UI_Offset_Down = 1f; //distance below eyes

    // Watch camera position
    Vector3 cameraPos = new Vector3(0f, 0f, 0f);

    // Watch camera rotation - UI does not adjust to Y rot of camera as this would occlude line of sight downward
    Vector3 prevCameraXZRot = new Vector3(0f, 0f, 0f);
    Vector3 cameraXZRot;
    Vector3 xzRotOffset;
    Vector3 prevOffset = new Vector3(0f, 0f, 0f);
    bool updateLookAt;

    void Update()
    {
        // Setup
        cameraXZRot.Set(cameraTransform.forward.x, 0, cameraTransform.forward.z);

        //Prevent jitter from adjusting to tiny movements
        if ((cameraTransform.position - cameraPos).magnitude >= Camera_Min_Delta_Pos)
        {
            cameraPos = cameraTransform.position;
        }

        // Prevent jitter from adjusting to tiny rotations
        if ((cameraXZRot - prevCameraXZRot).magnitude >= Camera_Min_Delta_Rot)
        {
            prevCameraXZRot = cameraXZRot;
            xzRotOffset = cameraXZRot;
            /*
             * Keeps the UI at consistent distance from player,
             * otherwise camera X and Z rotation affect UI pos
             */
            xzRotOffset.Normalize();
            xzRotOffset *= UI_Offset_Forward;

            updateLookAt = true;
            prevOffset = xzRotOffset;
        }
        else
        {
            xzRotOffset = prevOffset;
            updateLookAt = false;
        }

        // Could this be moved to the top of the Update method?
        // Also, should we be updating the UI visually (e.g. transparency) when either of these flags are true?
        if (locomotionIA.action.IsInProgress() || rotationIA.action.IsInProgress())
        {
            Debug.Log("Ignore Update on Locomotion Move/Rotation");
            return;
        }

        Vector3 newPos = new Vector3(cameraPos.x + xzRotOffset.x, cameraPos.y - UI_Offset_Down, cameraPos.z + xzRotOffset.z);

        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, 1.0f * Time.deltaTime);
        this.transform.LookAt(Camera.main.transform);
    }
}