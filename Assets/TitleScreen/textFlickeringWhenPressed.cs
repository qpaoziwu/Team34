using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textFlickeringWhenPressed : MonoBehaviour
{
    public Text playerOneText;
    public Text playerTwoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(flickeringText(playerOneText));
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(flickeringText(playerTwoText));
        }
    }


    public IEnumerator flickeringText(Text text)
    {
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        text.color = Color.white;
    }
}
