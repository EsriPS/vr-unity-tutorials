using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private ActionBasedContinuousTurnProvider _cTurnProvider;

    private void Start()
    {
        var lsGo = GameObject.Find("Locomotion System");
        _cTurnProvider = lsGo.GetComponent<ActionBasedContinuousTurnProvider>();
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void HandleGrabEnter(SelectEnterEventArgs args)
    {
        _cTurnProvider.enabled = false;
    }

    public void HandleGrabExit(SelectExitEventArgs args)
    {
        _cTurnProvider.enabled = true;

        var rb = args.interactableObject.transform.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
