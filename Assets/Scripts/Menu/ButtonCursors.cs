using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCursors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	public GameObject cursor;
	EventSystem eventSystem;
	static ButtonCursors lastSelectedWithMouse;

	[SerializeField] public int buttonIndex;

	public void Awake()
	{
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(gameObject);
		cursor.SetActive(true);
	}

	public void Update()
	{
		if (!eventSystem.currentSelectedGameObject && lastSelectedWithMouse == this)
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
		if (lastSelectedWithMouse && lastSelectedWithMouse != this)
			lastSelectedWithMouse.cursor.SetActive(false);
		cursor.SetActive(true);
		lastSelectedWithMouse = this;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (eventSystem.currentSelectedGameObject != gameObject)
			cursor.SetActive(false);
	}
}