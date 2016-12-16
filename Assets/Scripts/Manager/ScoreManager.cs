using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{

    private int m_maxScore = MyUtility.MAX_WARPOTENTIAL; //最大ゲージ数値
	private int m_minScore=MyUtility.MIN_WARPOTENTIAL;//最小ゲージ数値
    public int m_Score;    //現在のスコア数値

    private int m_totalPlayer = 2;  //総プレイヤー数
    private int m_totalColumn = 6;  //列

    private int[,] m_countSpawner;  //それぞれの出現させた回数
    private int[] m_countArea = { 0, 1, 3, 5, 7 };   //フェーズの推移
    private int[] m_scoreArea = { 10, 20, 40, 70, 100 };    //フェーズごとの得点情報
    private int m_countAreaStage;

    private float[] m_pointBouns = new float[2];
    private float[] m_pointBounsTime = new float[2];

    private const float DECREASE_TIME = 0.1f;   //スコアが減っていく時間間隔
    private float m_DecreaseTime = DECREASE_TIME;

  
    private PointViewScript m_pointView;
    void Awake()
    {
        m_pointView = GameObject.Find("PointView").GetComponent<PointViewScript>();
        //スコアの初期設定を行う。
        m_Score = m_maxScore / m_totalPlayer;

        //動的にスポーン回数を記憶するためのプレイヤー数と列分の配列を確保
        m_countSpawner = new int[m_totalPlayer, m_totalColumn];


        for (int i = 0; i < m_totalPlayer; i++)
        {
            for (int j = 0; j < m_totalColumn; j++)
            {
                m_countSpawner[i, j] = 0;
            }
        }

        for (int i = 0; i < m_totalPlayer; i++)
        {
            m_pointBouns[i] = 0.0f;
            m_pointBounsTime[i] = 0.0f;
        }
        m_countAreaStage = m_countArea.Length;

    }

    public void SpawnerCount(int _playerID, int _Column)
    {
        //配列に合わせるためすべて-1にする（修正したい）
        _playerID--;
        _Column--;

        //出現させた回数カウント
        m_countSpawner[_playerID, _Column]++;
        //ScoremDecrease関数の処理上制限しました　yoshida
        if (m_countSpawner[_playerID, _Column] >= 7) m_countSpawner[_playerID, _Column] = 7;
    }

    public int GetPointLevel(int _playerID, int _Column)
    {
        ////配列に合わせるためすべて-1にする（修正したい）
        _playerID--;
        _Column--;

        int checkCount = m_countSpawner[_playerID, _Column];
        if (m_countSpawner[_playerID, _Column] <= 0) m_countSpawner[_playerID, _Column] = 0;

        int allCountStage = m_countAreaStage - 1;

        for (int i = 0; i < m_countAreaStage; i++)
        {
            if (m_countArea[i] <= checkCount)
            {
                allCountStage = i;
            }
            else
            {
                return allCountStage;
            }

        }
        return allCountStage - 1;
    }

    public void DirectGetPoint(int _playerID, int _point,Vector3 _viewPos)
    {
        if (_playerID == 1)
        {
            
			if(m_Score<m_maxScore)
			{
			m_pointView.number(5, _playerID, _viewPos,1.0f);
            m_Score = m_Score + (int)((float)_point + m_pointBouns[_playerID - 1]);
			}
        }
        else
        {
			if(m_Score>m_minScore)
			{
            m_pointView.number(5, _playerID, _viewPos, 1.0f);
            m_Score = m_Score - (int)((float)_point + m_pointBouns[_playerID - 1]);
			}
        }

    }

    public void DirectGetPoint(int _playerID, int _point)
    {
        if (_playerID == 1)
        {
			if (m_Score < m_maxScore) 
			{
				m_Score = m_Score + (int)((float)_point + m_pointBouns [_playerID - 1]);

			}
        }
        else
        {
			if (m_Score > m_minScore) 
			{
				m_Score = m_Score - (int)((float)_point + m_pointBouns [_playerID - 1]);
			}
        }

    }

    //_Gradually...一気にスコアが減るかじわじわ減るか
    public void CountReset(int _PlayerID, int _Column, bool _Gradually)
    {
        _PlayerID--;
        _Column--;

        if (!_Gradually) m_countSpawner[_PlayerID, _Column] = 0;
        else ScoremDecrease(_PlayerID, _Column);
    }

    private void ScoremDecrease(int _PlayerID, int _Column)
    {
        if (m_countSpawner[_PlayerID, _Column] != 0)
        {
            m_DecreaseTime -= Time.deltaTime;
            if (m_DecreaseTime <= 0.0f)
            {
                m_DecreaseTime = DECREASE_TIME;
                if (m_countSpawner[_PlayerID, _Column] > 0) m_countSpawner[_PlayerID, _Column] -= 2;
                else m_countSpawner[_PlayerID, _Column] = 0;
            }
        }
    }

    public void GetPoint(int _playerID, int _Column)
    {
        int enemyPlayerID = 0;  //敵対プレイヤーID

        //敵対プレイヤーIDを取る
        if (_playerID == 1)
        {
            enemyPlayerID = 1;
        }
        else if (_playerID == 2)
        {
            enemyPlayerID = 0;
        }

        if (_playerID == 1)
		{if (m_Score < m_maxScore) 
			{
				int point = (int)(m_scoreArea [GetPointLevel (enemyPlayerID + 1, _Column)] + m_pointBouns [_playerID - 1]);
				m_Score = m_Score + point;
				m_pointView.number (point, _playerID, _Column, 1.0f);
			}
        }
        else
		{	if (m_Score > m_minScore) 
			{
				int score = GetPointLevel (enemyPlayerID + 1, _Column);
				int point = (int)(m_scoreArea [score] + m_pointBouns [_playerID - 1]);
				m_Score = m_Score - point;
				m_pointView.number (point, _playerID, _Column, 1.0f);
			}

        }

        CountReset(enemyPlayerID + 1, _Column, false);
    }

    public void pointBouns(int _playerID, float _bouns, float _time)
    {
        _playerID--;
        m_pointBouns[_playerID] = _bouns;
        m_pointBounsTime[_playerID] = _time;
    }

    public void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (m_pointBounsTime[i] > 0.0f)
            {
                m_pointBounsTime[i] -= Time.deltaTime;
            }
            else
            {
                m_pointBouns[i] = 0.0f;
            }

        }
    }
}
