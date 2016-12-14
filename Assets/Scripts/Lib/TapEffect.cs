using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TapEffect : MonoBehaviour
{
    [SerializeField]
    Camera m_camera;

    public static TapEffect m_Instance;                     //シングルトン

    GameObject[] tapEffect = new GameObject[m_maxEffect];
    const int m_maxEffect = 20;                             //スプライトの最大生成数   
    private bool[] m_isUseEffect = new bool[m_maxEffect];   //タップエフェクトが使用されているか
    ScaleTo[] scaleTo = new ScaleTo[m_maxEffect];

    readonly float m_effectTime = 0.7f;                     //エフェクトの生存時間
    readonly float m_m_maxScale = 0.2f;
    readonly Vector3 m_resetVec3 = new Vector3(0.0f, 0.0f, 0.0f);//タップエフェクトの初期値


    void Awake()
    {
        if (m_Instance == null)
        {
            //このTapEffectをインスタンスとする
            m_Instance = this;
        }
        else
        {
            //今回インスタンス化したTapEffectを破棄
            Destroy(gameObject);
        }

        //シーンを跨いでもインスタンスを破棄しない
        DontDestroyOnLoad(this.gameObject);

        //タップエフェクト用スプライト生成
        for (int i = 0; i < m_maxEffect; i++)
        {
            tapEffect[i] = MyUtility.CreateSprite(transform, "TapEffect", "UI/touch_effect");
            tapEffect[i].layer = LayerMask.NameToLayer("TapEffect");  //レイヤー変更
            tapEffect[i].transform.localScale = m_resetVec3;
            scaleTo[i] = tapEffect[i].AddComponent<ScaleTo>();
            m_isUseEffect[i] = false;
        }

    }

    void Start()
    {

    }

    void Update()
    {
        TouchManager.Update();

        for (int i = 0; i < TouchManager.touchCount; i++)
        {
            TouchInfo touch = TouchManager.GetTouchInfo(i);
            if (touch == TouchInfo.Began)
            {
                var touchPos = m_camera.ScreenToWorldPoint(TouchManager.GetTouchPosition(i) + m_camera.transform.forward * 10);
                for (int j = 0; j < m_maxEffect; j++)
                {
                    //使用されていなかったら
                    if (!m_isUseEffect[j])
                    {
                        tapEffect[j].transform.position = touchPos;
                        scaleTo[j].SetScaleTo(new Vector3(m_m_maxScale, m_m_maxScale, m_m_maxScale), m_effectTime, 0);
                        m_isUseEffect[j] = true;
                        break;
                    }
                }
            }
        }

        //初期化処理
        for (int i = 0; i < m_maxEffect; i++)
        {
            //使用されている　拡大が終わっている
            if(m_isUseEffect[i] && !scaleTo[i].isScale)
            {
                //初期化
                tapEffect[i].transform.localScale = m_resetVec3;
                m_isUseEffect[i] = false;
            }
        }
    }
}
