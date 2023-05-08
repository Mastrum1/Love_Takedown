using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PhoneLogics : MonoBehaviour
{
    [SerializeField] int connection = 4;

    [SerializeField] Image ConnectionFull;
    [SerializeField] Image Connection3;
    [SerializeField] Image Connection2;
    [SerializeField] Image Connection1;
    [SerializeField] Image Connection0;
    [SerializeField] Image ConnectionLost;
    [SerializeField] Image ConnectionErrorText;
    [SerializeField] GameObject taxi;
    [SerializeField] GameObject myPhone;
    [SerializeField] bool phoneActive = true;

    public bool gameStarted = false;

    bool gone = false;


    // Start is called before the first frame update
    void Start()
    {
       GameObject.Find("taxi").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (GameObject.Find("StartMenu"))
            {
                taxi.SetActive(true);
                GameObject.Find("taxi_near").GetComponent<Image>().enabled = false;
                GameObject.Find("StartMenu").SetActive(false);
            }

            if (GameObject.Find("taxi"))
            {
                GameObject.Find("taxi_mark1").gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("taxi_mark1").gameObject.transform.position, GameObject.Find("goto").transform.position, 0.1f);
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint1").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint2").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint2").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint3").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint3").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint4").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint4").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint5").gameObject.transform.position;
            }

            if (!phoneActive)
            {
                if (!gone)
                    gone = true;
                if (GameObject.Find("myPhone"))
                    myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("hidePosition").transform.position, 1);
            }
            else
            {
                if (gone)
                    gone = false;
                myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("showPosition").transform.position, 1);
            }
            ConnectionUpdate(connection);
        }
    }

    void ConnectionUpdate(int value)
    {
        switch (value)
        {
            case 4:
                ConnectionFull.enabled = true;
                Connection3.enabled = false;
                Connection2.enabled = false;
                Connection1.enabled = false;
                Connection0.enabled = false;
                ConnectionLost.enabled = false;
                ConnectionErrorText.enabled = false;
                break;
            case 3:
                ConnectionFull.enabled = false;
                Connection3.enabled = true;
                Connection2.enabled = false;
                Connection1.enabled = false;
                Connection0.enabled = false;
                ConnectionLost.enabled = false;
                ConnectionErrorText.enabled = false;
                break;
            case 2:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = true;
                Connection1.enabled = false;
                Connection0.enabled = false;
                ConnectionLost.enabled = false;
                ConnectionErrorText.enabled = false;
                break;
            case 1:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = false;
                Connection1.enabled = true;
                Connection0.enabled = false;
                ConnectionLost.enabled = false;
                ConnectionErrorText.enabled = false;
                break;
            case 0:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = false;
                Connection1.enabled = false;
                Connection0.enabled = true;
                ConnectionLost.enabled = true;
                ConnectionErrorText.enabled = true;
                break;
        }
    }
}
