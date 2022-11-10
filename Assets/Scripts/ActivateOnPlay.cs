using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnPlay : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(true);
    }
}
