using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeDamage : TurretUpgrade
{
    public event UnityAction DamageUpgraded;

    protected override void OnButtonClick()
    {
        DamageUpgraded?.Invoke();
    }
}
