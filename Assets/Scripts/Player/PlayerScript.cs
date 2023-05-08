using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    [SerializeField] bool isTakedown = false;

    public int currentHp;

    Adrenaline adrenalineScript;
	PlayerMovement movement;

	void Start()
	{
		currentHp = maxHp;
		adrenalineScript = gameObject.GetComponent<Adrenaline>();
		movement = gameObject.GetComponent<PlayerMovement>();
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
					if (currentHp == 2)
					TakeFirstDamage();
						
				Debug.Log("Player HP: " + currentHp);
			}
			else
			{
				float	launchForceUp = 8f;
				float	launchForceSide = 6f;
				Vector3 forceDirection;
				Vector3 force;

				Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
				forceDirection = (collision.transform.position - transform.position).normalized;
				forceDirection.x += Random.Range(-0.2f, 0.2f);
				forceDirection.z += Random.Range(-0.2f, 0.2f);

				force = (Vector3.up * launchForceUp) + (forceDirection * launchForceSide);
				collision.gameObject.GetComponent<EnnemyMovement>().enabled = false;
				collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				enemyRigidbody.AddForce(force, ForceMode.Impulse);
			}
		}
	}

	public void TakeFirstDamage()
	{
		GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect1(GameObject.Find("AudioManager").GetComponent<AudioController>().Reacts[Random.Range(0, GameObject.Find("AudioManager").GetComponent<AudioController>().Reacts.Length)]);
		StartCoroutine(waitForReact());
	}

	IEnumerator waitForReact()
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect1(GameObject.Find("AudioManager").GetComponent<AudioController>().Damage1[Random.Range(0, GameObject.Find("AudioManager").GetComponent<AudioController>().Damage1.Length)]);
		GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect2(GameObject.Find("AudioManager").GetComponent<AudioController>().Coeur1[Random.Range(0, GameObject.Find("AudioManager").GetComponent<AudioController>().Coeur1.Length)]);

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
