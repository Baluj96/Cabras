using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMovement : MonoBehaviour
{
    public float health = 1;
    public bool player;

    void Update()
    {
    }

    private void FixedUpdate()
    {
        GameObject target;
        if (player)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Goal");
        }

        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
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
