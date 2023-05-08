using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiScript : MonoBehaviour
{
	[SerializeField] GameObject taxi;
	[SerializeField] float      taxiSpeed = 10f;
    [SerializeField] GameObject WinArea;

    Rigidbody	rbody;

	bool taxiSpawned = false;

	GameObject waitingPoint;
	GameObject endPoint;


	void Start()
	{
		waitingPoint = GameObject.Find("waitingPoint");
		endPoint = GameObject.Find("endPoint");
		rbody = GetComponent<Rigidbody>();
	}

    private void Update()
    {
		if (!taxiSpawned)
		{
            if (GameObject.Find("Phone").GetComponent<PhoneLogics>().taxihere)
                ActivateTaxi();
        }
    }

    public void ActivateTaxi()
	{
			Debug.Log("Activating taxi");
			taxiSpawned = true;
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
		WinArea.SetActive(true );
		yield return new WaitForSeconds(10f);
		Debug.Log("Taxi finished waiting");
        GameObject.Find("AudioManager").GetComponent<AudioController>().PlayEffect3(GameObject.Find("AudioManager").GetComponent<AudioController>().Car[1]);
        WinArea.SetActive(false );
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

	IEnumerator Loose()
	{
        yield return new WaitForSeconds(10);

    }
}
