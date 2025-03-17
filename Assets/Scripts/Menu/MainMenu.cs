using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	private GameObject	fade;
	private EventSystem eventSystem;
	
	private AudioController audioController;

	private void Awake()
	{
		audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
		fade = GameObject.Find("Fade");
		if (fade != null)
		{
			if (SceneManager.GetActiveScene().name == "Menu")
				fade.SetActive(false);
			else
				fade.SetActive(true);
		}
	}

	public void PlayGame()
	{
		audioController.m_Effect1.loop = false;
		audioController.Music.loop = false;
		audioController.PlayMusic(audioController.Musics[1]);
		fade.SetActive(true);
		StartCoroutine(m_PlayGame());
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void OptionsMenu()
	{
		SceneManager.LoadScene("OptionsMenu");
	}

	public void CreditMenu()
	{
		SceneManager.LoadScene("CreditsMenu");
	}

	public void BackButton()
	{
		SceneManager.LoadScene("Menu");
	}

	private IEnumerator m_PlayGame()
	{
		fade.GetComponent<CanvasGroup>().alpha = 1;
		yield return new WaitForSeconds(15);
		audioController.PlayEffect1(audioController.notification);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
