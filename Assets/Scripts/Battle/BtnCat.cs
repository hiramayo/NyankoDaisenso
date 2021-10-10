using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BtnCat : MonoBehaviour
{
    [SerializeField] private GameObject btnCatPrefab;

    private GameObject catPrefab;
    private string id;
    public int price { get; private set; }
    private float productionInterval;
    private Button button;
    private Slider slider;

    public delegate bool CheckBeforeClick();
    public CheckBeforeClick checkBeforeClick;


    public void Initialize(string id, int price, float productionInterval) { 
        this.id = id;
        this.price = price;
        this.productionInterval = productionInterval;
        this.catPrefab = Resources.Load(string.Concat("Prefabs/", this.id)) as GameObject;
        button = this.GetComponent<Button>();
        slider = transform.Find("WaitingTimeBar").GetComponent<Slider>();
        //金額
        button.transform.Find("Price").GetComponent<Text>().text = price.ToString();

        //画像
        Sprite imageFromAssets = AssetDatabase.LoadAssetAtPath<Sprite>(string.Format("Assets/image/{0}.png", id));
        button.transform.Find("Image").GetComponent<Image>().overrideSprite = imageFromAssets;

        //click
        button.onClick.AddListener(() => {
            //check
            if (!checkBeforeClick()) return;

            GameObject cat = Object.Instantiate(catPrefab, GlobalConst.OWN_APPERRENCE_PLACE, Quaternion.identity);
            Character character = cat.GetComponent<Character>();
            character.Initialize();

            //disabled for a while
            DisableWhileProductionInterval();
	});

    }

    //Make it unusable while it is out of production.
    public void DisableWhileProductionInterval() {
        StartCoroutine(disableWhileProductionInterval());
    }

    IEnumerator disableWhileProductionInterval() { 
        button.interactable = false;
        float sumTime = 0f;
        while(sumTime < productionInterval){
            sumTime += Time.deltaTime;
            slider.value = sumTime / productionInterval;
            yield return null;
	    }

        button.interactable = true;
        yield break;
    }
    private void Update()
    {
        
    }

}
