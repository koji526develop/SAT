using UnityEngine;
using System.Collections;

public class OperatingFlick : MonoBehaviour
{
    Sprite[] m_OperatingSprite = new Sprite[MAX_SPRITE_NUM];
    SpriteRenderer m_DisplaySpriteRenderer;
    int m_DisplaySpriteNum;
    const int MAX_SPRITE_NUM = 6;

    public int DisplayNum
    {
        get
        {
            return m_DisplaySpriteNum;
        }
    }

    readonly int MAX_FLICK_X = Screen.width*1/4;

    void Start()
    {
        m_DisplaySpriteNum = 0;
        for (int i = 1; i < MAX_SPRITE_NUM+1; i++) 
        {
            m_OperatingSprite[i-1] = Resources.Load<Sprite>("Image/operating"+i.ToString());
        }
    }

    void Update()
    {
        TouchInfo touch = TouchManager.GetTouchInfo(0);

        if (touch == TouchInfo.Ended)
        {
            if (TouchManager.GetTouchMoveDistanceX(0) < -MAX_FLICK_X)
            {
                m_DisplaySpriteNum++;
                ChangeSprite();
            }
            else if (TouchManager.GetTouchMoveDistanceX(0) > MAX_FLICK_X)
            {
                m_DisplaySpriteNum--;
                ChangeSprite();
            }
        }
    }


    void ChangeSprite()
    {
        if (m_DisplaySpriteNum < 0)
        {
            m_DisplaySpriteNum = 0;
        }
        else if (m_DisplaySpriteNum >= m_OperatingSprite.Length)
        {
            m_DisplaySpriteNum = m_OperatingSprite.Length-1;
        }
        m_DisplaySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_DisplaySpriteRenderer.sprite = m_OperatingSprite[m_DisplaySpriteNum];
    }
}
