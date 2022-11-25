using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Blueprint")]
public class Blueprint : Item
{
    internal Recipe recipe;
    private void OnEnable()
    {
        type = ItemType.blueprint;
    }
}
