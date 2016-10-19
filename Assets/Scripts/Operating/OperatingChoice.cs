using UnityEngine;
using System.Collections;

public class OperatingChoice : MonoBehaviour
{
    Sprite[] m_OperatingChoiceSprite = new Sprite[2];
    SpriteRenderer m_DisplayChoiceSpriteRenderer;

    int m_SpriteNum;
    private int m_PrevNum;

    public int SpriteNum
    {
        set
        {
            m_SpriteNum = value;
        }
    }

    GameObject m_FlickObj;
    OperatingFlick m_flick;

    void Start()
    {
        m_FlickObj = GameObject.Find("OperatingSprite");
        m_flick = m_FlickObj.GetComponent<OperatingFlick>();
        m_PrevNum = 0;
        m_OperatingChoiceSprite[0] = Resources.Load<Sprite>("Image/operatingNoneChoice");
        m_OperatingChoiceSprite[1] = Resources.Load<Sprite>("Image/operatingChoice");
        if(m_SpriteNum == 0)
        {
            m_DisplayChoiceSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            m_DisplayChoiceSpriteRenderer.sprite = m_OperatingChoiceSprite[1];
        }
    }

    void Update()
    {
        if (m_PrevNum != m_flick.DisplayNum)
        {
            if (m_SpriteNum == m_flick.DisplayNum)
            {
                m_DisplayChoiceSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                m_DisplayChoiceSpriteRenderer.sprite = m_OperatingChoiceSprite[1];
            }
            if (m_SpriteNum == m_PrevNum)
            {
                m_DisplayChoiceSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                m_DisplayChoiceSpriteRenderer.sprite = m_OperatingChoiceSprite[0];
            }
            m_PrevNum = m_flick.DisplayNum;
        }
    }
}
