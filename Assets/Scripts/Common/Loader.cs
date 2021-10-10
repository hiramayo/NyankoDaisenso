using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject stageInfoManager;            //StageInfoManager prefab to instantiate.
    public GameObject gameManager;            //StageInfoManager prefab to instantiate.


    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (StageInfoManager.instance == null)

            //Instantiate gameManager prefab
            Instantiate(stageInfoManager);
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)

            //Instantiate gameManager prefab
            Instantiate(gameManager);

    }
}

