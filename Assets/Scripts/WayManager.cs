using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    public static WayManager instance;

    public float waitTime = 0.1f;
    public float dificultad = 0.1f;
    public GameObject[] wayPrefabs, Obstacles;
    public Transform ground;
    GameObject sun, goal, player;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        player = GameObject.FindGameObjectWithTag("Player");
        goal.transform.position = new Vector3(0, 0, 90 / dificultad);
        int d = Mathf.RoundToInt(Vector3.Distance(player.transform.position, goal.transform.position));
        for (int i = -15; i < d; i += 10)
        {
            int r = Random.Range(0, wayPrefabs.Length);
            Instantiate(wayPrefabs[r], new Vector3(0, 0, i), Quaternion.Euler(90, 0, 0), ground);

            

            /*if (i >= 15)
            {
                int o = Random.Range(1, 3);
                for (int j = 0; j < o; j++)
                {
                    float px = Random.Range(-3f, 3f), py = Random.Range(-5f, 5f);
                    int index = Random.Range(0, Obstacles.Length);
                    contador = contador + 1;
                    Debug.Log("Contador: " + contador);

                    Instantiate(Obstacles[index], new Vector3(px, 0, i + py), Quaternion.identity, ground);
                }
            }*/
        }
        
        CreateObstacle();


        //sun = GameObject.FindGameObjectWithTag("Sun");
        //StartCoroutine(GiroLuz());
    }

    public void CreateObstacle()
    {
        Debug.Log("genera troncos");
        int contador = 0;
        GameObject[] ways = GameObject.FindGameObjectsWithTag("Way");
        for (int i = 0; i < ways.Length; i++)
        {
            int o = 0;
            if (Vector3.Distance(ways[i].transform.position, player.transform.position) >= 20
                && (Vector3.Distance(ways[i].transform.position, player.transform.position) < 50))
            {
                o = Random.Range(1, 2);
                
            }
            else if ((Vector3.Distance(ways[i].transform.position, player.transform.position) >= 50) 
                && (Vector3.Distance(ways[i].transform.position, player.transform.position) < 80))
            {
                o = Random.Range(1, 3);
            }
            else if (Vector3.Distance(ways[i].transform.position, player.transform.position) >= 80)
            {
                o = Random.Range(2, 4);
            }

            Debug.Log("o" + o);
            for (int j = 0; j < o; j++)
            {
                float px = Random.Range(-3f, 3f), py = Random.Range(-5f, 5f);
                int index = Random.Range(0, Obstacles.Length);
                contador = contador + 1;
                Debug.Log("Contador: " + contador);

                Instantiate(Obstacles[index], new Vector3(px, 0, ways[i].transform.position.z + py), Quaternion.identity, ground);
            }
        }
    }

    /*IEnumerator GiroLuz()
    {
        while (sun.transform.rotation.x < 90)
        {
            sun.transform.rotation = Quaternion.Euler(Vector3.right * dificultad * Time.deltaTime);
            yield return new WaitForSeconds(waitTime);
        }

        if (sun.transform.rotation.x >= 90)
        {
            GameManager.instance.GameOver();
        }
    }*/
}
