using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Player;
    [Range (1, 2)]
    public int PlayerNum;
    [Range(-1, 1)]
    public float p1_v_sensitivity;
    [Range(-1, 1)]
    public float p1_h_sensitivity;
    [Range(-1, 1)]
    public float p1_v;
    [Range(-1, 1)]
    public float p1_h;
    [SerializeField]
    float p1_v_speed;
    [SerializeField]
    float p1_h_speed;


    [Range(-1, 1)]
    public float p2_v;
    [Range(-1, 1)]
    public float p2_h;

    [SerializeField]
    float p2_v_speed;
    [SerializeField]
    float p2_h_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }


    public void PlayerControl()
    {
        if (PlayerNum == 1) { P1_Input(); Player.transform.position += new Vector3(p1_h* p1_v_sensitivity, p1_v* p1_h_sensitivity); }
        if (PlayerNum == 2) { P2_Input(); Player.transform.position += new Vector3(Player.transform.position.x * p2_h, Player.transform.position.x * p2_v); }

    }
    public void P1_Input()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            p1_v += Time.deltaTime * p1_v_speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            p1_v -= Time.deltaTime * p1_v_speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            p1_h -= Time.deltaTime * p1_h_speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            p1_h += Time.deltaTime * p1_h_speed;
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            p1_h = 0;
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            p1_v = 0;
        }
    }

    public void P2_Input()
    {
        if (Input.GetKey(KeyCode.R))
        {
            p2_v += Time.deltaTime * p2_v_speed;
        }
        if (Input.GetKey(KeyCode.F))
        {
            p2_v += Time.deltaTime * p2_h_speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            p2_h += Time.deltaTime * p1_h_speed;
        }
        if (Input.GetKey(KeyCode.G))
        {
            p2_h -= Time.deltaTime * p1_h_speed;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.G))
        {
            p2_h = 0;
        }
    }
}

//Up Down Left Right
//    Left Ctrl
//    Left Alt
//    Space
//    z
//    x
//    c
//    5

//R F D G
//    A
//    S
//    Q
//    W
//    E
//    [
//    ]
//    6
