using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnRate = 2;
    [SerializeField] bool spawnActive = true;

    [SerializeField] GameObject Spawner1;
    [SerializeField] GameObject Spawner2;
    [SerializeField] GameObject Spawner3;
    [SerializeField] GameObject Spawner4;
    [SerializeField] GameObject Spawner5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnRate);

        if (spawnActive)
        {
            switch (Random.Range(1, 6))
            {
                case 1:
                    Instantiate(enemy, Spawner1.transform.position, Spawner4.transform.rotation);
                    break;
                case 2:
                    Instantiate(enemy, Spawner2.transform.position, Spawner4.transform.rotation);
                    break;
                case 3:
                    Instantiate(enemy, Spawner3.transform.position, Spawner4.transform.rotation);
                    break;
                case 4:
                    Instantiate(enemy, Spawner4.transform.position, Spawner4.transform.rotation);
                    break;
                case 5:
                    Instantiate(enemy, Spawner4.transform.position, Spawner4.transform.rotation);
                    break;
            }
        }
        StartCoroutine(SpawnEnemy());
    }
}
