using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Benjamin Kerr
 * Date: 3/4/2020
 * Description: This code will make the object attatch shake when it is spawned in
 */ 
public class exlimationMark_Behaviour : MonoBehaviour
{
    private Vector3 startPos; // Starting position for the object
    private Vector3 startScale;
    private float increase;
    private bool isShaking;

    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
        StartCoroutine(vibraiteText());

        //Start Courintine where it shake
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking == true)
        {
            increase += 0.1f;
            float scale = 0.4f * Mathf.Cos(0.7f * increase + 3.15f) + 1.4f;
            transform.localScale = new Vector3(scale,scale);
            transform.position = new Vector3(0.1f * Mathf.Cos(4f * increase) + startPos.x, transform.position.y);
        }
    }

    private IEnumerator vibraiteText()
    {
        isShaking = true;
        yield return new WaitForSeconds(1.7f);
        isShaking = false;
        transform.position = startPos;
        transform.localScale = startScale;
    }
}
