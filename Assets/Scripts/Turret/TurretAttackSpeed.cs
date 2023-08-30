using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackSpeed : TurretStats
{
    protected override void Upgrade()
    {
        if (_isMaxLevel)
            return;

        Value = 1 - Level * UpdateValue;
        Level++;
    }
}
