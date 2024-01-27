using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float range;

    GameObject player;
    float dis;
    float waitTime = 0.1f;
    bool able;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        able = true;
    }

    void Update()
    {
        dis = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(dis);

        if (dis < range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(DestroyTrunk());
            }
        }
    }

    IEnumerator DestroyTrunk()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        while (able)
        {
            yield return new WaitForSeconds(waitTime);
            float aux = transform.GetChild(0).GetComponent<SpriteRenderer>().color.a - 0.3f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, aux);

            if (transform.GetChild(0).GetComponent<SpriteRenderer>().color.a <= 0)
            {
                able = false;
                Destroy(gameObject);
            }
        }
    }
}
