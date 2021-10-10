using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static StageInfo;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class BattleDirector : MonoBehaviour
{
    public StageID stageID;

    [SerializeField] Transform buttonPanel;
    [SerializeField] Transform moneyPanel;

    private Camp playerCamp;
    private Camp enemyCamp;
    private Text moneyLabel;
    [SerializeField]
    GameEvent onCharacterDied;

    Dictionary<string, Character> PrefabCharacters = new Dictionary<string, Character>();

    private enum GameState {BEFORE_GAME_START, PROGRESS, END };
    private GameState gameState;
    private EnemyGenerator enemyGenerator;
    private MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.PROGRESS;
        //にゃんこ城の初期化
        playerCamp = GameObject.FindGameObjectWithTag("PlayerCamp").GetComponent<Camp>();
        //TODO 城のデータの取得
        playerCamp.SetUp(2000);

        //城のデータの取得
        enemyCamp = GameObject.FindGameObjectWithTag("EnemyCamp").GetComponent<Camp>();
        enemyCamp.SetUp(StageInfoManager.instance.GetStageInfo(this.stageID).EnemyCampStrength);

        enemyGenerator = this.transform.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        enemyGenerator.Initialize(StageInfoManager.instance.GetStageInfo(this.stageID),enemyCamp);

        //moneyManager init
        moneyManager = this.transform.Find("MoneyManager").GetComponent<MoneyManager>();
        moneyManager.Initialize();

        Dictionary<string,CharacterData> characters = MenuDirector.LoadCharacterData();
        //プレハブの情報を更新（レベルアップ）
        foreach (KeyValuePair<string, CharacterData> c in characters)
        {
            string prefabPath = string.Format("Assets/Resources/Prefabs/{0}.prefab", c.Key);
            GameObject cat = PrefabUtility.LoadPrefabContents(prefabPath);

            // 変更を加える
            Character character = cat.GetComponent<Character>();
            character.CharacterData.LevelUp(c.Value.Level);
            
            //退避
            PrefabCharacters.Add(c.Key, character);

            // 保存する
            PrefabUtility.SaveAsPrefabAsset(cat, prefabPath);
            PrefabUtility.UnloadPrefabContents(cat);
        }

        //フッター部のボタン作成
        foreach (KeyValuePair<string, CharacterData> c in characters)
        {
            //TODO 要価格計算
            int currentPrice = PrefabCharacters[c.Key].CharacterData.CurrentPrice;
            float productionInterval = PrefabCharacters[c.Key].CharacterData.ProductionInterval;
            GameObject button = BattleUI.createCatButton(c.Key, currentPrice, productionInterval);
            button.GetComponent<BtnCat>().checkBeforeClick = delegate() {
                return checkBeforeClick(currentPrice);

	        };
            button.transform.SetParent(buttonPanel);
        }
        //MONEY LABEL
        moneyLabel = moneyPanel.Find("Label").GetComponent<Text>();
        StartCoroutine(EarnMoneyCoroutine());

        //enemey generate
        foreach (EnemyInfo enemyInfo in StageInfoManager.instance.GetStageInfo(this.stageID).enemyInfos)
        { 
            StartCoroutine(SpawnCoroutine(enemyInfo));
	    }
    }

    private IEnumerator EarnMoneyCoroutine() {
        while (gameState != GameState.END) { 
	        int skipFrameCount = 5;
	        for(int i = 0; i < skipFrameCount; i++) {
                yield return null; 
	        }
            this.moneyManager.Earn();
            moneyLabel.text = string.Format("{0}/{1}円 ", this.moneyManager.Money, this.moneyManager.MaxMoney);
	    }
        yield break;
    }
    private IEnumerator SpawnCoroutine(EnemyInfo enemyInfo)
    {
        //生産上限
        int numberOfAppearance = enemyInfo.NumberOfAppearance;
        //check castle strength
        float defaultWaitTime = 0.1f;
        while (enemyInfo.StrengthOfCastle < this.enemyCamp.percentageOfRemainingHp) { 
            yield return new WaitForSeconds(defaultWaitTime);
	    }

        //first time waiting
        float firstWaitTime = enemyInfo.InitialIntervalFrame / GlobalConst.FLAME_RATE_ORIGINAL;
        Debug.LogFormat("firstWaitTime={0}", firstWaitTime);
        Debug.LogFormat("enemyInfo.StrengthOfCastle={0},this.enemyCamp.percentageOfRemainingHp={1}", enemyInfo.StrengthOfCastle,this.enemyCamp.percentageOfRemainingHp);
        yield return new WaitForSeconds(firstWaitTime);
        this.enemyGenerator.spawn(enemyInfo.characterID, () => this.moneyManager.Add(enemyInfo.Price));
        numberOfAppearance--;

        //spawn second time. third time...
        while(numberOfAppearance != 0) {
            float waitTime = enemyInfo.IntervalFrameFrom;
            yield return new WaitForSeconds(waitTime);
            this.enemyGenerator.spawn(enemyInfo.characterID, () => this.moneyManager.Add(enemyInfo.Price));
            numberOfAppearance--;
	    }
        //end
        yield break;
    }


    private void PlayerCampOnDestroyed()
    {
        gameState = GameState.END;
        CodeMonkey.CMDebug.Text("敗北...", localPosition: new Vector3(-3, 0), color: Color.red);

        //ボタンを使用禁止にする
        foreach (Transform child in buttonPanel)
        {
            //child is your child transform
            child.GetComponent<Button>().interactable = false;
        }
        //敵の生産をストップ
        enemyGenerator.stop();
        CodeMonkey.CMDebug.Button(transform, new Vector3(0, 0), "Retry", () => { 
            SceneManager.LoadScene("BattleScene");
	    });
    }

    private void EnemyCampOnDestroyed()
    {
        gameState = GameState.END;
        CodeMonkey.CMDebug.Text("勝利!!!", localPosition:new Vector3(-3, 0), color:Color.red);
        //ボタンを使用禁止にする
        foreach (Transform child in buttonPanel)
        {
            //child is your child transform
            child.GetComponent<Button>().interactable = false;
        }
        //敵の生産をストップ
        enemyGenerator.stop();

        CodeMonkey.CMDebug.Button(transform, new Vector3(0, 0), "OK", () => { 
            SceneManager.LoadScene("MenuScene");
	    });
    }

    private bool checkBeforeClick(int price)
    {
            //最大生産数のチェック
            GameObject[] character = GameObject.FindGameObjectsWithTag("Player");
            if (character.Length > 50)
            {
                Debug.Log(string.Format("キャラクターの生産上限です。生産数={0}", character.Length));
                return false;
            }
            //check payment
            if (!this.moneyManager.TryPay(price)) return false;

        return true;
    }

}
