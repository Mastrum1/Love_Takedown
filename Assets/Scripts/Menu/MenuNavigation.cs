using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject creditsMenu;

	private GameObject lastSelectedButton;

	private void Start()
	{
		mainMenu.GetComponent<PanelController>().PanelStateChanged += OnMainMenuStateChanged;
		optionsMenu.GetComponent<PanelController>().PanelStateChanged += OnOptionsMenuStateChanged;
		creditsMenu.GetComponent<PanelController>().PanelStateChanged += OnCreditsMenuStateChanged;
	}

	void Update()
	{
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			lastSelectedButton = EventSystem.current.currentSelectedGameObject;
		}

		if (Input.GetButtonDown("Cancel"))
		{
			if (optionsMenu.activeSelf)
			{
				optionsMenu.SetActive(false);
				mainMenu.SetActive(true);
			}
			else if (creditsMenu.activeSelf)
			{
				creditsMenu.SetActive(false);
				mainMenu.SetActive(true);
			}
		}
	}

	void OnMainMenuEnable(bool isEnabled)
	{
		if (isEnabled)
		{
			StartCoroutine(SetSelectedButtonWithDelay(lastSelectedButton, 0.1f));
		}
	}

	void OnSubMenuDisable(bool isEnabled)
	{
		if (!isEnabled)
		{
			StartCoroutine(SetSelectedButtonWithDelay(lastSelectedButton, 0.1f));
		}
	}

	private IEnumerator SetSelectedButtonWithDelay(GameObject button, float delay)
	{
		// Attendre le délai spécifié
		yield return new WaitForSeconds(delay);

		// Définir le bouton sélectionné après le délai
		if (EventSystem.current != null && button != null)
		{
			EventSystem.current.SetSelectedGameObject(button);
		}
	}

	private void OnMainMenuStateChanged(bool isActive)
	{
		if (isActive)
		{
			StartCoroutine(SetSelectedButtonWithDelay(lastSelectedButton, 0.1f));
		}
	}

	private void OnOptionsMenuStateChanged(bool isActive)
	{
		if (!isActive)
		{
			StartCoroutine(SetSelectedButtonWithDelay(lastSelectedButton, 0.1f));
		}
	}

	private void OnCreditsMenuStateChanged(bool isActive)
	{
		if (!isActive)
		{
			StartCoroutine(SetSelectedButtonWithDelay(lastSelectedButton, 0.1f));
		}
	}

}
