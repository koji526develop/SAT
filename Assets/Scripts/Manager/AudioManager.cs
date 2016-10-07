//===============================
//
//
//オーディオを管理するクラス
/// <summary>
/// Audio manager.
/// </summary>
/// 使い方
/// すきなスクリプトから１行指定すればならせます
/// AudioManager.Instance.Play ("BGMファイル名");
/// 
//===============================

using UnityEngine;
//オーディオで使うものの読み込み
using UnityEngine.Audio;
using System;
//
using System.Linq;
using System.Collections;
//ジェネリックコレクションを使用する為に宣言します
using System.Collections.Generic;

//シングルトンクラスを継承
public class AudioManager : MonoBehaviour
{
    static public AudioManager m_instance;//シングルトン　マネージャー変数
    //======
    //グローバル変数
    //======
    //デバックモード
    public bool m_debugMode = true;

    //========================================================================
    //BGM用
    //BGM用オーディオソース
    //クロスフェードに対応させるため二つ用意
    private List<AudioSource> m_bgmSource = null;
    /// 再生可能なBGM(AudioClip)のリストです。
    /// 実行時にBGMファイルから自動で読み込む
    private Dictionary<string, AudioClip> m_audioClipDict = null;

    /// 現在再生中のAudioSource
    /// FadeOut中のものは除く
    [NonSerialized]
    public AudioSource m_currentAudioSource = null;
    //========================================================================


    //========================================================================
    //SE用
    //SEのファイルを格納する
    public List<AudioClip> m_seList = null;
    //総読み込みファイル数
    //public int MaxSE = 10;
    //SE用オーディオソース
    private List<AudioSource> m_seSources = null;
    //Dictionary<Keyの型,値の型:>:キーを指定すると値を取得することができるコレクション
    private Dictionary<string, AudioClip> m_seDict = null;
    [NonSerialized]
    public AudioSource m_currentSe = null;
    //========================================================================


    //========================================================================
    //Common
    /// BGM再生音量
    /// 次回フェードインから適用されます。
    /// 再生中の音量を変更するには、m_currentAudioSource.Volumeを変更してください。
    [Range(0f, 1f)]
    public float m_targetVolume = 1.0f;

    /// フェードイン、フェードアウトにかかる時間です。
    public float m_timeToFade = 2.0f;

    /// フェードインとフェードアウトの実行を重ねる割合です。
    /// 0を指定すると、完全にフェードアウトしてからフェードインを開始します。
    /// 1を指定すると、フェードアウトとフェードインを同時に開始します。
    [Range(0f, 1f)]
    public float m_crossFadeRatio = 1.0f;

    /// コルーチン中断に使用
    private IEnumerator fadeOutCoroutine;
    /// <summary>
    /// コルーチン中断に使用
    /// </summary>
    private IEnumerator fadeInCoroutine;
    //========================================================================

    /// FadeOut中、もしくは再生待機中のAudioSource
    public AudioSource SubAudioSource
    {
        get
        {
            //m_bgmSourcesのうち、m_currentAudioSourceでない方を返す
            if (this.m_bgmSource == null)
            {
                return null;
            }
            //全ての要素を検索
            foreach (AudioSource s in this.m_bgmSource)
            {
                //現在の要素と同じで無ければ
                if (s != this.m_currentAudioSource)
                {
                    return s;
                }
            }
            return null;
        }
    }

    //クロスフェードのためのsubAudioSource
    public AudioSource SubSESource
    {
        get
        {
            //オーディオソースが存在しなかったら
            if (this.m_seSources == null)
            {
                return null;
            }
            //全ての要素を検索
            foreach (AudioSource s in this.m_seSources)
            {
                //現在のSEと同じじゃ無ければ
                if (s != this.m_currentSe)
                {
                    //オーディオソースを返す
                    return s;
                }
            }
            return null;
        }

    }

    //一番最初に呼び出す
    public void Awake()
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

        //オブジェクトをシーン間え破棄しない
        DontDestroyOnLoad(this.gameObject);

