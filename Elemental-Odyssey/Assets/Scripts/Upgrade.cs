using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType; 
    public float value; 
}

public enum UpgradeType
{
    MovementSpeed,
    DashCooldown,
    AttackDamage,
    DashLength
}
