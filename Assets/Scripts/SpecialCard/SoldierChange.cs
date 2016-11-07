using UnityEngine;
using System.Collections;

//バグあり
//敵味方関係なく入れ替わる
//指が一つしか機能しない

public class SoldierChange : SpecialCard {

    private Camera m_mainCamera;
    private GameObject m_1stSolider;
    private GameObject m_2ndSolider;

    // Use this for initialization
    public override void Start()
    {
        //カメラのコンポーネント取得
        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    public override void Update()
    {
        //はじめのコマ確認中
        if (m_1stSolider == null)
        {
            //対象にタッチしていれば、１個目のオブジェクトに格納
            m_1stSolider = TouchManager.GetRaycastHitObject(m_mainCamera, 0);
        }

        //２個目のコマ選択中
        else if (m_2ndSolider == null)
        {
            //対象にタッチしていれば、２個目のオブジェクトに格納
            m_2ndSolider = TouchManager.GetRaycastHitObject(m_mainCamera, 0);

            //オブジェクトが同一なら処理はキャンセル
            if(m_2ndSolider == m_1stSolider)
            {
                m_2ndSolider = null;
                return;
            }

            //正しく選択されていれば
            if (m_1stSolider != null && m_2ndSolider != null)
            {
                Vector3 tmpPos;
                int tmpColumn;

                //座標と列番号定番のやり方で入れ替え
                tmpPos = m_1stSolider.transform.position;
                tmpColumn = m_1stSolider.GetComponent<Character>().mapColumn;

                m_1stSolider.transform.position = m_2ndSolider.transform.position;
                m_1stSolider.GetComponent<Character>().mapColumn = m_2ndSolider.GetComponent<Character>().mapColumn;

                m_2ndSolider.transform.position = tmpPos;
                m_2ndSolider.GetComponent<Character>().mapColumn = tmpColumn;


                //最後に自身を削除
                Destroy(this);

            }
        }


    }
}