        //オーディオリスナーの作成
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            //オーディオリスナーの取得
            this.gameObject.AddComponent<AudioListener>();
        }
        //オーディオソースの作成
        //クロスフェードをするためにオーディオソースをふたつ用意
        this.m_bgmSource = new List<AudioSource>();
        this.m_bgmSource.Add(this.gameObject.AddComponent<AudioSource>());
        this.m_bgmSource.Add(this.gameObject.AddComponent<AudioSource>());
        //全ての要素のオプションを変更
        foreach (AudioSource s in this.m_bgmSource)
        {
            //開始時しかならない設定を切る
            s.playOnAwake = false;
            //初期のvolumeは０
            s.volume = 0f;
            //ループ再生はオン
            s.loop = true;
        }

        //[Resources/Audio/BGM]フォルダからBGMを探す
        this.m_audioClipDict = new Dictionary<string, AudioClip>();
        //LoadAllで引数のパスの中のものを全て探して加える
        foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Audio/BGM"))
        {
            this.m_audioClipDict.Add(bgm.name, bgm);
        }


        //SEオーディオソースのリスト作成
        this.m_seSources = new List<AudioSource>();
        //SEも同様に二つ用意
        this.m_seSources.Add(this.gameObject.AddComponent<AudioSource>());
        this.m_seSources.Add(this.gameObject.AddComponent<AudioSource>());
        //全ての要素のオプションを変更
        foreach (AudioSource s_se in this.m_seSources)
        {
            //開始時しかならない設定を切る
            s_se.playOnAwake = false;
            s_se.volume = 1f;
            //ループ再生はオフ
            s_se.loop = false;
        }

        //[Resources/Audio/SE]フォルダからSEを探す
        this.m_seDict = new Dictionary<string, AudioClip>();
        //オーディオクリップをSEフォルダから読み込み
        foreach (AudioClip se in Resources.LoadAll<AudioClip>("Audio/SE"))
        {
            this.m_seDict.Add(se.name, se);
        }
    }


    /*
	 *関数名 :playSE
	 *内容	:SEを流す
	 *引数	:setName:SEファイル名
	 *戻り値 :
	*/
    public void PlaySE(string seName)
    {
        //Dictionary内のファイル名を探す
        if (!this.m_seDict.ContainsKey(seName))
        {
            Debug.LogError(string.Format("SE名[{0}]が見つかりません", seName));
            return;
        }
        //もしオーディオソースが存在し、クリップ名が検索したいSE名と同じだったら
        //		if((this.m_currentSe != null) && (this.m_currentSe.clip == this.m_seDict[seName]))
        //		{
        //			//早期リターン
        //			return;
        //		}
        //現在のSEを止める
        //this.StopSE ();
        //現在のオーディオソースに使っていないソースを入れる
        this.m_currentSe = this.SubSESource;
        //クリップ名を読み込んで来たSE名に変更
        this.m_currentSe.clip = this.m_seDict[seName];
        //SEを鳴らす
        this.m_currentSe.Play();
    }

    /*
	 *関数名 :StopSE
	 *内容	:全リストを検索してSEを止める
	 *引数	:
	 *戻り値 :
	*/
    public void StopSE()
    {
        //全ての要素を検索しストップする
        foreach (AudioSource s in this.m_seSources)
        {
            s.Stop();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    ///以下BGM関連の関数
    /*
	 *関数名 :PlayBGM
	 *内容	:BGMの再生
	 *引数	:bgm名
	 *戻り値 :
	*/
    public void PlayBGM(string bgmName)
    {
        if (!this.m_audioClipDict.ContainsKey(bgmName))
        {
            Debug.LogError(string.Format("BGM名[{0}]が見つかりません。", bgmName));
            return;
        }

        if ((this.m_currentAudioSource != null)
            && (this.m_currentAudioSource.clip == this.m_audioClipDict[bgmName]))
        {
            //すでに指定されたBGMを再生中
            return;
        }

        //クロスフェード中なら中止
        stopFadeOut();
        stopFadeIn();

        //再生中のBGMをフェードアウト開始
        this.StopBGM();

        float fadeInStartDelay = this.m_timeToFade * (1.0f - this.m_crossFadeRatio);

        //BGM再生開始
        this.m_currentAudioSource = this.SubAudioSource;
        this.m_currentAudioSource.clip = this.m_audioClipDict[bgmName];
        this.fadeInCoroutine = fadeIn(this.m_currentAudioSource, this.m_timeToFade, this.m_currentAudioSource.volume, this.m_targetVolume, fadeInStartDelay);
        StartCoroutine(this.fadeInCoroutine);

    }

    /*
	 *関数名 :StopBGM
	 *内容	:BGMを止める
	 *引数	:
	 *戻り値 :
	*/
    public void StopBGM()
    {
        if (this.m_currentAudioSource != null)
        {
            this.fadeOutCoroutine = fadeOut(this.m_currentAudioSource, this.m_timeToFade, this.m_currentAudioSource.volume, 0f);
            StartCoroutine(this.fadeOutCoroutine);
        }
    }

    /// <summary>
    /// BGMをただちに停止します。
    /// </summary>
    public void StopImmediately()
    {
        this.fadeInCoroutine = null;
        this.fadeOutCoroutine = null;
        foreach (AudioSource s in this.m_bgmSource)
        {
            s.Stop();
        }
        this.m_currentAudioSource = null;
    }

    /// <summary>
    /// BGMをフェードインさせながら再生を開始します。
    /// </summary>
    /// <param name="bgm">AudioSource</param>
    /// <param name="m_timeToFade">フェードインにかかる時間</param>
    /// <param name="fromVolume">初期音量</param>
    /// <param name="toVolume">フェードイン完了時の音量</param>
    /// <param name="delay">フェードイン開始までの待ち時間</param>
    private IEnumerator fadeIn(AudioSource bgm, float m_timeToFade, float fromVolume, float toVolume, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }


        float startTime = Time.time;
        bgm.Play();
        while (true)
        {
            float spentTime = Time.time - startTime;
            if (spentTime > m_timeToFade)
            {
                bgm.volume = toVolume;
                this.fadeInCoroutine = null;
                break;
            }

            float rate = spentTime / m_timeToFade;
            float vol = Mathf.Lerp(fromVolume, toVolume, rate);
            bgm.volume = vol;
            yield return null;
        }
    }

    /// <summary>
    /// BGMをフェードアウトし、その後停止します。
    /// </summary>
    /// <param name="bgm">フェードアウトさせるAudioSource</param>
    /// <param name="m_timeToFade">フェードアウトにかかる時間</param>
    /// <param name="fromVolume">フェードアウト開始前の音量</param>
    /// <param name="toVolume">フェードアウト完了時の音量</param>
    private IEnumerator fadeOut(AudioSource bgm, float m_timeToFade, float fromVolume, float toVolume)
    {
        float startTime = Time.time;
        while (true)
        {
            float spentTime = Time.time - startTime;
            if (spentTime > m_timeToFade)
            {
                bgm.volume = toVolume;
                bgm.Stop();
                this.fadeOutCoroutine = null;
                break;
            }

            float rate = spentTime / m_timeToFade;
            float vol = Mathf.Lerp(fromVolume, toVolume, rate);
            bgm.volume = vol;
            yield return null;
        }
    }

    /// <summary>
    /// フェードイン処理を中断します。
    /// </summary>
    private void stopFadeIn()
    {
        if (this.fadeInCoroutine != null)
            StopCoroutine(this.fadeInCoroutine);
        this.fadeInCoroutine = null;

    }

    /// <summary>
    /// フェードアウト処理を中断します。
    /// </summary>
    private void stopFadeOut()
    {
        if (this.fadeOutCoroutine != null)
        {
            StopCoroutine(this.fadeOutCoroutine);
        }

        this.fadeOutCoroutine = null;
    }




    /// デバッグ用操作パネルを表示
    public void OnGUI()
    {
        if (this.m_debugMode)
        {
            //AudioClipが見つからなかった場合
            if (this.m_audioClipDict.Count == 0)
            {
                GUI.Box(new Rect(10, 10, 200, 50), "Audio Manager(Debug Mode)");
                GUI.Label(new Rect(10, 35, 80, 20), "Audio clips not found.");
                return;
            }

            //枠
            GUI.Box(new Rect(10, 10, 200, 150 + this.m_audioClipDict.Count * 25), "BGM Manager(Debug Mode)");
            int i = 0;
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Target Volume : " + this.m_targetVolume.ToString("0.00"));
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Time to Fade : " + this.m_timeToFade.ToString("0.00"));
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Crossfade Ratio : " + this.m_crossFadeRatio.ToString("0.00"));

            i = 0;
            //再生ボタン
            foreach (AudioClip bgm in this.m_audioClipDict.Values)
            {
                bool currentBgm = (this.m_currentAudioSource != null && this.m_currentAudioSource.clip == this.m_audioClipDict[bgm.name]);

                if (GUI.Button(new Rect(20, 100 + i * 25, 40, 20), "Play"))
                {
                    this.PlayBGM(bgm.name);
                }
                string txt = string.Format("[{0}] {1}", currentBgm ? "X" : "_", bgm.name);
                GUI.Label(new Rect(70, 100 + i * 25, 1000, 20), txt);

                i++;
            }

            //停止ボタン
            if (GUI.Button(new Rect(20, 100 + i++ * 25, 180, 20), "Stop"))
            {
                this.StopBGM();
            }
            if (GUI.Button(new Rect(20, 100 + i++ * 25, 180, 20), "Stop Immediately"))
            {
                this.StopImmediately();
            }
        }

        if (this.m_debugMode)
        {
            int j = 0;
            foreach (AudioClip se in this.m_seDict.Values)
            {
                bool ism_currentSe = (this.m_currentSe != null && this.m_currentSe.clip == this.m_seDict[se.name]);

                if (GUI.Button(new Rect(320, 100 + j * 25, 40, 20), "Play"))
                {
                    this.PlaySE(se.name);
                }
                string txt = string.Format("[{0}] {1}", ism_currentSe ? "X" : "_", se.name);
                GUI.Label(new Rect(370, 100 + j * 25, 1000, 20), txt);
                j++;
            }
            if (GUI.Button(new Rect(320, 200 + j * 25, 180, 20), "Stop"))
            {
                this.StopSE();
            }

        }
    }
}




