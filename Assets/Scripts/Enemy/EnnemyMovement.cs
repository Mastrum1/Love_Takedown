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

    int isBurstActivate;
	int energyRecoveryTime;
	float maxEnnemySpeed;
	float minEnnemySpeed;
	float timeUntilNextBurst;
	float playerSpeed;
	float distanceToPlayer;

	GameObject player;
	Animator animator;

    int isRunningHash;

    private void Start()
	{
		animator = gameObject.GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().baseMoveSpeed;
		minEnnemySpeed = playerSpeed - (playerSpeed / 6);
		maxEnnemySpeed = playerSpeed - (playerSpeed / 2);
		normalSpeed = Random.Range(minEnnemySpeed, maxEnnemySpeed);
		burstSpeed = normalSpeed * 2;
		speed = normalSpeed;
		player = GameObject.FindGameObjectWithTag("Player");
		timeUntilNextBurst = Random.Range(GameObject.Find("GameManager").GetComponent<GameManager>().minTimeUntilNextBurst, GameObject.Find("GameManager").GetComponent<GameManager>().maxTimeUntilNextBurst);
		distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		minSprintDistance = GameObject.Find("GameManager").GetComponent<GameManager>().enemyMinSprintDistance;
		burstDuration = GameObject.Find("GameManager").GetComponent<GameManager>().burstDuration;
		energyRecoveryTime = GameObject.Find("GameManager").GetComponent<GameManager>().enemyEnergyRecoveryTime;
		energy = GameObject.Find("GameManager").GetComponent<GameManager>().maxEnemyEnergy;
    }

	void Update()
	{
		if (GameObject.Find("Phone").GetComponent<PhoneLogics>().gameStarted)
		{
            float step = speed * Time.deltaTime;
            gameObject.transform.LookAt(player.transform);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
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
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

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
			energy = GameObject.Find("GameManager").GetComponent<GameManager>().maxEnemyEnergy;
		}
	}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").transform.position, minSprintDistance);
    }
#endif

}