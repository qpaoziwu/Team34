using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private LevelController controller;
    private ObjectPooler pool;
    public Vector3 startingPosition;


    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
        if (GetComponent<BoxCollider2D>().isTrigger == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
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
