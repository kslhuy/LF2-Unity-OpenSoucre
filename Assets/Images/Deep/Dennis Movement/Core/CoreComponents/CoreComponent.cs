using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core ; 

    protected virtual void Awake (){
        // SetMovement which is CoreComponent
        // is going to be a child of Game Object that how Core Script
        core = transform.parent.GetComponent<Core>();

        if (core == null){ Debug.LogError("There is no Core ");}
    }
}
