using UnityEngine;
using System.Collections;

//バグあり
//指が一つしか機能しない

public class SoldierChange : SpecialCard {

    private Camera      m_mainCamera;
    private GameObject  m_1stSolider;
    private GameObject  m_2ndSolider;

    int GetPlayerID(GameObject _targetObject)
    {

        return _targetObject.GetComponent<Character>().status.PlayerID;
    }


    // Use this for initialization
    public override void Start()
    {
        //カメラのコンポーネント取得
        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    public override void Update()
    {
        //１個目のコマの処理を行う。
        if (m_1stSolider == null)
        {
            //正しくオブジェクトが処理されているか確認
            if (m_1stSolider = TouchManager.GetRaycastHitObject(m_mainCamera, 0))
            {
                //対象のゲームオブジェクトのプレイヤーIDを取得
                int targetPlayerID = GetPlayerID(m_1stSolider);

                //使用したプレイヤーの兵士ではなければ削除して中断する
                if (targetPlayerID != m_UsedPlayerID)
                {
                    m_1stSolider = null;
                    return;
                }
            }
        }


        //２個目のコマの処理を行う。
        else if (m_2ndSolider == null)
        {
            //対象にタッチしていれば、２個目のオブジェクトに格納
            if (m_2ndSolider = TouchManager.GetRaycastHitObject(m_mainCamera, 0))
            {
                //対象のゲームオブジェクトのプレイヤーIDを取得
                int targetPlayerID = GetPlayerID(m_2ndSolider);


                //同一オブジェクトか使用したプレイヤーの兵士ではなければ削除して中断する
                if (m_2ndSolider == m_1stSolider ||
                    targetPlayerID != m_UsedPlayerID)
                {
                    m_2ndSolider = null;
                    return;
                }

            }

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
