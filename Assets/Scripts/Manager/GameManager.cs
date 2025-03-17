using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float enemyMinSprintDistance = 10f;
    public float minTimeUntilNextBurst = 10f;
    public float maxTimeUntilNextBurst = 15f;
    public int burstDuration = 1;
    public int enemyEnergyRecoveryTime = 15;
    public int maxEnemyEnergy = 10;
    
    [SerializeField] private PlayerScript player;

    private AudioController audioController;

    private void Start()
    {
        audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
    }

    void Update()
    {
        if (player.currentHp <= 0)
        {
            OnDefeat();
        } 
    }

    public void OnDefeat()
    {
        audioController.m_Effect1.loop = true;
        audioController.PlayMusic(audioController.endSound[0]);
        audioController.PlayEffect1(audioController.endSound[1]);
        SceneManager.LoadScene("DefeatScene");
	}

    public void OnVictory()
    {
		SceneManager.LoadScene("VictoryScene");
	}
}
