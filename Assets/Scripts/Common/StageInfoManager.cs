using UnityEngine;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists. 
using System;
using static StageInfo;

public class StageInfoManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static StageInfoManager instance = null;

    public List<StageInfo> stageInfos;
    private Dictionary<StageID, StageInfo> stageInfoDictionary = new Dictionary<StageID, StageInfo>();
    //Store a reference to our BoardManager which will set up the level.
    //private BoardManager boardScript;
    //Current level number, expressed in game as "Day  1".

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        if(stageInfos == null) {
            throw new InvalidOperationException("stageInfos are not set!!");
	    }
        else {
            foreach(StageInfo stageInfo in stageInfos){
                stageInfoDictionary.Add(stageInfo.StgID, stageInfo); 
                Debug.LogFormat("add StageInfoDictionary. stageID={0}", stageInfo.StgID);
	        }
	    }

    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);

    }

    public StageInfo GetStageInfo(StageID id) {
        Debug.LogFormat("stageID={0}", id.ToString());
        return stageInfoDictionary[id];
    }



    //Update is called every frame.
    void Update()
    {

    }
}