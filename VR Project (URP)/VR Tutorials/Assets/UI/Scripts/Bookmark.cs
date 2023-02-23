using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bookmark : MonoBehaviour
{
    [Header("Location")]
    [SerializeField] float _lon;
    [SerializeField] float _lat;
    [SerializeField] float _alt;

    public void Select(ActivateEventArgs args)
    {
        StateManager.Instance.SetPlayerLocation(_lon, _lat, _alt);
    }
}
