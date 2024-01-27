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
            int o = Random.Range(1,4);
            for (int j = 0; j < o; j++)
            {
                float px = Random.Range(- 3f, 3f), py = Random.Range(-5f, 5f);
                int index = Random.Range(0, Obstacles.Length);
                Instantiate(Obstacles[index], new Vector3(i + px, 0, i), Quaternion.Euler(0, 0, 0), ground);
            }
        }

        //sun = GameObject.FindGameObjectWithTag("Sun");
        //StartCoroutine(GiroLuz());
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
