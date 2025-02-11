using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public int currentHp;
	public float invincibleTime = 2.0f;
	public float timeSinceInvincible = 0.0f;
	public  bool isTakedown = false;
	
	[SerializeField] private int maxHp = 3;
	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	private AudioController audioController;

	void Start()
	{
		currentHp = maxHp;
		audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();
	}

	void Update()
	{
		virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>(). m_CameraDistance = (12 - ((maxHp - currentHp) * 6))+1;
		if (currentHp == 3) virtualCamera.m_Lens.NearClipPlane = 21;
		else if (currentHp == 2) virtualCamera.m_Lens.NearClipPlane = 15;
		else if (currentHp == 1) virtualCamera.m_Lens.NearClipPlane = 10;
		timeSinceInvincible += Time.deltaTime;
	}

	private void OnCollisionStay(Collision collision)
	{
		var obj = collision.gameObject;
		if (obj.CompareTag("Enemy") && timeSinceInvincible >= invincibleTime || obj.CompareTag("Enemy") && isTakedown)
		{
			timeSinceInvincible = 0.0f;
			if (!isTakedown)
			{
				if (currentHp > 0) currentHp--;
				if (currentHp == 2) TakeFirstDamage();
				else if (currentHp == 1) TakeSecondDamage();
				Debug.Log("Player HP: " + currentHp);
			}
//		Takedown
			else
			{
				float	launchForceUp = 8f;
				float	launchForceSide = 6f;
				Vector3 forceDirection;
				Vector3 force;

				Rigidbody enemyRigidbody = obj.GetComponent<Rigidbody>();
				forceDirection = (collision.transform.position - transform.position).normalized;
				forceDirection.x += Random.Range(-0.2f, 0.2f);
				forceDirection.z += Random.Range(-0.2f, 0.2f);

				force = (Vector3.up * launchForceUp) + (forceDirection * launchForceSide);
				collision.gameObject.GetComponent<EnnemyMovement>().enabled = false;
				collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
				enemyRigidbody.constraints = RigidbodyConstraints.None;
				enemyRigidbody.AddForce(force, ForceMode.Impulse);
			}
		}
	}

	public void TakeFirstDamage()
	{
		audioController.PlayEffect1(audioController.Reacts[Random.Range(0, audioController.Reacts.Length)]);
		StartCoroutine(WaitForReact1());
	}

	IEnumerator WaitForReact1()
	{
		yield return new WaitForSeconds(1f);
		audioController.PlayEffect1(audioController.Damage1[Random.Range(0, audioController.Damage1.Length)]);
		audioController.PlayEffect2(audioController.Coeur1[Random.Range(0, audioController.Coeur1.Length)]);
	}

	public void TakeSecondDamage()
	{
		audioController.PlayEffect1(audioController.Reacts[Random.Range(0, audioController.Reacts.Length)]);
		StartCoroutine(waitForReact2());
	}

	IEnumerator waitForReact2()
	{
		yield return new WaitForSeconds(1f);
		audioController.PlayEffect1(audioController.Damage1[Random.Range(0, audioController.Damage2.Length)]);
		audioController.PlayEffect2(audioController.Coeur1[Random.Range(0, audioController.Coeur2.Length)]);
	}
}
