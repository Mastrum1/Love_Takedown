using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
	int isBurstActivate;
	[SerializeField]
	int burstDuration = 1;
	[SerializeField]
	float normalSpeed;
	[SerializeField]
	float burstSpeed;
	float speed;
	float maxEnnemySpeed;
	float minEnnemySpeed;
	float timeUntilNextBurst;
	float playerSpeed;
	float minSprintDistance;
	float distanceToPlayer;
	GameObject player;
	//Faire en sorte que les ennemis ne sprintent que si ils sont proches du joueur.

	private void Start()
	{
		playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().moveSpeed;
		minEnnemySpeed = playerSpeed - (playerSpeed / 5);
		maxEnnemySpeed = playerSpeed - (playerSpeed / 10);
		normalSpeed = Random.Range(minEnnemySpeed, maxEnnemySpeed);
		burstSpeed = normalSpeed * 2;
		speed = normalSpeed;
		player = GameObject.FindGameObjectWithTag("Player");
		timeUntilNextBurst = Random.Range(10f, 15f);
		distanceToPlayer = 10;
	}

	void Update()
	{
		float step = (float)speed * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
		timeUntilNextBurst -= Time.deltaTime;
		if (timeUntilNextBurst <= 0)
		{
			StartCoroutine(Burst());
			timeUntilNextBurst = Random.Range(10f, 15f);
		}
	}

	IEnumerator Burst()
	{
		distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		isBurstActivate = Random.Range(0, 100);
		if (distanceToPlayer <= minSprintDistance && isBurstActivate <= 0)
			speed = burstSpeed;
		yield return new WaitForSeconds(burstDuration);
		speed = normalSpeed;
	}

}