using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
	private static AudioController instance;

	[Range(0f,1f)]
	public float volume = 1f;

	public AudioClip[] Musics;
	public AudioClip[] Reacts;
	public AudioClip[] Damage1;
	public AudioClip[] Damage2;
	public AudioClip[] Coeur1;
	public AudioClip[] Coeur2;
	public AudioClip[] Ennemy;
	public AudioClip[] Car;
	public AudioClip[] endSound;
	public AudioClip lamp;
	public AudioClip notification;
	public AudioClip takedown;


	public AudioSource Music;
	public AudioSource m_Effect1;
	private AudioSource m_Effect2;
    private AudioSource m_Effect3;
    private AudioSource[] SoundsAudio;
	private float timer;

	private void Awake()
	{
		SoundsAudio = GetComponents<AudioSource>();
		Music = SoundsAudio[0];
		m_Effect1 = SoundsAudio[1];
		m_Effect2 = SoundsAudio[2];
        m_Effect2 = SoundsAudio[3];
        volume = Music.volume;
	}

	// Start is called before the first frame update
	void Start()
	{

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		}
		else Destroy(gameObject);

		Music.clip = Musics[0];
		Music.loop = true;
		Music.volume = volume;
		m_Effect1.volume = volume - 0.10f;
		m_Effect2.volume = volume - 0.10f;
		Music.Play();

		m_Effect1.clip = Damage1[0];
		m_Effect2.clip = Damage2[0];
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PlayAndPose()
	{
		if(!Music.isPlaying)
			Music.Play();
		else Music.Pause();
	}

	public void PlayMusic(AudioClip source)
	{
		Music.clip = source;
		Music.Play();
	}
	public void PlayEffect1(AudioClip source)
	{
		m_Effect1.clip = source;
		m_Effect1.Play();
	}
	public void PlayEffect2(AudioClip source)
	{
		m_Effect2.clip = source;
		m_Effect2.Play();
	}
    public void PlayEffect3(AudioClip source)
    {
        m_Effect3.clip = source;
        m_Effect3.Play();
    }

    public IEnumerator FadeOutVolumeMusic(float duration)
	{
		float startVolume = gameObject.GetComponent<AudioSource>().volume;
		while (Music.volume > 0)
		{
			gameObject.GetComponent<AudioSource>().volume -= startVolume * Time.deltaTime / duration;
			Debug.Log(gameObject.GetComponent<AudioSource>().volume);
			yield return null;
		}
		Music.Stop();
		Music.volume = startVolume;
	}
}
