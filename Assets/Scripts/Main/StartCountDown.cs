using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//ゲーム始めのカウントダウン

public class StartCountDown : MonoBehaviour
{
    private float m_alfa;
    private float m_nowTime;         //現在の時間
    private float m_fadeTime = 1.0f;        //フェードインにかける時間
    private bool m_isFade = false;          //フェードインをするか
    private Color m_color;

    public bool FadeFlag
    {
        set
        {
            m_isFade = value;
        }
        get
        {
            return m_isFade;
        }
    }


    void Awake()
    {
        
    }

    void Start()
    {
        m_color = GetComponent<Image>().color;
        m_alfa = 0.0f;
        m_nowTime = 0.0f;
    }

    void Update()
    {
        if(m_isFade)
        {
            FadeIn();
        }
    }

    //画像に透過処理をする
    void FadeIn()
    {
        if (m_nowTime <= m_fadeTime)
        {
            m_alfa = Mathf.Lerp(1.0f, 0.0f, m_nowTime / m_fadeTime);
            m_color.a = m_alfa;

            m_nowTime += Time.deltaTime;

            GetComponent<Image>().color = m_color;
        }
        else
        {
            m_isFade = false;
        }
    }
}
