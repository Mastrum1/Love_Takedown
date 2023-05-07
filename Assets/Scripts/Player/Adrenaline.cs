using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : MonoBehaviour
{
	public int maxAdrenaline = 100;
	public float adrenaline;
	[SerializeField] float adrenalineIncreaseSpeed;
	GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		adrenalineIncreaseSpeed = 1f;
		adrenaline = 0f;
		StartCoroutine(Timer());
	}

	void Update()
	{
		adrenalineIncreaseSpeed = IncreaseAdrenaline();
	}

	float IncreaseAdrenaline()
	{
		float IncreaseSpeed;
		int playerHP = player.GetComponent<PlayerScript>().currentHp;
		int HowManyEnemiesAround;
		float HowManyPerSeconds;
		float Multiplier = 1;

		if (playerHP == 3)
			HowManyPerSeconds = 1f;
		else if (playerHP == 2)
			HowManyPerSeconds = 1.5f;
		else
			HowManyPerSeconds = 2f;
		HowManyEnemiesAround = CountEnemiesInRadius();
		if (HowManyEnemiesAround == 1)
			Multiplier = 1.5f;
		else if (HowManyEnemiesAround == 2)
			Multiplier = 2f;
		else if (HowManyEnemiesAround == 3)
			Multiplier = 2.5f;
		else if (HowManyEnemiesAround >= 4)
			Multiplier = 3f;
		IncreaseSpeed = HowManyPerSeconds * Multiplier;
		return IncreaseSpeed;
	}
	int CountEnemiesInRadius()
	{
		int enemyCount = 0;
		float DetectionRadius = 5f;

		Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, DetectionRadius);
		foreach (Collider hitCollider in hitColliders)
		{
			if (hitCollider.CompareTag("Enemy"))
				enemyCount++;
		}
		return enemyCount;
	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds(1f);
		if (adrenaline < maxAdrenaline)
			adrenaline += adrenalineIncreaseSpeed;
		StartCoroutine(Timer());
	}
}