using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ;
        GameObject.Find("AudioManager").GetComponent<AudioController>().Music.loop = false;
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().Car[2]);
        GameObject.Find("GameManager").GetComponent<GameManager>().OnVictory();

    }
}
