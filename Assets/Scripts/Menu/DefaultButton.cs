using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButton : MonoBehaviour
{
	[SerializeField]
	GameObject defaultButton;
	void Start()
	{
		if (!defaultButton)
			EventSystem.current.SetSelectedGameObject(defaultButton);
	}
}
