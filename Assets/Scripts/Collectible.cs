﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private LevelController controller;
    private ObjectPooler pool;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        MoveCollectible();
    }

    private void MoveCollectible()
    {
        if (transform.position.y > -controller.screenYLimit)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (controller.scrollSpeed * Time.deltaTime));
        }
        else
        {
            pool.Drown(gameObject);
        }
    }
}
