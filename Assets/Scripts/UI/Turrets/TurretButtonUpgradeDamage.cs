using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretButtonUpgradeDamage : TurretButtonUpgrade
{
    public event UnityAction ButtonUpgradeDamagePressed;

    protected override void OnButtonClick()
    {
        ButtonUpgradeDamagePressed?.Invoke();
    }
}
