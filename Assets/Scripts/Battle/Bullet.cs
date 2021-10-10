using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Vector3 moveDirection;
    float currentSpeed = 1;
    string bulletTag;
    int damage;
    float range;
    public GameObject explosionEffect;
    Vector3 startPosition;

    public void init(Vector3 moveDirection, string tag, int damage, float range)
    {
        this.moveDirection = moveDirection;
        this.bulletTag = tag;
        this.damage = damage;
        this.range = range;
    }


    //衝突時処理
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //索敵範囲内に敵(＝陣営が異なるキャラクター)を検知したら、ストップ
        if (collision.tag.Equals(this.bulletTag)) return; //同一タグ(仲間)は無視

        //TODO ダメージ計算
        Character target = collision.GetComponent<Character>();
        target.DecreaseHP(damage);

        //TODO パーティクルの生成
        GameObject attackEffect = Object.Instantiate(explosionEffect, this.transform.position, Quaternion.identity);

        //自身を破壊
        Destroy(gameObject);

    }


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(moveDirection * currentSpeed);
        if(Vector3.Distance(startPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
}
