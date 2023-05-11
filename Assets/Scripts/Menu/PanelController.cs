using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PanelController : MonoBehaviour
{
	public event Action<bool> PanelStateChanged;

	private void OnEnable()
	{
		PanelStateChanged?.Invoke(true);
	}

	private void OnDisable()
	{
		PanelStateChanged?.Invoke(false);
	}
}
