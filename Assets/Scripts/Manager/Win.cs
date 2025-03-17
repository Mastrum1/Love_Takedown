using UnityEngine;

public class Win : MonoBehaviour
{
    
    private AudioController audioController;
    
    private void Start()
    {
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioController.Music.loop = false;
            audioController.PlayMusic(audioController.Car[2]);
            GameObject.Find("GameManager").GetComponent<GameManager>().OnVictory();
        }
    }
}
