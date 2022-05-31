using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class AnimationControl : MonoBehaviour
{
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ani.SetTrigger("Walk");
        }
        if (Input.GetKey(KeyCode.R))
        {
            ani.SetTrigger("Run");
        }
        if (Input.GetKey(KeyCode.J))
        {
            ani.SetTrigger("Jump");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ani.SetTrigger("Stand");
        }
        if (Input.GetKey(KeyCode.S))
        {
            ani.SetTrigger("Sprint");
        }

    }
}
