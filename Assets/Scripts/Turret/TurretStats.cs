using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretStats : MonoBehaviour
{
    [SerializeField] protected int StartCost;
    [SerializeField] protected int CostForLevel;
    [SerializeField] protected float StartValue;
    [SerializeField] protected float UpdateValue;

    protected int Level;
    protected int Cost;
    protected float Value;
    protected int MaxLevel = 10;
    protected bool _isMaxLevel = false;

    private void OnEnable()
    {
        Value = StartValue;
        Level = 0;
    }

    protected abstract void Upgrade();

    public float GetValue()
    {
        return Value;
    }

    public float GetCostUpgrade()
    {
        Cost = StartCost + CostForLevel * Level;
        return Cost;
    }

}
