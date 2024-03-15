using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private GameObject pointer;

    public void SetRotation()
    {
        var transformRotation = pointer.transform.rotation;
        transformRotation.x = 0f;
        transformRotation.y = 0f;
        transformRotation.z = 0f;
    }
}
