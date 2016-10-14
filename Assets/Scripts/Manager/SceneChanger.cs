using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    //タイトル
    public void ChangeToMenu()
    {
        MySceneManager.m_instance.LoadLevel("Menu", 1.0f);
    }

    //メニュー
    public void ChangeToTitle()
    {
        MySceneManager.m_instance.LoadLevel("Title", 1.0f);
    }

    //操作説明
    public void ChangeToOperating()
    {
        MySceneManager.m_instance.LoadLevel("Operating", 1.0f);
    }

    //ゲーム
    public void ChangeToGame()
    {
        MySceneManager.m_instance.LoadLevel("Main", 1.0f);
    }

    //リザルト
    public void ChangeToResukt()
    {
        MySceneManager.m_instance.LoadLevel("Result", 1.0f);
    }
}
