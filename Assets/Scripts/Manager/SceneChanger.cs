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

    //ゲームに行く
    public void ChangeToGame()
    {
        MySceneManager.m_instance.LoadLevel("Game", 1.0f);
    }

    //リザルトに行く
    public void ChangeToResukt()
    {
        MySceneManager.m_instance.LoadLevel("Result", 1.0f);
    }
}
