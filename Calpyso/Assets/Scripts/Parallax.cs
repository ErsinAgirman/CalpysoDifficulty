using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{   
    [SerializeField] private float parallaxFactor;
    private Transform playerTransform;
    private float startPositionX;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPositionX = transform.position.x;
    }

    void Update()
    {
        float deltaX = (playerTransform.position.x - startPositionX) * parallaxFactor;
        transform.position = new Vector3(startPositionX + deltaX, transform.position.y, transform.position.z);
    }
}
