using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackSpeed : TurretStats
{
    private Turret _turret;

    public bool IsMaxLevel { get; private set; }

    private void OnEnable()
    {
        IsMaxLevel = false;
        _turret = GetComponent<Turret>();
    }

    public override void Upgrade()
    {
        Value = 1 - Level * UpdateValue;
        Level++;

        _turret.SetAttackSpeed(Value);

        if (Level == MaxLevel)
        {
            IsMaxLevel = true;
        }
    }
}
