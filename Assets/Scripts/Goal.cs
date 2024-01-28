using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyGoats();
            other.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            GameManager.instance.ToVictory();
            other.GetComponent<PlayerMovement>().enabled = false;
        }
        if (other.CompareTag("Goat"))
        {
            other.tag = "MainGoat";
            DestroyGoats();
            other.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().Win();
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
