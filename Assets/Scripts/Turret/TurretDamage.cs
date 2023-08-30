using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamage : TurretStats
{
    protected override void Upgrade()
    {
        if (_isMaxLevel)
            return;

        Value += UpdateValue;
        Level++;
    }
}
