using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public SetMovement SetMovement{get; private set;}
    private void Awake() {
        SetMovement = GetComponentInChildren<SetMovement>();

        if (!SetMovement){ Debug.LogError("Missing Core Component");}
    }
}
