using UnityEngine;
using System.Collections;

public class OperatinFlick : MonoBehaviour
{
    public Sprite[] m_OperatingSprite = new Sprite[6];
    SpriteRenderer m_DisplaySpriteRenderer;
    private int m_DisplaySpriteNum;
    readonly int MAX_FLICK_X = Screen.width*1/4;

    void Start()
    {
        m_DisplaySpriteNum = 0;
    }

    void Update()
    {
        Debug.Log(MAX_FLICK_X);
        Debug.Log(TouchManager.GetTouchMoveDistanceX(0));

        TouchInfo touch = TouchManager.GetTouchInfo(0);

        if (touch == TouchInfo.Ended)
        {
            if (TouchManager.GetTouchMoveDistanceX(0) > MAX_FLICK_X)
            {
                m_DisplaySpriteNum++;
                ChangeSprite();
            }
            else if (TouchManager.GetTouchMoveDistanceX(0) < MAX_FLICK_X)
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
        else if (m_DisplaySpriteNum > 5)
        {
            m_DisplaySpriteNum = 5;
        }
        m_DisplaySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_DisplaySpriteRenderer.sprite = m_OperatingSprite[m_DisplaySpriteNum];
        Debug.Log("スプライト変更");
    }
}
