using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeAttackSpeed : TurretUpgrade
{
    public event UnityAction AttackSpeedUpgraded;

    protected override void OnButtonClick()
    {
        AttackSpeedUpgraded?.Invoke();
    }
}
