using System;
using System.Collections.Generic;
using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BattleUI

{
    public BattleUI()
    {
    }

    public static GameObject createCatButton(string characterId, int currentPrice, float productionInterval )
    {
        //にゃんこボタンを生成
        GameObject prefabFromAssets = Resources.Load(string.Concat("Prefabs/btnCat")) as GameObject;
        GameObject button = Object.Instantiate(prefabFromAssets) as GameObject;
        button.GetComponent<BtnCat>().Initialize(characterId, currentPrice, productionInterval);
        ////金額
        //button.transform.Find("price").GetComponent<Text>().text = currentPrice.ToString();

        ////画像
        //Sprite imageFromAssets = AssetDatabase.LoadAssetAtPath<Sprite>(string.Format("Assets/image/{0}.png", characterId));
        //button.transform.Find("Image").GetComponent<Image>().overrideSprite = imageFromAssets;
        ////クリック時処理
        //button.GetComponent<Button>().onClick.AddListener(onClick);
        return button;
    }

    public static void createCharacter(BaseCharacterData character)
    {
        GameObject prefabFromAssets = Resources.Load(string.Concat("Prefabs/", character.CharacterId)) as GameObject;
        Object.Instantiate(prefabFromAssets, GlobalConst.OWN_APPERRENCE_PLACE, Quaternion.identity);
    }
}
