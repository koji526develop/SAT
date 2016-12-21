using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour
{

    readonly float fadeOutTime = 0.5f;

    //タイトルに行く
    public void ChangeToTitle()
    {
        MySceneManager.m_instance.LoadLevel("Title", fadeOutTime);
    }

    //メニューに行く
    public void ChangeToMenu()
    {
        MySceneManager.m_instance.LoadLevel("Menu", fadeOutTime);
    }

    //操作説明に行く
    public void ChangeToOperating()
    {
        MySceneManager.m_instance.LoadLevel("Operating", fadeOutTime);
    }

    //兵士選択画面に行く
    public void ChangeToSelect()
    {
        MySceneManager.m_instance.LoadLevel("Select", fadeOutTime);
    }

    //特殊カード選択に行く
    public void ChangeToSelectSpecial()
    {
        MySceneManager.m_instance.LoadLevel("SelectSpecial", fadeOutTime);
    }

    //チュートリアルに行く
    public void ChangeToTutorial()
    {
        MySceneManager.m_instance.LoadLevel("Tutorial", fadeOutTime);
    }

    //ゲームに行く
    public void ChangeToGame()
    {
        MySceneManager.m_instance.LoadLevel("Game", fadeOutTime);
        //MySceneManager.m_instance.LoadLevel("GameAndUI", 1.0f);
        //MySceneManager.m_instance.LoadLevel("GameAndUI_nozawa", fadeOutTime);
    }

    //リザルトに行く
    public void ChangeToResult()
    {
        MySceneManager.m_instance.LoadLevel("Result", fadeOutTime);
    }
}
