using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMovement : MonoBehaviour
{
    public float health = 1;

    void Update()
    {
    }

    private void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            GameManager.instance.numGenerateGoats--;
            
            GameManager.instance.CheckUI();
            GameManager.instance.ToGameOver();
            Destroy(gameObject, 0.2f);
        }
    }
}
