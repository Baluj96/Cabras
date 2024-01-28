using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMovement : MonoBehaviour
{
    public float health = 1;
    float speed = 20;
    float waitTime = 0.1f;
    public bool player;
    bool able;
    AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        able = true;
    
        audioSource = GetComponent<AudioSource>();
        Invoke("Play", 2);
    }

    void Play()
    {
        int r = Random.Range(0, clips.Length);
        audioSource.clip = clips[r];
        audioSource.Play();
        float t = Random.Range(2f, 4f);
        Invoke("Play", t);
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
            Invoke("Damage", 1);
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
            float aux = transform.GetChild(0).GetComponent<SpriteRenderer>().color.a - 0.05f;
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
            transform.tag = "Death";
            GameManager.instance.CheckUI();
            GameManager.instance.ToGameOver();

            StartCoroutine(RotateGoat());
        }
    }
}
