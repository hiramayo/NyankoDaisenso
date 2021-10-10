using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStartController : MonoBehaviour
{
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
        SceneManager.LoadScene("MenuScene");

    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
//        BattleDirector battleDirector = GameObject.Find("BattleDirector").GetComponent<BattleDirector>();

        //TODO データを渡す処理
        //battleDirector.score = 100;

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
