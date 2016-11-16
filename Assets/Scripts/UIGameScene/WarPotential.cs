using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarPotential : MonoBehaviour {

    //public関数

    public float m_valueMoveSpeed = 1.0f;   //戦力ゲージ移動スピード

    //private関数
    private int m_maxWarPotentialValue = MyUtility.MAX_WARPOTENTIAL;  //戦力ゲージの総数
    private ScoreManager m_socreManager;    //スコアマネージャーのコンポーネント
    private Slider m_blueSlider;            //青ゲージのコンポーネント
    private Slider m_redSlider;             //赤ゲージのコンポーネント

    public static float lastWarPotential;   //最終的な戦力ゲージ

    // Use this for initialization
    void Start() {

        m_socreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        //青と赤ゲージのコンポーネントをそれぞれ検索。格納する。
        m_blueSlider = transform.FindChild("BlueSlider").GetComponent<Slider>();
        m_redSlider = transform.FindChild("RedSlider").GetComponent<Slider>();

        //戦力ゲージの初期化を行う。
        m_blueSlider.maxValue = m_redSlider.maxValue = m_maxWarPotentialValue;
        m_blueSlider.value = m_redSlider.value = m_maxWarPotentialValue / 2.0f;
    }

    void Update()
    {

        float m_localWarPotentialValue=m_socreManager.m_Score;      //現在の戦力ゲージ（青を基準とする）

        lastWarPotential = m_localWarPotentialValue;

        if (m_blueSlider.value < m_localWarPotentialValue)
        {
            //           //超えていなければ、移動速度に合わせて戦力ゲージを移動する
            m_blueSlider.value += m_valueMoveSpeed;
            m_redSlider.value -= m_valueMoveSpeed;

            if (m_blueSlider.value >= m_localWarPotentialValue)
            {
                m_blueSlider.value = m_localWarPotentialValue;
                m_redSlider.value = m_maxWarPotentialValue - m_localWarPotentialValue;
            }
            return;
        }

        else if (m_blueSlider.value > m_localWarPotentialValue)
        {
            m_blueSlider.value -= m_valueMoveSpeed;
            m_redSlider.value += m_valueMoveSpeed;

            if (m_blueSlider.value <= m_localWarPotentialValue)
            {
                m_blueSlider.value = m_localWarPotentialValue;
                m_redSlider.value = m_maxWarPotentialValue - m_localWarPotentialValue;
            }
            return;
        }

    }



 //   //戦力ゲージの移動関数
 //   private IEnumerator SliderMove(int _getPoint)
 //   {
 //       //戦力ゲージの変数を変更
 //       m_warPotentialValue=_getPoint;

 //       //↓戦力ゲージの移動演出を行う。

 //       //青ゲージ移動時に規定よりまだ超えていないか確認
 //       while(m_blueSlider.value < m_warPotentialValue)
 //       {
 //           //超えていなければ、移動速度に合わせて戦力ゲージを移動する
 //           m_blueSlider.value += m_valueMoveSpeed;
 //           m_redSlider.value  -= m_valueMoveSpeed;
 //           yield return 0;
 //       }

 //       //赤ゲージ移動時に規定よりまだ超えていないか確認
 //       while (m_blueSlider.value > m_warPotentialValue)
 //       {
 //           //超えていなければ、移動速度に合わせて戦力ゲージを移動する
 //           m_blueSlider.value -= m_valueMoveSpeed;
 //           m_redSlider.value  += m_valueMoveSpeed;
 //           yield return 0;
 //       }

 //       //それぞれのゲージのズレを修正。
 //       m_blueSlider.value = m_warPotentialValue;
 //       m_redSlider.value = m_maxWarPotentialValue - m_warPotentialValue;

 //       lastWarPotential = m_warPotentialValue;
 //   }
    
	//// Update is called once per frame
	//void Update ()
 //   {
 //       if (m_warPotentialValue != m_socreManager.m_Score)
 //       {
 //           StartCoroutine(SliderMove(m_socreManager.m_Score));
 //       }
 //   }
}
