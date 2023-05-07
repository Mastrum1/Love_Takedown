using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCursors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField]
	bool					defaultSelected = false;
	public GameObject		cursor;
	EventSystem				eventSystem;
	static ButtonCursors	lastSelectedWithMouse;

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

	public void Update()
	{
		if (eventSystem.currentSelectedGameObject == null && lastSelectedWithMouse == this)
			eventSystem.SetSelectedGameObject(gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (lastSelectedWithMouse != null && lastSelectedWithMouse != this)
			lastSelectedWithMouse.cursor.SetActive(false);
		cursor.SetActive(true);
		lastSelectedWithMouse = this;
		eventSystem.SetSelectedGameObject(gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log("Button selected: " + gameObject.name);
		if (lastSelectedWithMouse && lastSelectedWithMouse != this)
			lastSelectedWithMouse.cursor.SetActive(false);
		cursor.SetActive(true);
		lastSelectedWithMouse = this;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Debug.Log("Button deselected: " + gameObject.name);
		if (eventSystem.currentSelectedGameObject != gameObject)
			cursor.SetActive(false);
	}
}
