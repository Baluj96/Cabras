using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    [SerializeField] float waitTime = 0.1f;
    [SerializeField] float dificultad = 0.1f;
    public GameObject[] wayPrefabs;
    GameObject sun, goal;

    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        goal = GameObject.FindGameObjectWithTag("Goal");
        StartCoroutine(GiroLuz());
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
