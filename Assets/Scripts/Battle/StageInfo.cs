using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageInfo : ScriptableObject
{
	[System.Serializable]
	public class EnemyInfo
	{
		public string characterID 
		{
			get { return this.baseCharacterData.CharacterId; }
		}


		public BaseCharacterData baseCharacterData;
		public int Price { get { return baseCharacterData.BasePrice; } }

		//出現数（無制限:-1）
		public int NumberOfAppearance;

		//倍率（比率）ex.(1.2)
		public float Magnification;
		//城連動（比率）ex.(0.99)
		public float StrengthOfCastle;
		//初登場F
		public float InitialIntervalFrame;
		//再登場F(FROM)（一度のみ:0）
		public float IntervalFrameFrom;
		//再登場F(TO)（一度のみ:0）
		public float IntervalFrameTo;

	}
	public StageID StgID;

	public int EnemyCampStrength;

	public List<EnemyInfo> enemyInfos;

	public enum StageID{ 
		Stage1,
		Stage2,
		Stage3,
		Stage4,
		Stage5,
		Stage6,
		Stage7,
		Stage8,
		Stage9,
		Stage10,
		Stage11,
		Stage12,
		Stage13,
		Stage14
		
    }
}