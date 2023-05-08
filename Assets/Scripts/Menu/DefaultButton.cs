using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButton : MonoBehaviour
{
	[SerializeField]
	GameObject[] menuButtons;

	void Start()
	{
		if (MenuState.Instance != null && menuButtons != null)
		{
			EventSystem.current.SetSelectedGameObject(menuButtons[MenuState.Instance.LastSelectedButtonIndex]);
		}
		else if (menuButtons != null)
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
				MenuState.Instance.LastSelectedButtonIndex = i;
				Debug.Log("LastSelectedButtonIndex: " + i);
				break;
			}
		}
	}
}