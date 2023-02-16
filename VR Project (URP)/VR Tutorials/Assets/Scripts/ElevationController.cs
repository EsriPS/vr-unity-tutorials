using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ElevationController : MonoBehaviour
{
    public float MoveSpeed = 10.0f;

    [SerializeField] InputActionReference _elevation;
    private Vector2 _elevationValue;
    private InputAction.CallbackContext _ctx;

    private void Update()
    {

        // This avoids a "corkscrew" effect when the player turns right or left. 
        // Essentially, we only want to turn or go up, not a mixture of the two.
        if (Mathf.Abs(_elevationValue.y) <= Mathf.Abs(_elevationValue.x))
            return;

        var yAxis = _elevationValue.y * MoveSpeed;
        Vector3 up = StateManager.Instance.PlayerXRO.transform.TransformDirection(Vector3.up);
        StateManager.Instance.PlayerXRO.transform.position += up * yAxis;
    }

    void OnEnable()
    {
        _elevation.action.performed += GetElevation;
        _elevation.action.canceled  += UpDownCanceled;
    }

    void UpDownCanceled(InputAction.CallbackContext obj)
    {
        _elevationValue = Vector2.zero;
    }

    void GetElevation(InputAction.CallbackContext obj)
    {
        _elevationValue = obj.ReadValue<Vector2>();
    }


}
