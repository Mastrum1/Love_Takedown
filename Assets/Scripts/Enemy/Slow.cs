using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{

    [SerializeField] float slowForce = 2;

    bool working = true;

    // Start is called before the first frame update
    void Start()
    {
        slowForce = Random.Range(1.5f, 2);  
    }

    // Update is called once per frame
    void Update()
    {
        if (!working)
        {
            gameObject.GetComponent<Light>().enabled = false;
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnnemyMovement>().speed = collider.gameObject.GetComponent<EnnemyMovement>().normalSpeed / slowForce;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnnemyMovement>().speed = collider.gameObject.GetComponent<EnnemyMovement>().normalSpeed;
        }
    }

    IEnumerator stopWorking()
    {
        gameObject.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.8f);
        gameObject.GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Light>().enabled = false;
        working = false;

        yield return new WaitForSeconds(8);
        working = true;
    }

}
