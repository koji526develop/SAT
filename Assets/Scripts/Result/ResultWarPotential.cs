using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultWarPotential : MonoBehaviour
{
    WarPotential warPotential;

    //private関数
    private float m_warPotentialValue;          //現在の戦力ゲージ（青を基準とする）
    private Slider m_blueSlider;                //青ゲージのコンポーネント
    private Slider m_redSlider;                 //赤ゲージのコンポーネント
    private int m_maxWarPotentialValue = MyUtility.MAX_WARPOTENTIAL;   //戦力ゲージの総数

    void Start()
    {
        //青と赤ゲージのコンポーネントをそれぞれ検索。格納する。
        m_blueSlider = transform.FindChild("BlueSlider").GetComponent<Slider>();
        m_redSlider = transform.FindChild("RedSlider").GetComponent<Slider>();

        //戦力ゲージの最大数を入れる
        m_blueSlider.maxValue = m_redSlider.maxValue = m_maxWarPotentialValue;
        //最終的なゲージの値を持ってくる
        m_warPotentialValue = WarPotential.lastWarPotential;

        //それぞれの結果を代入
        m_blueSlider.value = m_warPotentialValue;
        m_redSlider.value = m_maxWarPotentialValue - m_warPotentialValue;

    }

    void Update()
    {

    }
}
