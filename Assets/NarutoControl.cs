using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class NarutoControl : MonoBehaviour
{
    public Dropdown dropDown;
    public Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationChange()
    {
        ani.SetInteger("AnimationRank", dropDown.value);

    }


}
