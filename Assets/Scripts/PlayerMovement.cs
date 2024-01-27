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
    //Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
