using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

struct markerState
{
    public Color color;
    public Vector3 scale;

    public markerState(Color c, Vector3 s)
    {
		color = c;
        scale = s;
    }
}

public class MinimapMarker : MonoBehaviour
{
    public TextMeshPro number;
    public GameObject cube;
	public bool addedByUser = false; //set by Minimap.cs

    public enum mode { idle, hover, active };

    Dictionary<mode, markerState> markerStates = new Dictionary<mode, markerState>()
    {
        { mode.idle, new markerState(new Color(1,1,1), new Vector3(0,0,0)) },
        { mode.hover, new markerState(new Color(0.349f, 0.839f, 1), new Vector3(-0.025f, -0.025f, -0.025f)) },
        { mode.active, new markerState(new Color(0,0.602f,0.945f), new Vector3(-0.035f, -0.035f, -0.035f)) }
    };

	public void UpdateMarker(mode initialMode, mode targetMode)
	{
		//frontPlateImg.color = Color.Lerp(buttonStates[initialMode].bgColor, buttonStates[targetMode].bgColor, 1f);
		//icon.color = Color.Lerp(buttonStates[initialMode].fgColor, buttonStates[targetMode].fgColor, 1f);
		//txt.color = Color.Lerp(buttonStates[initialMode].fgColor, buttonStates[targetMode].fgColor, 1f);
		//fpTransform.anchoredPosition3D = basePos + buttonStates[targetMode].offset;
	}

	public void hoverEnter()
	{
		UpdateMarker(mode.idle, mode.hover);
	}

	public void hoverExit()
	{
		UpdateMarker(mode.hover, mode.idle);
	}

	public void selectEnter()
	{
		UpdateMarker(mode.hover, mode.active);
	}

	public void selectExit()
	{
		UpdateMarker(mode.active, mode.idle);
	}

	public void SetText(string newText)
	{
		number.text = newText;
	}
}
