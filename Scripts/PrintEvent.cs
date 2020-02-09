// This C# function can be called by an Animation Event
using UnityEngine;
using System.Collections;


public class PrintEvent : MonoBehaviour
{
   

    public void Print(Animator animator)
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}