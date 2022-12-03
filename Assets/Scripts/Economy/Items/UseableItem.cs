using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableItem : Item
{

    /// <summary>
    /// Uses interactable item
    /// </summary>
    public virtual void Use()
    {
        StatManager.instance.playerInventory.RemoveItem(this, 1);
    }
}
