using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.Victory();
            other.GetComponent<PlayerMovement>().enabled = false;
            DestroyGoats();
        }
    }

     void DestroyGoats()
    {
        GameObject[] goats = GameObject.FindGameObjectsWithTag("Goat");
        foreach (GameObject goat in goats)
        {
            Destroy(goat);
        }
    }
}
