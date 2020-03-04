using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInPlayer : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //spawn in the player one
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Spawn in player two
        }
    }
}
