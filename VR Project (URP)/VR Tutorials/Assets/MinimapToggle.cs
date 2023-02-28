using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MinimapToggle : MonoBehaviour
{
    bool showMinimap = false;
    public GameObject minimap;
    XRRayInteractor LHRay;

    // Start is called before the first frame update
    void Start()
    {
        minimap.SetActive(false);
        LHRay = GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        //between 150 and 210
        if ((this.transform.localRotation.eulerAngles.z <= 210) && (this.transform.localRotation.eulerAngles.z >= 150))
        {
            if (!showMinimap)
            {
                Debug.Log("Setting active");
                minimap.SetActive(true);
                showMinimap = true;
                LHRay.enabled = false;
            }
        } else
        {
            if (showMinimap)
            {
                Debug.Log("Hiding");
                minimap.SetActive(false);
                showMinimap = false;
                LHRay.enabled = true;
            }
        }
    }
}
