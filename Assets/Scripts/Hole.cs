using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] bool hole;
    [SerializeField] float range;
    public Sprite tap;

    GameObject player;
    float dis;
    Animator ani;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        dis = Vector3.Distance(player.transform.position, transform.position);

        if (dis < range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ani.SetTrigger("Tap");
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = tap;
            }
        }
    }
}
