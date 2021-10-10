using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseCharacterData : ScriptableObject
{

    public BaseCharacterData()
    {

    }
    public enum ImageOrientation  {FACING_LEFT, FACING_RIGHT};

    public enum MillitaryCamp { OWN, ENEMY };

    public ImageOrientation imageOrientation;
    
    public MillitaryCamp millitaryCamp;


    public enum CharacterType {WHITE, RED, BLACK};

    public List<CharacterType> CharacterTypes;

    public List<SpecialEffect> SpecialEffects;

    public enum AttackType { SINGLE_UNIT, MULTIPLE_UNIT, BULLET };

    public AttackType attackType;

    public GameObject attackEffect;

    public string Name;

    public string CharacterId;

    public int BasePrice;

    public int BaseHP;

    public float attackFrequency;

    public float attackRange;

    public float BaseMoveSpeed;


    public int BaseAttackPoint;

    public int NumberOfKnockBack;

    public float ProductionInterval;

}
