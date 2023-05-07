using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{

    [SerializeField] float slowForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnnemyMovement>().speed /= slowForce;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnnemyMovement>().speed = collider.gameObject.GetComponent<EnnemyMovement>().normalSpeed;
        }
    }

}
