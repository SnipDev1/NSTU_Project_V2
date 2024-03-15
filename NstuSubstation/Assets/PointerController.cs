using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public static PointerController Instance { get; private set; }
    public Quaternion initialRot;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject pointer;

    public void RotationDump()
    {
        initialRot = pointer.transform.rotation;
    }
    
    public void SetRotation()
    {
        pointer.transform.rotation = initialRot;
    }
}
