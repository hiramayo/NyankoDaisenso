using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor;
using UnityEngine;

public class Camp : MonoBehaviour
{

    [SerializeField]
    GameEvent onDestroyed;
    
    [SerializeField]
    private HealthBar healthBarPrefab;
    private HealthBar healthBar;

    public GameObject destroyEffect;

    public event EventHandler OnDestroyed;
    public event EventHandler OnHpChanged;

    public int MaxHP;

    public int CurrentHP;

    public float percentageOfRemainingHp;

    public void SetUp(int hp)
    {
        this.MaxHP = hp;
        this.CurrentHP = hp;
        this.percentageOfRemainingHp = 100f;
        healthBar = Instantiate(healthBarPrefab, transform.position + new Vector3(0,3), Quaternion.identity);
        this.healthBar.Initialize(MaxHP);
    }

    public void Damage(int damage)
    {
        if (this.CurrentHP <= 0) return;
        this.CurrentHP = this.CurrentHP - damage;
        float NoramalizedSize = (float)CurrentHP / MaxHP;
        this.percentageOfRemainingHp = NoramalizedSize * 100f; 
        this.healthBar.SetData(CurrentHP, NoramalizedSize);
        if (this.OnHpChanged != null)
        {
            OnHpChanged(this, EventArgs.Empty);
        }
        if (this.CurrentHP <= 0){
            this.CurrentHP = 0;
            if (this.OnDestroyed != null)
            {
                OnDestroyed(this, EventArgs.Empty);

            }
            Debug.LogFormat("this.onCharacterDied.Raise()");
            this.onDestroyed.Raise();
        }
    }
}
