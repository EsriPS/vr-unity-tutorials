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
    public TextMeshPro numberText;
    public GameObject cube;
	private Renderer cubeRenderer;
	public bool addedByUser = false; //set by Minimap.cs
	public int locIndex; //set by Minimap.cs

    public enum mode { idle, hover, active };

    Dictionary<mode, markerState> markerStates = new Dictionary<mode, markerState>()
    {
        { mode.idle, new markerState(new Color(1,1,1), new Vector3(1,1,1)) },
        { mode.hover, new markerState(new Color(0.257f, 0.812f, 0.945f), new Vector3(1.1f, 1.1f, 1.1f)) },
        { mode.active, new markerState(new Color(0,0.825f,0.887f), new Vector3(1.3f, 1.3f, 1.3f)) }
    };

    private void Start()
    {
		cubeRenderer = cube.GetComponent<Renderer>();
		SetText(locIndex.ToString());
    }

    public void UpdateMarker(mode initialMode, mode targetMode)
	{
		cubeRenderer.material.SetColor("_BaseColor", markerStates[targetMode].color);
		cubeRenderer.material.SetColor("_EmissionColor", markerStates[targetMode].color);

		cube.transform.localScale = markerStates[targetMode].scale;
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
		this.transform.parent.GetComponent<Minimap>().OnSelectMarker(locIndex);
	}

	public void selectExit()
	{
		UpdateMarker(mode.active, mode.idle);
	}

	public void SetText(string newText)
	{
		numberText.text = newText;
	}
}
