﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;// Unity5.3から画面遷移として必要なもの

/********************************
シーンマネージャー
*********************************/
public class MySceneManager : MonoBehaviour {
    static public MySceneManager m_instance;//シングルトン　マネージャー変数
    public string m_nextScene;              //次のシーンの名前
    private AsyncOperation m_async;             //シーン情報

	public float Progress
	{
		get
		{
			return m_async.progress;
		}
	}
    private Color fadeColor = Color.black;      //フェードイン・アウトの黒画像

    private float   m_nowTime = 0.0f;           //時間
    private float   m_intervalTime = 0.0f;      //どのぐらいの時間かけてフェードイン・アウトさせるか保存
    private float   m_fadeAlpha = 0.0f;         //アルファ値
    private bool    m_isFade=false;             //フェードインアウトスイッチ

    static public bool m_SceneChangeFlag = true;  //シーンチェンジしていいかのフラグ
   
    // Use this for initialization
    void Awake()
    {
        //シングルトンを作成
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //外部からシーン遷移関数（これをメインで使う）
    public void LoadLevel(string _nextSceneName,float _fadeTime)
    {
        if(m_SceneChangeFlag)
        {
            //フェードスタートと初期化を行う。
            this.m_isFade = true;
            this.m_nowTime = 0.0f;

            m_nextScene = _nextSceneName;
            m_intervalTime = _fadeTime;
            StartCoroutine(TransScene());

            m_SceneChangeFlag = false;
        }
    }

    //非同期でフェードイン・アウトと画面遷移の処理を行う。

	private IEnumerator FadeIn()
	{
		//フェードスタートと初期化を行う。
		this.m_isFade = true;
		this.m_nowTime = 0.0f;

		//フェードインしたかどうか確認する。
		while (m_nowTime <= m_intervalTime)
		{
			this.m_fadeAlpha = Mathf.Lerp(1.0f, 0.0f, m_nowTime / m_intervalTime);

			m_nowTime += Time.deltaTime;
			yield return 0;
		}
	}

	private IEnumerator FadeOut()
	{
		//フェードスタートと初期化を行う。
		this.m_isFade = true;
		this.m_nowTime = 0.0f;

		//フェードアウトしたか確認
		while (m_nowTime <= m_intervalTime) {
			this.m_fadeAlpha = Mathf.Lerp (0.0f, 1.0f, m_nowTime / m_intervalTime);

			m_nowTime += Time.deltaTime;
			yield return 0;
		}
	}

	private IEnumerator ChaeckChangeSceneLoad()
	{
		//非同期で画面遷移のロードが終わっているかどうか、(0.9が完了？らしい）
		while (this.m_async.progress < 0.9f)
		{
			yield return 0;
		}
	}

	//画面遷移を非同期で処理を開始させる。
    private IEnumerator TransScene()
	{
		// フェードアウト開始
		yield return StartCoroutine (FadeOut ());

		// ロード画面に遷移
		SceneManager.LoadScene("Loading");
		yield return 0;

		// 次のシーンを読み込んでおく
		this.m_async = SceneManager.LoadSceneAsync (m_nextScene);
		// 自動で画面遷移をオフに
		this.m_async.allowSceneActivation = false;  

		// フェードイン開始
		yield return StartCoroutine (FadeIn ());

		// 非同期で画面遷移のロードが終わっているかどうかのチェック
		yield return StartCoroutine (ChaeckChangeSceneLoad ());

		yield return StartCoroutine (FadeOut ());

        // 画面遷移を許可する
        this.m_async.allowSceneActivation = true;

		yield return StartCoroutine (FadeIn ());

        //暗転処理終了
        m_SceneChangeFlag = true;
        this.m_nextScene = "";
        this.m_intervalTime = 0.0f;
		this.m_isFade = false;
    }

    public void OnGUI()
    {
        //もし暗転中のフラグがたっていたら
        if (this.m_isFade)
        {
            //透明度を変更
            this.fadeColor.a = this.m_fadeAlpha;
            //色を変更
            GUI.color = this.fadeColor;
            //暗転の画像を表す
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    void Update()
    {
        //デバッグで画面遷移の内容を確認する
        if (m_nextScene != "")
        {
            //Debug.Log(" [次のシーンの名前->" + m_nextScene + "] [読み込み進捗度->" + this.m_async.progress + "]");
        }
    }
}
