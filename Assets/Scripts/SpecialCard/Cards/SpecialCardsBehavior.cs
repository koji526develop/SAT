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

    SpecialCardButton spButton;
    public SpecialCardButton SpecialButton
    {
        set
        {
            spButton = value;
        }
    }


    void Awake()
    {

    }
    void Start()
    {
        if (m_useOrder != 1)
        {
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }

    void Update()
    {
        //使用されたら
        if(spButton.UsedCard == m_useOrder)
        {
            Destroy(this.gameObject);
        }

        //次に使用なら
        if(spButton.UsedCard == m_useOrder-1)
        {
            ScaleTo scaleTo = gameObject.AddComponent<ScaleTo>();
            scaleTo.SetScaleTo(new Vector3(1.0f, 1.0f, 1.0f), 0.5f, 0);
            //MoveTo moveTo = gameObject.AddComponent<MoveTo>();
            //moveTo.SetMoveTo()
        }
        //まだ
        else
        {

        }
    }
}
