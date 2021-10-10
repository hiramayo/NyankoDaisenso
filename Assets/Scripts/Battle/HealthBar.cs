using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //[SerializeField]
    private Transform bar;
    private Transform canvas;
    private Text HPLabel;
    private int maxHP;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.transform.Find("Bar");
        canvas = this.transform.Find("Canvas");
        HPLabel = canvas.transform.Find("HPLabel").GetComponent<Text>();
        setHP(this.currentHP);
    }
    public void Initialize(int maxHp) {
        maxHP = maxHp;
        currentHP = maxHp;
    }
    public void SetData(int currentHP, float sizeNoramalized)
    {
        //setCurrentHP(currentHP);
        SetSize(sizeNoramalized);
        setHP(currentHP);
    }

    private void setHP(int currentHP) {
        this.currentHP = currentHP;
        HPLabel.text = string.Concat(currentHP, "/", this.maxHP);

    }

    //public void setCurrentHP(int hp) { 
    //    currentHP = hp;
    //}

    public void SetSize(float sizeNoramalized) {
        bar = this.transform.Find("Bar");
        bar.localScale = new Vector3(sizeNoramalized, 1f);
    }
    public void SetColor(Color color) {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
