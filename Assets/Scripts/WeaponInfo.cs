using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    energy,
    projectile,
    missile,
    healing,
    laser,
    mining,
}

public class WeaponInfo : MonoBehaviour
{
    public WeaponType weaponType;
}
