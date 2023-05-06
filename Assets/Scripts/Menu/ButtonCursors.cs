using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCursors : MonoBehaviour
{
	[SerializeField]
	GameObject cursor;
	Button button;
	EventSystem eventSystem;

	private void Awake()
	{
		button = GetComponent<Button>();
		eventSystem = EventSystem.current;
	}

	void Update()
	{
		if (eventSystem.currentSelectedGameObject == gameObject)
			cursor.SetActive(true);
		else
			cursor.SetActive(false);
		
	}
}
