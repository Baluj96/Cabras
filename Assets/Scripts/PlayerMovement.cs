using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float speed,
                  smoothTime;

    
    Rigidbody rb;
    Vector3 targetVelocity, dampVelocity;
    AudioSource audioSource;
    public AudioClip[] clips;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("Play", 1);
    }

    void Play()
    {
        int r = Random.Range(0, clips.Length);
        audioSource.clip = clips[r];
        audioSource.Play();
        float t = Random.Range(5f, 7f);
        Invoke("Play", t);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        targetVelocity = new Vector3(h, 0, v).normalized * speed;
        Animating(h, v);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref dampVelocity, smoothTime);
    }

    void Animating(float h, float v)
    {
        /*if (h != 0 || v != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else if (h == 0 || v == 0)
        {
            anim.SetBool("IsRunning", false);
        }*/
    }
}
