using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update

    public float enemyMinSprintDistance = 10f;
    public float minTimeUntilNextBurst = 10f;
    public float maxTimeUntilNextBurst = 15f;
    public int burstDuration = 1;
    public int enemyEnergyRecoveryTime = 15;
    public int maxEnemyEnergy = 10;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerScript>().currentHp <= 0)
        {
            Debug.Log("Game Over");
            OnDefeat();
        } 
    }

    public void OnDefeat()
    {
        GameObject.Find("AudioManager").GetComponent<AudioController>().m_Effect1.loop = true;
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayMusic(GameObject.Find("AudioManager").GetComponent<AudioController>().endSound[0]);
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect1(GameObject.Find("AudioManager").GetComponent<AudioController>().endSound[1]);
        SceneManager.LoadScene("DefeatScene");
	}

    public void OnVictory()
    {
		SceneManager.LoadScene("VictoryScene");
	}
}
