using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField]
	Adrenaline		adrenalineScript;
	[SerializeField]
	PlayerMovement	movement;
	[SerializeField]
	int				maxHp = 3;
	public int		currentHp;
	[SerializeField]
	bool			isTakedown = false;

	void Start()
	{
		currentHp = maxHp;
	}

	void Update()
	{
		if (adrenalineScript.adrenaline >= adrenalineScript.maxAdrenaline)
			StartCoroutine(ActivateTakedown());
		GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>(). m_CameraDistance = (12 - ((maxHp - currentHp) * 6))+1;
		if (currentHp == 3)
			GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 21;
		else if (currentHp == 2)
			GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 15;
		else if (currentHp == 1)
			GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 10;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision");
		if (collision.gameObject.tag == "Enemy")
		{
			if (!isTakedown)
			{
				if (currentHp > 0)
					currentHp--;
				Debug.Log("Player HP: " + currentHp);
			}
			else
			{
				float	launchForceUp = 5f;
				float	launchForceSide = 10f;
				Vector3 forceDirection;
				Vector3 force;

				Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
				forceDirection = (collision.transform.position - transform.position).normalized;
				forceDirection.x += Random.Range(-0.2f, 0.2f);
				forceDirection.z += Random.Range(-0.2f, 0.2f);

				force = (Vector3.up * launchForceUp) + (forceDirection * launchForceSide);
				enemyRigidbody.AddForce(force, ForceMode.Impulse);
			}
		}
	}

	IEnumerator ActivateTakedown()
	{
		Debug.Log("TAKEDOWN !!!!");
		isTakedown = true;
		movement.moveSpeed = 4.5f;
		yield return new WaitForSeconds(8f);
		isTakedown = false;
		movement.moveSpeed = 3f;
		adrenalineScript.adrenaline = 0;
	}
}
