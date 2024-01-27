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
        //Debug.Log(currentAmount);
    }


    protected void DiminishStat()
    {
        if (canDiminsh)
        {
            currentAmount -= 5 * Mathf.Clamp(Time.deltaTime, 0, maxAmount);
            currentAmount = Mathf.Clamp(currentAmount, 0, maxAmount);


        }
    }
}
