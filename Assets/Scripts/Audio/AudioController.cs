using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;

    [Range(0f,1f)]
    public float volume = 1f;

    public AudioClip[] Musics;
    public AudioClip[] Reacts;
    public AudioClip[] Damage1;
    public AudioClip[] Damage2;
    public AudioClip[] Coeur;
    public AudioClip[] Ennemy;
    public AudioClip lamp;

    private AudioSource m_Source;
    private float timer;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();

        volume = m_Source.volume;
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

        m_Source.clip = Musics[0];
        m_Source.loop = true;
        m_Source.volume = volume;
        m_Source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAndPose()
    {
        if(!m_Source.isPlaying)
            m_Source.Play();
        else m_Source.Pause();
    }

    public void PlayMusic(AudioClip source)
    {
        m_Source.clip = source;
        m_Source.Play();
    }

    public IEnumerator FadeOutVolumeMusic(float duration)
    {
        float startVolume = gameObject.GetComponent<AudioSource>().volume;
        while (m_Source.volume > 0)
        {
            gameObject.GetComponent<AudioSource>().volume -= startVolume * Time.deltaTime / duration;
            Debug.Log(gameObject.GetComponent<AudioSource>().volume);
            yield return null;
        }
        m_Source.Stop();
        m_Source.volume = startVolume;
    }
}
