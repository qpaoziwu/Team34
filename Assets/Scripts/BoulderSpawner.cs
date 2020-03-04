using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Benjamin Kerr
 * Date: 3/4/20202
 * Description: This code will spawn boulders from the top with intervals.
 * Before landing an ico will apear warning the player of the icoming danger.
 */ 
public class BoulderSpawner : MonoBehaviour
{
    public GameObject boulder;
    public GameObject exclimationMarkIcon;
    private bool spawnBoulder; // This variable is responsible for spawning a boulder
    private float maxTime;


    // Start is called before the first frame update
    void Start()
    {
        maxTime = 6f;
        spawnBoulder = false;
        StartCoroutine(waitForNextRepawn(maxTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnBoulder == true)
        {
            print(Screen.width);
            float xPos = Random.Range(-3, 5);
            Instantiate(boulder, new Vector3(xPos, 10, 0), Quaternion.identity);
            Instantiate(exclimationMarkIcon, new Vector3(xPos, 4, 0), Quaternion.identity);
            spawnBoulder = false;
            StartCoroutine(waitForNextRepawn(maxTime));

            //Spawn the boulder
        }
    }


    private IEnumerator waitForNextRepawn(float time)
    {
        spawnBoulder = false;
        yield return new WaitForSeconds(time);
        maxTime = Random.Range(3, 9);
        spawnBoulder = true;
    }
}
