using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseCharacterData;
using System.Linq;

public class DamageCalculator : MonoBehaviour
{
    private Character character;

    public DamageCalculator(Character character)
    {
        this.character = character;
    }

    public int Calculate(Character attackedCharacter) {
        float damage = this.character.CharacterData.CurrentAttackPoint;
        List<SpecialEffect> specialEffects = this.character.CharacterData.SpecialEffects;
        List<CharacterType> targetCharacterTypes = attackedCharacter.CharacterData.CharacterTypes;
        foreach(SpecialEffect se in specialEffects) {
            if (targetCharacterTypes.Contains(se.targetType)) {
                if (se.damageMultiplierWhenAttack < 0) continue; 
                damage = se.damageMultiplierWhenAttack * damage;
	        }
	    }
        List<SpecialEffect> targetSpecialEffects = attackedCharacter.CharacterData.SpecialEffects;
        List<CharacterType> characterTypes = this.character.CharacterData.CharacterTypes;

        foreach(SpecialEffect se in targetSpecialEffects) {
            if (characterTypes.Contains(se.targetType)) {
                if (se.damageMultiplierWhenAttacked < 0) continue; 
                damage = se.damageMultiplierWhenAttacked * damage;
	        }
	    }

        return (int)damage;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
