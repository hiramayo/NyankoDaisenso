using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.Events;
using static BaseCharacterData;

public class Character : MonoBehaviour
{

    [SerializeField]
    public UnityAction onDied;

    public enum ImageOrientation  {FACING_LEFT, FACING_RIGHT };
    public enum MillitaryCamp { OWN, ENEMY };

    public CharacterData CharacterData;
    private KnockBackSystem knockBackSystem;


    private int layerMask;

    public ImageOrientation imageOrientation;
    //TODO 生成時に陣営を判定したい。
    public MillitaryCamp millitaryCamp;
    Vector3 moveDirection;
    float currentSpeed;
    Animator animator;

    Collider2D  m_collider2D;

    public float span = 1f; //1秒毎に攻撃
    private float currentTime = 0f;

    private DamageCalculator damageCalculator;
    int i = 0;

   
    // Start is called before the first frame update
    public void Initialize(MillitaryCamp millitaryCamp = MillitaryCamp.OWN)
    {
        this.animator = GetComponent<Animator>();

        this.currentSpeed = calcSpeed();
        this.damageCalculator = new DamageCalculator(this);

        //画像の向きと陣営にずれがある場合、画像の向きを修正
        if (millitaryCamp == MillitaryCamp.OWN && imageOrientation == ImageOrientation.FACING_RIGHT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //進行方向の決定
        if(millitaryCamp == MillitaryCamp.OWN)
        {
            moveDirection = -1 * transform.right;
            layerMask = 1 << LayerMask.NameToLayer("Enemy");
        }
        else
        {
            moveDirection = transform.right;
            layerMask = 1 << LayerMask.NameToLayer("Player");
        }
        span = CharacterData.AttackFrequency / GlobalConst.FLAME_RATE_ORIGINAL;
        Debug.LogFormat("name={0}, span={1}", this.name, span);
        m_collider2D = GetComponent<Collider2D>();
        knockBackSystem = new KnockBackSystem(CharacterData.MaxHP, CharacterData.NumberOfKnockBack);

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * currentSpeed);

        Ray2D ray = new Ray2D(calculateRayOrigin(),  moveDirection);

        RaycastHit2D hit = Physics2DExtentsion.RaycastAndDraw(ray.origin, ray.direction, CharacterData.AttackRange, layerMask);

        if (hit.collider)
        {
            // 動きを止める
            StopMoving();
            // 攻撃
            currentTime += Time.deltaTime;
            if (currentTime > span)
            {
                attack();
                currentTime = 0f;
            }
        }
        else
        {
            // 動きを再開する
            restartMoving();

        }



    }

    private Vector2 calculateRayOrigin()
    {
        Vector2 rayOrigin;
        if (moveDirection == transform.right)
        {
            rayOrigin = new Vector2(m_collider2D.bounds.max.x, transform.position.y);
        }
        else
        {
            rayOrigin = new Vector2(m_collider2D.bounds.min.x, transform.position.y);
        }
        return rayOrigin;
    }

    private void attack()
    {
        Ray2D ray;
        RaycastHit2D hit;
        GameObject attackEffect;
        this.animator.SetTrigger("AttackTrigger");
        switch (CharacterData.AttackType)
        {
            case AttackType.SINGLE_UNIT:

                //オブジェクトから進行方向にRayを伸ばす
                ray = new Ray2D(calculateRayOrigin(),  moveDirection);
                hit = Physics2DExtentsion.RaycastClosest(ray.origin, ray.direction, CharacterData.AttackRange, layerMask);

                if (hit.collider)
                {
                    //ダメージ計算
                    //キャラクターの場合
                    Character targetCharacter = hit.collider.GetComponent<Character>();
                    if(targetCharacter != null)
                    {

                        int damage = damageCalculator.Calculate(targetCharacter);
                        targetCharacter.DecreaseHP(damage);
                        //パーティクルの生成
                         attackEffect = Object.Instantiate(CharacterData.attackEffect, targetCharacter.transform.position, Quaternion.identity);
                        return;
                    }
                    Camp targetCamp = hit.collider.GetComponent<Camp>();

                    //城の場合
                    if(targetCamp != null)
                    {
                        targetCamp.Damage(this.CharacterData.CurrentAttackPoint);
                        //パーティクルの生成
                         attackEffect = Object.Instantiate(CharacterData.attackEffect, targetCamp.transform.position, Quaternion.identity);
                    }
                }
                break;

            case AttackType.MULTIPLE_UNIT:

                //オブジェクトから進行方向にRayを伸ばす
                ray = new Ray2D(calculateRayOrigin(),  moveDirection);
                RaycastHit2D[] hits  = Physics2D.RaycastAll(ray.origin, ray.direction, CharacterData.AttackRange, layerMask);
                for (int i = 0; i < hits.Length; i++)
                {
                    hit = hits[i];

                    //ダメージ計算
                    //キャラクターの場合
                    Character targetCharacter = hit.collider.GetComponent<Character>();
                    if(targetCharacter != null)
                    {
                        int damage = damageCalculator.Calculate(targetCharacter);
                        targetCharacter.DecreaseHP(damage);
                        //パーティクルの生成
                         attackEffect = Object.Instantiate(CharacterData.attackEffect, targetCharacter.transform.position, Quaternion.identity);
                        return;
                    }
                    //城の場合
                    Camp targetCamp = hit.collider.GetComponent<Camp>();
                    if (targetCamp != null)
                    {
                        targetCamp.Damage(this.CharacterData.CurrentAttackPoint);
                        //パーティクルの生成
                         attackEffect = Object.Instantiate(CharacterData.attackEffect, targetCamp.transform.position, Quaternion.identity);

                    }

               }
                break;

            case AttackType.BULLET:

                GameObject prefabFromAssets = Resources.Load(string.Concat("Prefabs/", "bullet")) as GameObject;
                GameObject bullet = Object.Instantiate(prefabFromAssets, this.transform.position, Quaternion.identity);
                //TODO 射程
                float range = 5.0f;
                bullet.GetComponent<Bullet>().init(this.moveDirection, this.tag, this.CharacterData.CurrentAttackPoint, range);
                break;


        }
    }

    public void DecreaseHP(int damage)
    {
        int beforeHP = this.CharacterData.CurrentHP;
        this.CharacterData.CurrentHP = this.CharacterData.CurrentHP - damage;
        //死亡判定
        if(this.CharacterData.CurrentHP < 0)
        {
            this.CharacterData.CurrentHP = 0;
            this.KillSelf();
            if(onDied != null) { 
                onDied();
	        }
        }
        //check knockback
        Debug.LogFormat("CharacterID={0},beforeHP={1},afterHP={2}", this.name, beforeHP, this.CharacterData.CurrentHP);
        if (this.knockBackSystem.KnockBack(beforeHP, this.CharacterData.CurrentHP)) {
            Debug.Log("Knockback!!");
            this.transform.position += moveDirection * (-1);
	    }
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }

    private void StopMoving()
    {
        currentSpeed = 0f;
    }

    private void restartMoving()
    {
        currentSpeed = calcSpeed();
    }

    //スピードの再計算
    private float calcSpeed()
    {
        return CharacterData.MoveSpeed / 1000;
    }
}


