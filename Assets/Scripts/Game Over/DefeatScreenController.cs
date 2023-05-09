using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefeatScreenController : MonoBehaviour
{
	public Image defeatImage;
	public Image fadePanel;
	public float fadeDuration = 3f;

	void Start()
	{
        GameObject.Find("AudioManager").GetComponent<AudioController>().m_Effect3.Stop();
        defeatImage.gameObject.SetActive(true);
		StartCoroutine(HandleDefeatScreen());
	}

	IEnumerator HandleDefeatScreen()
	{
		yield return new WaitForSeconds(6f);

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
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().Musics[0]);
        SceneManager.LoadScene("Menu");
	}
}
