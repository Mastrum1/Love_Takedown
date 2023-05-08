using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiScript : MonoBehaviour
{
	[SerializeField] GameObject taxi;
	[SerializeField] float      taxiSpeed = 10f;

	Rigidbody	rbody;

	GameObject waitingPoint;
	GameObject endPoint;

	void Start()
	{
		waitingPoint = GameObject.Find("waitingPoint");
		endPoint = GameObject.Find("endPoint");
		rbody = GetComponent<Rigidbody>();
		ActivateTaxi();
	}

	void ActivateTaxi()
	{
			Debug.Log("Activating taxi");
			taxi.SetActive(true);
			StartCoroutine(MoveTaxi());
	}

	IEnumerator MoveTaxi()
	{
		while (Vector3.Distance(taxi.transform.position, waitingPoint.transform.position) > 0.02f)
		{
			float step = taxiSpeed * Time.deltaTime;
			taxi.transform.position = Vector3.MoveTowards(taxi.transform.position, waitingPoint.transform.position, step);
			yield return null;
		}
		Debug.Log("Taxi reached waiting point");
		yield return new WaitForSeconds(10f);
		Debug.Log("Taxi finished waiting");
		while (Vector3.Distance(taxi.transform.position, endPoint.transform.position) > 0.02f)
		{
			float step = taxiSpeed * Time.deltaTime;
			taxi.transform.position = Vector3.MoveTowards(taxi.transform.position, endPoint.transform.position, step);
			yield return null;
		}
		DeactivateTaxi();
	}

	void DeactivateTaxi()
	{
		Debug.Log("Deactivating taxi");
		taxi.SetActive(false);
	}
}
