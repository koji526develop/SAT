using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour
{

    //タイトルに行く
    public void ChangeToTitle()
    {
        MySceneManager.m_instance.LoadLevel("Title", 1.0f);
    }

    //操作説明に行く
    public void ChangeToOperating()
    {
        MySceneManager.m_instance.LoadLevel("Operating", 1.0f);
    }

    //兵士選択画面に行く
    public void ChangeToSelect()
    {
        MySceneManager.m_instance.LoadLevel("Select", 1.0f);
    }

    //特殊カード選択に行く
    public void ChangeToSelectSpecial()
    {
        MySceneManager.m_instance.LoadLevel("SelectSpecial", 1.0f);
    }

    //ゲームに行く
    public void ChangeToGame()
    {
        //MySceneManager.m_instance.LoadLevel("Game", 1.0f);
        MySceneManager.m_instance.LoadLevel("GameAndUI", 1.0f);
    }

    //リザルトに行く
    public void ChangeToResult()
    {
        MySceneManager.m_instance.LoadLevel("Result", 1.0f);
    }
}
