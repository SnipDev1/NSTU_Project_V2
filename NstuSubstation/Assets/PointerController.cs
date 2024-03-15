using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public static PointerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject pointer;

    public void SetRotation()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
