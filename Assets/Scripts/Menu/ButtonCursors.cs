using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCursors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	bool defaultSelected = false;
	public GameObject cursor;
	EventSystem eventSystem;
	static ButtonCursors lastSelectedWithMouse;

	public void Awake()
	{
		eventSystem = EventSystem.current;
		if (defaultSelected)
		{
			eventSystem.SetSelectedGameObject(gameObject);
			cursor.SetActive(true);
		}
		else
			cursor.SetActive(false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (lastSelectedWithMouse != null && lastSelectedWithMouse != this)
		{
			lastSelectedWithMouse.cursor.SetActive(false);
		}
		cursor.SetActive(true);
		lastSelectedWithMouse = this;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}
}
