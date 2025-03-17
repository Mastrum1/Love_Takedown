using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Phone").GetComponent<PhoneLogics>().connection++;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Phone").GetComponent<PhoneLogics>().connection--;
        }
    }
}
