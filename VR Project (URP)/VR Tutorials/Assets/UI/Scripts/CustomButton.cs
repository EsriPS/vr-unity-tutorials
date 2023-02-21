using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

struct buttonState
{
	public Color bgColor;
	public Color fgColor;
	public Vector3 offset;

	public buttonState(Color bg, Color fg, Vector3 position)
    {
		bgColor = bg;
		fgColor = fg;
		offset = position;
    }
}

namespace ESAVR
{
	public class CustomButton : MonoBehaviour
	{
		public Image icon;
		public TextMeshProUGUI txt;
		public RectTransform fpTransform;
		public Image frontPlateImg;

		Vector3 basePos;

		public enum mode { idle, hover, active };

		Dictionary<mode, buttonState> buttonStates = new Dictionary<mode, buttonState>()
	{
		{ mode.idle, new buttonState(new Color(0.5f, 0.5f, 0.5f, 0f), new Color(1,1,1), new Vector3(0,0,0)) },
		{ mode.hover, new buttonState(new Color(0.5f, 0.5f, 0.5f, 0.15f), new Color(0.349f, 0.839f, 1), new Vector3(-0.025f, -0.025f, -0.025f)) },
		{ mode.active, new buttonState(new Color(0.5f, 0.5f, 0.5f, 0.25f), new Color(0,0.602f,0.945f), new Vector3(-0.035f, -0.035f, -0.035f)) }
	};

		// Start is called before the first frame update
		void Start()
        {
			basePos = fpTransform.anchoredPosition3D;
		}

		public void UpdateButton(mode initialMode, mode targetMode)
		{
			frontPlateImg.color = Color.Lerp(buttonStates[initialMode].bgColor, buttonStates[targetMode].bgColor, 1f);
			icon.color = Color.Lerp(buttonStates[initialMode].fgColor, buttonStates[targetMode].fgColor, 1f);
			txt.color = Color.Lerp(buttonStates[initialMode].fgColor, buttonStates[targetMode].fgColor, 1f);
			fpTransform.anchoredPosition3D = basePos + buttonStates[targetMode].offset;
		}

		public void hoverEnter()
		{
			UpdateButton(mode.idle, mode.hover);
		}

		public void hoverExit()
		{
			UpdateButton(mode.hover, mode.idle);
		}

		public void selectEnter()
		{
			UpdateButton(mode.hover, mode.active);
		}

		public void selectExit()
		{
			UpdateButton(mode.active, mode.idle);
		}

		public void SetText(string newText)
        {
			txt.text = newText;
        }
	}
}
