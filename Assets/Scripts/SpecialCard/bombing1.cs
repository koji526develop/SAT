using UnityEngine;
using System.Collections;

public class bombing1 : SpecialCard
{
    private GameObject m_obj;

    // Use this for initialization
    void Start()
    {

    }

         // Update is called once per frame
    public override void Update()
    {

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
        
                foreach (Transform childObj in battleManager)
                {
                    //キャラクタータグであるならもしくは、同じ列であるなら
                    if (childObj.tag == "Character" &&
                        checkColumn == childObj.GetComponent<Character>().m_mapColumn)
                    {
                        if (childObj.GetComponent<Character>().status.PlayerID != m_UsedPlayerID)
                        {
                            //対象を削除
                            Destroy(childObj.gameObject);
                            Destroy(this);
                            return;
                        }
                    }
                }
                Destroy(this);
                return;
            }

        }
    }
}