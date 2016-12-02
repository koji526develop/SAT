using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialCardsBehavior : MonoBehaviour
{
    //何番目に使用されるか
    int m_useOrder = 0;
    public int UseOrder
    {
        set
        {
            m_useOrder = value;
        }
    }

    //特殊カードの情報
    SpecialCardButton spButton;
    public SpecialCardButton SpecialButton
    {
        set
        {
            spButton = value;
        }
    }

    private int m_specialUseNum = 0;            //何回特殊カードが使用されたか
    private bool m_usedSpecial = false;         //特殊カードが使用されたか


    void Awake()
    {

    }
    void Start()
    {
        //使用されるカード以外をちょっと小さく
        if (m_useOrder != 1)
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }

    void Update()
    {
        //自分が使用されたら削除
        if(spButton.UsedCard == m_useOrder)
        {
            Destroy(this.gameObject);
        }

        //カードが使われた
        if(m_specialUseNum < spButton.UsedCard)
        {
            m_specialUseNum++;
            m_usedSpecial = true;
        }

        if(m_usedSpecial)
        {
            MoveTo moveTo = gameObject.AddComponent<MoveTo>();
            moveTo.SetMoveTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 12.5f, gameObject.transform.position.z), 0.3f, 0);
            m_usedSpecial = false;

            //次に使用なら大きさも戻す
            if (spButton.UsedCard == m_useOrder - 1)
            {
                ScaleTo scaleTo = gameObject.AddComponent<ScaleTo>();
                scaleTo.SetScaleTo(new Vector3(1.0f, 1.0f, 1.0f), 0.3f, 0);
            }
        }
    }
}
