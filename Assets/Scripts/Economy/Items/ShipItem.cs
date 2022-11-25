using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItem : UseableItem
{
    [SerializeField] ShipInfoObject shipInfo;
    /// <summary>
    /// Uses interactable ship item
    /// </summary>
    public override void Use()
    {
        shipInfo.isOwned = true;
        StatManager.instance.playerInventory.RemoveItem(this, 1);
    }
}
