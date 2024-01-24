using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 0.2f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        OneWay();   
    }

    void OneWay()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.2f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.2f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }
    }
}
