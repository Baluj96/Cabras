using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool trunk;
    [SerializeField] bool hole;
    [SerializeField] float range;
    public Sprite tap;

    GameObject player;
    float dis;
    float waitTime = 0.1f;


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
                if (hole)
                {
                    GetComponent<SpriteRenderer>().sprite = tap;
                    GetComponent<BoxCollider2D>().enabled = false;
                }

                if (trunk)
                {
                    StartCoroutine(DestroyTrunk());
                }
            }
        }
    }

    IEnumerator DestroyTrunk()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            float aux = GetComponent<SpriteRenderer>().color.a - 0.1f;
            GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, aux);

            if (GetComponent<SpriteRenderer>().color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
