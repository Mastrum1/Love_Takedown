using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    public int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (currentHp > 0)
                currentHp--;
            Debug.Log("Player HP: " + currentHp);
        }
    }
}
