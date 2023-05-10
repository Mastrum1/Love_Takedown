using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButton : MonoBehaviour
{
	[SerializeField]
	GameObject[] menuButtons;

	static int lastIndex = 0;

	void Start()
	{
		if (MenuState.Instance != null && menuButtons != null)
		{
			ButtonCursors[] allButtons = FindObjectsOfType<ButtonCursors>();
			foreach (ButtonCursors button in allButtons)
			{
				if (button.buttonIndex == MenuState.Instance.LastSelectedButtonIndex)
				{
					EventSystem.current.SetSelectedGameObject(button.gameObject);
					break;
				}
			}
		}
		else if (menuButtons != null && menuButtons.Length > 0)
		{
			EventSystem.current.SetSelectedGameObject(menuButtons[0]);
		}
	}

	void OnDestroy()
	{
		for (int i = 0; i < menuButtons.Length; i++)
		{
			if (EventSystem.current.currentSelectedGameObject == menuButtons[i])
			{
				lastIndex = i;
				break;
			}
		}
	}
}