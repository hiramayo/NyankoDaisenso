using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StageInfo;

public class ButtonStage1 : MonoBehaviour
{
    public string stageID = "stg1";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        //Debug.Log("Button click!");
        //// 非表示にする

        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        // シーン切り替え
        SceneManager.LoadScene("BattleScene");

    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得

        //BattleDirector enemyGenerator = GameObject.FindWithTag("BattleDirector").GetComponent<BattleDirector>();
        BattleDirector battleDirector = GameObject.Find("BattleDirector").GetComponent<BattleDirector>();

        //TODO データを渡す処理
        battleDirector.stageID = StageID.Stage1;

        // イベントから削２除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
