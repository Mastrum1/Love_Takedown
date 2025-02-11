using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnnemyMovement : MonoBehaviour
{
	public float speed;
	public float normalSpeed;

    [SerializeField] int burstDuration;
	[SerializeField] float burstSpeed;
	[SerializeField] float minSprintDistance;
	[SerializeField] int energy;
	[SerializeField] Animator animator;

    int isBurstActivate;
	int energyRecoveryTime;
	float maxEnnemySpeed;
	float minEnnemySpeed;
	float timeUntilNextBurst;
	float playerSpeed;
	float distanceToPlayer;

	GameObject _player;
	
    int isRunningHash;
	int isWalkingHash;

	private bool _gameStarted;
	private GameManager _gameManager;

    private void Start()
    {
	    _gameStarted = GameObject.Find("Phone").GetComponent<PhoneLogics>().gameStarted;
	    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_player = GameObject.FindGameObjectWithTag("Player");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingHash = Animator.StringToHash("isWalking");
        playerSpeed = _player.GetComponent<PlayerMovement>().baseMoveSpeed;
		minEnnemySpeed = playerSpeed - (playerSpeed / 6);
		maxEnnemySpeed = playerSpeed - (playerSpeed / 2);
		normalSpeed = Random.Range(minEnnemySpeed, maxEnnemySpeed);
		burstSpeed = normalSpeed * 2;
		speed = normalSpeed;
		timeUntilNextBurst = Random.Range(_gameManager.minTimeUntilNextBurst, _gameManager.maxTimeUntilNextBurst);
		distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
		minSprintDistance = _gameManager.enemyMinSprintDistance;
		burstDuration = _gameManager.burstDuration;
		energyRecoveryTime = _gameManager.enemyEnergyRecoveryTime;
		energy = _gameManager.maxEnemyEnergy;
    }

	void Update()
	{
		if (_gameStarted)
		{
            float step = speed * Time.deltaTime;
            gameObject.transform.LookAt(_player.transform);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, step);
            timeUntilNextBurst -= Time.deltaTime;
            if (timeUntilNextBurst <= 0)
            {
                StartCoroutine(Burst());
                timeUntilNextBurst = Random.Range(10f, 15f);
            }
        }
    }

	IEnumerator Burst()
	{
		isBurstActivate = Random.Range(0, 100);
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);

		if (distanceToPlayer <= minSprintDistance)
			energy--;
        if (distanceToPlayer <= minSprintDistance && isBurstActivate <= 50 && energy > 0)
		{
            speed = burstSpeed;
            energy -= 5;
			animator.SetBool(isRunningHash, true);
        }
		yield return new WaitForSeconds(burstDuration);
		speed = normalSpeed;
		animator.SetBool(isRunningHash, false);
		StartCoroutine(LoseEnergy());

	}

	//Enemmy Energy manager
	IEnumerator LoseEnergy()
	{
		if (energy <= 0)
		{
			speed = normalSpeed/2;
			yield return new WaitForSeconds(energyRecoveryTime);
			speed = normalSpeed;
			energy = _gameManager.maxEnemyEnergy;
		}
	}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_player.transform.position, minSprintDistance);
    }
#endif

}