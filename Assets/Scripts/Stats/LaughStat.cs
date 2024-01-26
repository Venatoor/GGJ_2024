using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughStat : Stat
{

    private bool canDiminsh = true;
    protected override void Initialize()
    {
        //TODO : Read From GameManager
    }

    public override void Increase(float amount)
    {
        base.Increase(amount);
    }


    // Update is called once per frame
    void Update()
    {
        DiminishStat();
    }


    protected void DiminishStat()
    {
        if (canDiminsh)
        {
            currentAmount -= Mathf.Clamp(Time.deltaTime, 0, maxAmount);
        }
    }
}
