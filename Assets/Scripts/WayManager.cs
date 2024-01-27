using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    public static WayManager instance;

    public float waitTime = 0.1f;
    public float dificultad = 0.1f;
    public GameObject[] wayPrefabs;
    GameObject sun, goal, player;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        StartCoroutine(GiroLuz());

        goal = GameObject.FindGameObjectWithTag("Goal");
        player = GameObject.FindGameObjectWithTag("Player");
        goal.transform.position = new Vector3(90 / dificultad, 0, 0);
        int d = Mathf.RoundToInt(Vector3.Distance(player.transform.position, goal.transform.position));
        for (int i = -15; i < d; i += 10)
        {
            int r = Random.Range(0, wayPrefabs.Length);
            Instantiate(wayPrefabs[r], new Vector3(0, 0, i), Quaternion.Euler(90, 0, 0));
        }
    }

    IEnumerator GiroLuz()
    {
        while (sun.transform.rotation.x <= 90)
        {
            sun.transform.rotation = Quaternion.Euler(Vector3.right * dificultad);
            yield return new WaitForSeconds(waitTime);
        }
        GameManager.instance.GameOver();
    }
}
