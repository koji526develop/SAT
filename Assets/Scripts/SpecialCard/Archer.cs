using UnityEngine;
using System.Collections;

public class Archer : SpecialCard {

    private Camera m_mainCamera;
    private GameObject m_obj;
    public GameObject m_Solderobj;

    // Use this for initialization
    public override void Start () {

        //カメラのコンポーネント取得
        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    public override void Update () {

        //レイキャストからFloorにあるオブジェクトをとれば特殊カード実行
        if (m_obj = TouchManager.GetRaycastHitObject(m_mainCamera, 0))
        {

            //フロアであるなら
            if (m_obj.name == "Floor")
            {
                //数値確認用変数（初期設定値0)
                int checkColumn = 0;

                //タグを取得し、何列目かの数値を抽出
                checkColumn = int.Parse(m_obj.tag.Substring(m_obj.tag.Length - 1));

                //BattleManager内をすべて確認
                Transform battleManager = GameObject.Find("BattleManager").transform;
                foreach (Transform childObj in battleManager)
                {
                    //キャラクタータグであるならもしくは、同じ列であるなら
                    if (childObj.tag == "Character" && 
                        checkColumn == childObj.GetComponent<Character>().m_mapColumn)
                    {
                        if (childObj.GetComponent<Character>().status.PlayerID != m_UsedPlayerID)
                        {
                            m_Solderobj = childObj.gameObject;
                        }
                    }
                }
            }
            //対象を削除
            Destroy(m_Solderobj);
            Destroy(this);
        }
   }
}
