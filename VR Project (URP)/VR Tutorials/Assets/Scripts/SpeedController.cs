using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class SpeedController : MonoBehaviour
{
	[SerializeField] InputActionReference rightHandGripAction;
	public float slowMoveUpDownSpeed = 0.1f;
	public float slowTurnSpeed = 10;
	[SerializeField] InputActionReference leftHandGripAction;
	public float slowMoveSpeed = 2;

	private InputAction.CallbackContext ctx;

	private float savedMoveSpeed = 50;
	private float savedMoveUpDownSpeed = 2;
	private float savedTurnSpeed = 50;

	void Start()
	{
		rightHandGripAction.action.performed += RightHandeGripPressed;
		rightHandGripAction.action.started += RightHandGripActionStarted;
		rightHandGripAction.action.canceled += RightHandGripActionCanceled;

		leftHandGripAction.action.performed += LeftHandGripPressed;
		leftHandGripAction.action.started += LeftHandGripActionStarted;
		leftHandGripAction.action.canceled += LeftHandGripActionCanceled;
	}

	private void LeftHandGripPressed(InputAction.CallbackContext obj)
	{
		ActionBasedContinuousMoveProvider continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
		if (continuousMoveProvider != null)
		{
			savedMoveSpeed = continuousMoveProvider.moveSpeed;
			continuousMoveProvider.moveSpeed = slowMoveSpeed;
		}
	}

	private void LeftHandGripActionStarted(InputAction.CallbackContext obj)
	{
	}

	private void LeftHandGripActionCanceled(InputAction.CallbackContext obj)
	{
		ActionBasedContinuousMoveProvider continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
		if (continuousMoveProvider != null)
		{
			continuousMoveProvider.moveSpeed = savedMoveSpeed;
		}
	}

	private void RightHandeGripPressed(InputAction.CallbackContext obj)
	{
		ActionBasedContinuousTurnProvider continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
		if (continuousTurnProvider != null)
		{
			savedTurnSpeed = continuousTurnProvider.turnSpeed;
			continuousTurnProvider.turnSpeed = slowTurnSpeed;
		}

		ElevationController upDownController = GetComponent<ElevationController>();
		if (upDownController != null)
		{
			savedMoveUpDownSpeed = upDownController.MoveSpeed;
			upDownController.MoveSpeed = slowMoveUpDownSpeed;
		}
	}

	private void RightHandGripActionStarted(InputAction.CallbackContext obj)
	{
	}

	private void RightHandGripActionCanceled(InputAction.CallbackContext obj)
	{
		ActionBasedContinuousTurnProvider continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
		if (continuousTurnProvider != null)
		{
			continuousTurnProvider.turnSpeed = savedTurnSpeed;
		}
		ElevationController upDownController = GetComponent<ElevationController>();
		if (upDownController != null)
		{
			upDownController.MoveSpeed = savedMoveUpDownSpeed;
		}
	}

}