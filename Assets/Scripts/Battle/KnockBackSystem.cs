using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackSystem 
{
    private float maxHP;
    private int numberOfKnockBack;
    private List<float> boudaryHPs;

    public KnockBackSystem(float maxHP, int numberOfKnockBack)
    {
        this.maxHP = maxHP;
        this.numberOfKnockBack = numberOfKnockBack;
        this.boudaryHPs = calculateBoundaryHPs();
        foreach(float hp in boudaryHPs) {
            Debug.LogFormat(" hp={0}", hp);
	    } 
    }

    //public void Initialize(int maxHP, int numberOfKnockBack) {
    //    this.maxHP = maxHP;
    //    this.numberOfKnockBack = numberOfKnockBack;
    //    this.normalizedHPs = calculateSplitStrength();
    //}

    private List<float> calculateBoundaryHPs()
    {
        List<float> result = new List<float>();
        float strengthPerUnit = maxHP / (numberOfKnockBack + 1);
        for(int i = 0; i < this.numberOfKnockBack; i++) {
            result.Add(strengthPerUnit * (i + 1));
	    }
        return result;
    }

    //private float NormalizedHP(int HP) {
    //    return HP / this.maxHP;
    //} 

    private int IndexOfBoundaryHPs(int HP) {
        //float normalizedHP = NormalizedHP(HP);
        for (int i = 0; i < this.numberOfKnockBack; i++) {
            //Debug.LogFormat("normalizedHP ={0} this.normalizedHPs[i]={1}", normalizedHP, this.normalizedHPs[i]); 
            //If physical strength is within the relevant range, return the relevant index.
            if (HP <= this.boudaryHPs[i]  ) {
                return i;
	        }
	    }
	    // if arrive here, strength is near full capacity.
        return this.numberOfKnockBack;
    }

    public bool KnockBack(int beforeHP, int afterHP) {
        Debug.LogFormat("IndexOfSplitStrength(beforeHP={0})IndexOfSplitStrength(afterHP={1})",IndexOfBoundaryHPs(beforeHP),IndexOfBoundaryHPs(afterHP));
        return IndexOfBoundaryHPs(beforeHP) != IndexOfBoundaryHPs(afterHP);
    }
}
