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
    [SerializeField] Image taxiMark;
    [SerializeField] Image Takedown;
    [SerializeField] GameObject taxi;
    [SerializeField] GameObject myPhone;


    [SerializeField] public bool phoneActive = true;

    public bool gameStarted = false;

    bool gone = false;
    bool running;
    public Adrenaline adrenalineScript;
    public bool taxihere = false;


    // Start is called before the first frame update
    void Start()
    {
       GameObject.Find("taxi").SetActive(false);
       Takedown.enabled = false;
        ConnectionLost.GetComponent<Image>().enabled = false;
        ConnectionErrorText.GetComponent<Image>().enabled = false;

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
                GameObject.Find("taxi_mark1").gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("taxi_mark1").gameObject.transform.position, GameObject.Find("goto").transform.position, 1.023f);
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint1").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint2").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint2").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint3").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint3").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint4").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint4").gameObject.transform.position)
                    GameObject.Find("goto").gameObject.transform.position = GameObject.Find("checkpoint5").gameObject.transform.position;
                if (GameObject.Find("taxi_mark1").gameObject.transform.position == GameObject.Find("checkpoint5").gameObject.transform.position)
                    taxihere = true;
            }

            if (GameObject.Find("Player").GetComponent<PlayerMovement>().adrenalineScript.adrenaline >= GameObject.Find("Player").GetComponent<PlayerMovement>().adrenalineScript.maxAdrenaline)
                Takedown.enabled = true;
            else Takedown.enabled = false;

            if (!phoneActive)
            {
                if (!gone)
                    gone = true;
                if (GameObject.Find("myPhone"))
                    myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("hidePosition").transform.position, 3);
            }
            else
            {
                if (gone)
                    gone = false;
                myPhone.gameObject.transform.position = Vector3.MoveTowards(GameObject.Find("myPhone").gameObject.transform.position, GameObject.Find("showPosition").transform.position, 3);
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
                ConnectionFull.GetComponent<Image>().enabled = true;
                Connection3.GetComponent<Image>().enabled = false;
                Connection2.GetComponent<Image>().enabled = false;
                Connection1.GetComponent<Image>().enabled = false;
                Connection0.GetComponent<Image>().enabled = false;
                ConnectionLost.GetComponent<Image>().enabled = false;
                ConnectionErrorText.GetComponent<Image>().enabled = false;
                taxiMark.GetComponent<Image>().enabled = true;
                break;
            case 3:
                ConnectionFull.GetComponent<Image>().enabled = false;
                Connection3.GetComponent<Image>().enabled = true;
                Connection2.GetComponent<Image>().enabled = false;
                Connection1.GetComponent<Image>().enabled = false;
                Connection0.GetComponent<Image>().enabled = false;
                break;
            case 2:
                ConnectionFull.GetComponent<Image>().enabled = false;
                Connection3.GetComponent<Image>().enabled = false;
                Connection2.GetComponent<Image>().enabled = true;
                Connection1.GetComponent<Image>().enabled = false;
                Connection0.GetComponent<Image>().enabled = false;
                break;
            case 1:
                ConnectionFull.GetComponent<Image>().enabled = false;
                Connection3.GetComponent<Image>().enabled = false;
                Connection2.GetComponent<Image>().enabled = false;
                Connection1.GetComponent<Image>().enabled = true;
                Connection0.GetComponent<Image>().enabled = false;
                break;
            case 0:
                ConnectionFull.GetComponent<Image>().enabled = false;
                Connection3.GetComponent<Image>().enabled = false;
                Connection2.GetComponent<Image>().enabled = false;
                Connection1.GetComponent<Image>().enabled = false;
                Connection0.GetComponent<Image>().enabled = true;
                ConnectionLost.GetComponent<Image>().enabled = true;
                ConnectionErrorText.GetComponent<Image>().enabled = true;
                taxiMark.GetComponent<Image>().enabled = false;
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
                        ConnectionLost.GetComponent<Image>().enabled = true;
                        ConnectionErrorText.GetComponent<Image>().enabled = true;
                        taxiMark.GetComponent<Image>().enabled = false;
                    }
                    break;

                case 2:
                    if (Random.Range(0, 100) > 50)
                    {
                        ConnectionLost.GetComponent<Image>().enabled = true;
                        ConnectionErrorText.GetComponent<Image>().enabled = true;
                        taxiMark.GetComponent<Image>().enabled = false;
                    }
                    break;
                case 3:
                    if (Random.Range(0, 100) > 80)
                    {
                        ConnectionLost.GetComponent<Image>().enabled = true;
                        ConnectionErrorText.GetComponent<Image>().enabled = true;
                        taxiMark.GetComponent<Image>().enabled = false;
                    }
                    break;
            }
            yield return new WaitForSeconds(5);
            ConnectionLost.GetComponent<Image>().enabled = false;
            ConnectionErrorText.GetComponent<Image>().enabled = false;
            taxiMark.GetComponent<Image>().enabled = true;
            running = false;
        }
    }
}
