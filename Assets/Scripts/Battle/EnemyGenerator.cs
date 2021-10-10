using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEditor;
using static StageInfo;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject catPrefab;
    public StageInfo stageInfo;

    public void Initialize(StageInfo stageInfo, Camp enemyCamp)
    {
        this.stageInfo = stageInfo;

        //プレハブの情報を更新（レベルアップ）
        foreach (EnemyInfo enemyInfo in stageInfo.enemyInfos)
        {
            string prefabPath = string.Format("Assets/Resources/Prefabs/{0}.prefab", enemyInfo.characterID);
            GameObject cat = PrefabUtility.LoadPrefabContents(prefabPath);

            // 変更を加える
            Character character = cat.GetComponent<Character>();
            character.CharacterData.EnhanceXTimes(enemyInfo.Magnification);

            // 保存する
            PrefabUtility.SaveAsPrefabAsset(cat, prefabPath);
            PrefabUtility.UnloadPrefabContents(cat);
        }
    }

    public void spawn(string id, UnityAction onDied) { 
        GameObject prefabFromAssets = Resources.Load(string.Concat("Prefabs/", id)) as GameObject;
        GameObject prefab = (GameObject)Instantiate(prefabFromAssets, GlobalConst.ENEMY_APPERRENCE_PLACE, Quaternion.identity);
        Character character = prefab.GetComponent<Character>();
        character.Initialize(Character.MillitaryCamp.ENEMY);
        character.onDied = onDied;
    }
    public void stop()
    {
        enabled = false;
    }

}
