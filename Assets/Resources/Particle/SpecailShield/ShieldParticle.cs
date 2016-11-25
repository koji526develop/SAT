using UnityEngine;
using System.Collections;

//パーティクルが消えないようにいい感じにストップさせる
public class ShieldParticle : MonoBehaviour
{
    ParticleSystem m_shieldParticle;
    //何回も通さなくするためのもの
    bool isStop;

    void Awake()
    {
        //パーティクル取得
        m_shieldParticle = this.GetComponent<ParticleSystem>();
        isStop = true;
    }

	void Start ()
    {

	}

	void Update ()
    {
        //パーティクルが消える前に停止する
        if (m_shieldParticle.time >= 0.09f && isStop)
        {
            m_shieldParticle.Pause();
            isStop = false;
        }
    }
}
