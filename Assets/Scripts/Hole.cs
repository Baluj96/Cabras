using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] float range;
    public Sprite tap;
    public GameObject Agujero;

    GameObject player;
    float dis;
    Animator ani;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dis = Vector3.Distance(player.transform.position, transform.position);

        if (dis < range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Presiona e");
                GetComponent<SpriteRenderer>().sprite = tap;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
