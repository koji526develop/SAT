using UnityEngine;
using System.Collections;

public class CharacterAttack : State<Character>
{
    public CharacterAttack(Character _instance) : base(_instance) { }

    // 敵のキャラクターオブジェクト
    GameObject m_enemyObj;

    // 敵のキャラクター
    Character m_enemyCharacter;

    // 攻撃のインターバル
    float m_attackTime;

    //　敵の情報格納用
    Character.Status m_changeStatus;

    //攻撃倍率
    float magnification = 1.0f;

    public GameObject enemyObj
    {
        set
        {
            m_enemyObj = value;
        }
    }

    public override void Enter()
    {
        m_enemyCharacter = m_enemyObj.GetComponent<Character>();
        m_attackTime = 0.0f;
        m_changeStatus = m_enemyCharacter.status;

        m_instance.animator.Play("Attack");
    }

    public override void Update()
    {
//        if (m_instance.status.PlayerID == 1)
//        {
//            Debug.Log("1p" + m_instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString());
//            ;
//        }
//        else
//        {
//            Debug.Log("2p" + m_instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString());
//        }



        m_attackTime += Time.deltaTime;

        if (m_instance.IsFinishAnimation())
        {
            m_instance.NowAnimationRePlay();

            Attak();
           
            m_attackTime = 0;

        }

        if (m_enemyCharacter == null)
            m_instance.ChangeState(Character.CharacterState.Move);
	
    }
    void Attak()
    {
        if (m_enemyCharacter.Barrier)
        {
            foreach (Transform child in m_enemyCharacter.transform)
            {
                if (child.name == "Barrier")
                    Character.Destroy(child.gameObject);
            }
            m_enemyCharacter.Barrier = false;
            return;
        }
        else
        {
            magnification = 1.0f;
            switch (m_instance.status.characterType)
            {
                case Character.CharacterType.Sword:
                    AudioManager.m_instance.PlaySE("sowrd_SE");
                    if (m_enemyCharacter.status.characterType == Character.CharacterType.Spear) magnification = 1.5f;
                    break;
                case Character.CharacterType.Spear:
                    AudioManager.m_instance.PlaySE("spear_SE");
				if (m_enemyCharacter.status.characterType == Character.CharacterType.Ax) magnification =1.5f;
                    break;
                case Character.CharacterType.Shield:
                    AudioManager.m_instance.PlaySE("shield_SE");
                    //if (m_enemyCharacter.status.characterType == Character.CharacterType.Spear) magnification = 2.0f;
                    break;
                case Character.CharacterType.Ax:
                    AudioManager.m_instance.PlaySE("sowrd_SE");
				if (m_enemyCharacter.status.characterType == Character.CharacterType.Sword) magnification = 1.5f;
                    break;
            }

            m_changeStatus.life -= m_instance.status.attack * magnification;

            m_enemyCharacter.status = m_changeStatus;
        }
    }

    public override void Exit()
    {
        m_enemyObj = null;
        m_enemyCharacter = null;
    }
}
