using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretStats : MonoBehaviour
{
    
    [SerializeField] protected float StartValue;
    [SerializeField] protected float UpdateValue;

    protected int Level;
    //protected int Cost;
    protected float Value;
    protected int MaxLevel = 10;

    private void OnEnable()
    {
        Value = StartValue;
        Level = 0;       
    }

    public abstract void Upgrade();

    public float GetValue()
    {
        return Value;
    }

    public int GetLevel()
    {
        return Level;
    }

    

}
