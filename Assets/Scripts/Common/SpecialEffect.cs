using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseCharacterData;

[CreateAssetMenu]
public class SpecialEffect : ScriptableObject
{
    public SpecialEffectType specialEffectType;
    public string explanation;
    public CharacterType targetType;
    public float damageMultiplierWhenAttack;
    public float damageMultiplierWhenAttacked;
    public float distanceToBlow;



}

public enum SpecialEffectType {
    ExtremelyResistantToRedEnemies,//赤い敵にめっぽう強い


}

