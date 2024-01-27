using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    Vector3 dampVelcity;
    public float smoothTargetTime;
    bool able = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (able)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref dampVelcity, smoothTargetTime);
        }
    }

    public void Win()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainGoat");
        able = false;
        transform.position = Vector3.SmoothDamp(transform.position, go.transform.position + offset, ref dampVelcity, smoothTargetTime);
        Invoke("Next", 2);
    }
    
    void Next()
    {
        able = true;
        GameManager.instance.Victory();
    }
}
