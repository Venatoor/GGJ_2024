using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    protected float maxAmount;
    protected float currentAmount;

    protected abstract void Initialize();

    public virtual void Increase(float amount)
    {
        currentAmount += Mathf.Clamp(amount, 0,maxAmount);
    }

    public virtual void Decrease(float amount)
    {
        currentAmount -= Mathf.Clamp(amount, 0, maxAmount);
    }

    public void FullRegen()
    {
        currentAmount = maxAmount;
    }

    public float GetMaxAmount()
    {
        return maxAmount;
    }

    public void SetMaxAmount(float maxAmount)
    {
        this.maxAmount = maxAmount;
    }

}
