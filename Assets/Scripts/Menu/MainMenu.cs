using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
	{
		GameObject.Find("AudioManager").GetComponent<AudioController>().Music.loop = false;
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().Musics[1]);
		StartCoroutine(m_PlayGame());
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	private IEnumerator m_PlayGame()
	{
		yield return new WaitForSeconds(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
