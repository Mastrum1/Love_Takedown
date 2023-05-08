using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PhoneLogics : MonoBehaviour
{
    [SerializeField] public int connection = 4;

    [SerializeField] Image ConnectionFull;
    [SerializeField] Image Connection3;
    [SerializeField] Image Connection2;
    [SerializeField] Image Connection1;
    [SerializeField] Image Connection0;
    [SerializeField] Image ConnectionLost;
    [SerializeField] Image ConnectionErrorText;
    [SerializeField] Image current_taxiMark;
    [SerializeField] Image transparent;
    [SerializeField] Image saved_taxi_mark;
    [SerializeField] GameObject taxi;
    [SerializeField] GameObject myPhone;

    [SerializeField] public bool phoneActive = true;

    public bool gameStarted = false;

    bool gone = false;
    bool running;


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
                GameObject.Find("taxi_mark1").gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("taxi_mark1").gameObject.transform.position, GameObject.Find("goto").transform.position, 0.01f);
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
                    myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("hidePosition").transform.position, 2);
            }
            else
            {
                if (gone)
                    gone = false;
                myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("showPosition").transform.position, 2);
            }
            ConnectionUpdate(connection);
            StartCoroutine(ConnectionChanger());
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
                current_taxiMark.enabled = true;
                break;
            case 3:
                ConnectionFull.enabled = false;
                Connection3.enabled = true;
                Connection2.enabled = false;
                Connection1.enabled = false;
                Connection0.enabled = false;
                break;
            case 2:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = true;
                Connection1.enabled = false;
                Connection0.enabled = false;
                break;
            case 1:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = false;
                Connection1.enabled = true;
                Connection0.enabled = false;
                break;
            case 0:
                ConnectionFull.enabled = false;
                Connection3.enabled = false;
                Connection2.enabled = false;
                Connection1.enabled = false;
                Connection0.enabled = true;
                ConnectionLost.enabled = true;
                ConnectionErrorText.enabled = true;
                current_taxiMark.enabled = false;
                break;
        }    
            }
    IEnumerator ConnectionChanger()
    {
        if (!running)
        {
            running = true;
            switch (connection)
            {
                case 1:
                    if (Random.Range(0, 100) > 20)
                    {
                        current_taxiMark = transparent;
                        ConnectionLost.enabled = true;
                        ConnectionErrorText.enabled = true;
                    }
                    break;

                case 2:
                    if (Random.Range(0, 100) > 50)
                    {
                        current_taxiMark = transparent;
                        ConnectionLost.enabled = true;
                        ConnectionErrorText.enabled = true;
                    }
                    break;
                case 3:
                    if (Random.Range(0, 100) > 80)
                    {
                        current_taxiMark = transparent;
                        ConnectionLost.enabled = true;
                        ConnectionErrorText.enabled = true;
                    }
                    break;
            }
            yield return new WaitForSeconds(5);
            current_taxiMark = saved_taxi_mark;
            ConnectionLost.enabled = false;
            ConnectionErrorText.enabled = false;
            running = false;
        }
    }
}
