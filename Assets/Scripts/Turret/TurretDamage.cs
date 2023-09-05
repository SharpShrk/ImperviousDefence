using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamage : TurretStats
{
    public bool IsMaxLevel { get; private set; }

    private void OnEnable()
    {
        IsMaxLevel = false;
    }

    public override void Upgrade()
    {
        Value += UpdateValue;
        Level++;

        Debug.Log(gameObject.name + "Апгрейд урона. Текущий урон: " + Value + ", текущий левел: " + Level);

        if(Level == MaxLevel)
        {
            IsMaxLevel = true;
        }
    }
}
