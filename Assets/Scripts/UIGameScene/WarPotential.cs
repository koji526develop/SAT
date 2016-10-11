using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarPotential : MonoBehaviour {

    //public関数
    public int m_maxWarPotentialValue=500;  //戦力ゲージの総数
    public float m_valueMoveSpeed = 1.0f;   //戦力ゲージ移動スピード

    //private関数
    private float m_warPotentialValue;      //現在の戦力ゲージ（青を基準とする）
    private Slider m_blueSlider;            //青ゲージのコンポーネント
    private Slider m_redSlider;             //赤ゲージのコンポーネント



    //青ゲージに加算するpublic関数
    public void BluePoint(int _getPoint)
    {
        StartCoroutine(SliderMove(_getPoint));
    }

    //赤ゲージに加算するpublic関数
    public void RedPoint(int _getPoint)
    {
        StartCoroutine(SliderMove(-_getPoint));
    }


    // Use this for initialization
    void Start () {

        //青と赤ゲージのコンポーネントをそれぞれ検索。格納する。
        m_blueSlider            = transform.FindChild("BlueSlider").GetComponent<Slider>();
        m_redSlider             = transform.FindChild("RedSlider").GetComponent<Slider>();

        //戦力ゲージの初期化を行う。
        m_blueSlider.maxValue   = m_redSlider.maxValue = m_maxWarPotentialValue;
        m_blueSlider.value      = m_redSlider.value = m_maxWarPotentialValue/2.0f;
        m_warPotentialValue     = m_maxWarPotentialValue / 2.0f;
    }

    //戦力ゲージの移動関数
    private IEnumerator SliderMove(int _getPoint)
    {
        //戦力ゲージの変数を変更
        m_warPotentialValue+=_getPoint;

        //↓戦力ゲージの移動演出を行う。

        //青ゲージ移動時に規定よりまだ超えていないか確認
        while(m_blueSlider.value < m_warPotentialValue)
        {
            //超えていなければ、移動速度に合わせて戦力ゲージを移動する
            m_blueSlider.value += m_valueMoveSpeed;
            m_redSlider.value  -= m_valueMoveSpeed;
            yield return 0;
        }

        //赤ゲージ移動時に規定よりまだ超えていないか確認
        while (m_blueSlider.value > m_warPotentialValue)
        {
            //超えていなければ、移動速度に合わせて戦力ゲージを移動する
            m_blueSlider.value -= m_valueMoveSpeed;
            m_redSlider.value  += m_valueMoveSpeed;
            yield return 0;
        }

        //それぞれのゲージのズレを修正。
        m_blueSlider.value = m_warPotentialValue;
        m_redSlider.value = m_maxWarPotentialValue - m_warPotentialValue;
    }
    
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            BluePoint(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RedPoint(10);
        }
    }
}
