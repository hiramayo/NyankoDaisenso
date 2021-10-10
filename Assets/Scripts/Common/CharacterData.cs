using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using static BaseCharacterData;

[System.Serializable]
public class CharacterData
{

    public BaseCharacterData BaseCharacterData;
    public int Level = 1;

    public float Magnification = 1;

    public int MaxHP;

    public int CurrentHP;
    
    public int CurrentPrice;

    public float CurrentMoveSpeed;

    public List<CharacterType> CharacterTypes { get { return BaseCharacterData.CharacterTypes; } }

    public List<SpecialEffect> SpecialEffects { get { return BaseCharacterData.SpecialEffects; } }

    public string Name {
	    get { return BaseCharacterData.Name; } 
    }

    public string CharacterId {
	    get { return BaseCharacterData.CharacterId; } 
    }

    public AttackType AttackType {
	    get { return BaseCharacterData.attackType; } 
    }

    public GameObject attackEffect {
	    get { return BaseCharacterData.attackEffect; } 
    }

    public float AttackFrequency {
	    get { return BaseCharacterData.attackFrequency; } 
    }

    public float AttackRange;
	
	public float MoveSpeed {
	    get { return BaseCharacterData.BaseMoveSpeed; } 
    }

    public int CurrentAttackPoint;

    public int NumberOfKnockBack {
        get { return BaseCharacterData.NumberOfKnockBack; }
    }
    public float ProductionInterval {
        get { return BaseCharacterData.ProductionInterval; }
    }

    public CharacterData()
    {
    }

    public void EnhanceXTimes(float magnification)
    {
        this.Level = 1;
        this.AttackRange = BaseCharacterData.attackRange / GlobalConst.REDUCTION_RATIO;
        this.Magnification = magnification; 
        this.MaxHP = (int)(BaseCharacterData.BaseHP * this.Magnification);
        this.CurrentPrice = this.BaseCharacterData.BasePrice;
        this.CurrentHP = this.MaxHP;
        this.CurrentMoveSpeed = BaseCharacterData.BaseMoveSpeed;
        this.CurrentAttackPoint = (int)(BaseCharacterData.BaseAttackPoint * this.Magnification);
    }
    public void LevelUp(int level) { 
        if (level < 1) throw new Exception("レベルは１以上の整数で入力してください。");
        this.Level = level;
        this.AttackRange = BaseCharacterData.attackRange / GlobalConst.REDUCTION_RATIO;
        this.Magnification = 1 + 0.2f * (level - 1);
        this.MaxHP = (int)(BaseCharacterData.BaseHP * this.Magnification);
        this.CurrentPrice = this.BaseCharacterData.BasePrice;
        this.CurrentHP = this.MaxHP;
        this.CurrentMoveSpeed = BaseCharacterData.BaseMoveSpeed;
        this.CurrentAttackPoint = (int)(BaseCharacterData.BaseAttackPoint * this.Magnification);
    }
}
