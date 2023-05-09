using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreenController : MonoBehaviour
{
	public Image VictoryImage;
	public Image fadePanel;
	public float fadeDuration = 5f;

	void Start()
	{
		VictoryImage.gameObject.SetActive(true);
		StartCoroutine(HandleVictoryScreen());
	}

	IEnumerator HandleVictoryScreen()
	{
		yield return new WaitForSeconds(10f);

		float timer = 0f;
		Color fadeColor = fadePanel.color;
		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			fadeColor.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
			fadePanel.color = fadeColor;
			yield return null;
		}
        GameObject.Find("AudioManager").GetComponent<AudioController>().Music.loop = true;
		GameObject.Find("AudioManager").GetComponent<AudioController>().m_Effect1.Stop();
        GameObject.Find("AudioManager").GetComponent<AudioController>().m_Effect3.Stop();
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().Musics[0]);
        SceneManager.LoadScene("Menu");
	}
}
