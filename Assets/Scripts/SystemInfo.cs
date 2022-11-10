using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfo : MonoBehaviour
{
    public string systemName;

    [ContextMenu("Set Name")]
    public void SetNameToSystem()
    {
        systemName = this.gameObject.name;
    }

}
