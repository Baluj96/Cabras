using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMovement : MonoBehaviour
{
    public float health = 1, speed = 3;
    float waitTime = 0.1f;
    public bool player;
    bool able;

    void Start()
    {
        able = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Invoke("Damage", 2);
            
            
        }
    }

    IEnumerator RotateGoat()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        while (able)
        {
            yield return new WaitForSeconds(waitTime);
            transform.rotation = Quaternion.Euler(Vector3.right * speed * Time.deltaTime);
            float aux = transform.GetChild(0).GetComponent<SpriteRenderer>().color.a - 0.1f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, aux);

            if (transform.rotation.x >= 1800 || transform.GetChild(0).GetComponent<SpriteRenderer>().color.a == 0)
            {
                able = false;
                Destroy(gameObject);
            }
        }
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            GameManager.instance.numGenerateGoats--;

            GameManager.instance.CheckUI();
            GameManager.instance.ToGameOver();

            StartCoroutine(RotateGoat());
        }
    }
}
