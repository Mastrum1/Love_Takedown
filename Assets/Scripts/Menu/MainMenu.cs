using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	GameObject fade;

    private void Awake()
    {
		fade = GameObject.Find("Fade");
        fade.SetActive(false);
    }
    public void PlayGame()
	{
		GameObject.Find("AudioManager").GetComponent<AudioController>().Music.loop = false;
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().Musics[1]);
        fade.SetActive(true);
        StartCoroutine(m_PlayGame());
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	private IEnumerator m_PlayGame()
	{
		fade.GetComponent<CanvasGroup>().alpha = 1;
        yield return new WaitForSeconds(15);
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect1(GameObject.Find("AudioManager").GetComponent<AudioController>().notification);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
