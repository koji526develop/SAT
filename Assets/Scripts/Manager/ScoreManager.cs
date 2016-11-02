using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int m_maxScore = MyUtility.MAX_WARPOTENTIAL; //最大ゲージ数値
    public  int m_Score;    //現在のスコア数値

    private int m_totalPlayer = 2;  //総プレイヤー数
    private int m_totalColumn = 6;  //列

    private int[,] m_countSpawner;
    private int[] m_countArea= {7,5,3,1,0};
    private int[] m_scoreArea = { 100, 70, 40, 20 ,10};
    private int m_countAreaStage;

    void Awake()
    {
        //スコアの初期設定
        m_Score = m_maxScore / m_totalPlayer;

        m_countSpawner = new int[m_totalPlayer, m_totalColumn];

        for (int i = 0; i < m_totalPlayer; i++)
        {
            for (int j = 0; j < m_totalColumn; j++)
            {
                m_countSpawner[i, j] = 0;
            }
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
    }

    public void GetPoint(int _playerID, int _Column)
    {
        int enemyPlayerID = 0;  //敵対プレイヤーID

        //敵対プレイヤーIDを取る
        if (_playerID == 1)
        {
            enemyPlayerID = 1;
        }
        else if(_playerID == 2)
        {
            enemyPlayerID = 0;
        }

        //カウントを一度取る
        int checkCount = m_countSpawner[enemyPlayerID, _Column];
        int countStage=m_countAreaStage-1;

        for (int i = 0; i <= m_countAreaStage; i++)
        {
            if (m_countArea[i] <= checkCount)
            {
                countStage = i;
                break;
            }
        }

        if (_playerID == 1)
        {
            m_Score += m_scoreArea[countStage];
        }
        else
        {
            m_Score -= m_scoreArea[countStage];
        }

    }
    //private int[,] m_GetPointIdx;
    //private int[,] m_CountSpawner;

    //void Awake() {

    //    m_Score = m_maxScore / m_totalPlayer;
    //    m_GetPointIdx   = new int[m_totalPlayer, m_totalColumn];
    //    m_CountSpawner  = new int[m_totalPlayer, m_totalColumn];

    //    for (int i=0; i < m_totalPlayer; i++)
    //    {
    //        for(int j = 0; j < m_totalColumn; j++)
    //        {
    //            m_GetPointIdx[i, j] = 10;
    //            m_CountSpawner[i, j] = 0;
    //        }
    //    }
    //}

    //public void GetPoint(int _playerID ,int _Column)
    //{
    //    int targetPlayer = 0;
    //    if (_playerID==1)
    //    {
    //        targetPlayer = 1;
    //    }

    //    _Column--;
    //    int movePoint = m_GetPointIdx[targetPlayer, _Column];

    //    if (_playerID == 1)
    //    {
    //        m_Score += movePoint;
    //    }
    //    else
    //    {
    //        m_Score -= movePoint;
    //    }
    //}

    //public void SpawnerCount(int _playerID, int _Column)
    //{
    //    _playerID--;
    //    _Column--;

    //    m_CountSpawner[_playerID, _Column]++;

    //    if (m_CountSpawner[_playerID, _Column] >= 7)
    //    {
    //        m_GetPointIdx[_playerID, _Column] = 100;
    //    }
    //    else if (m_CountSpawner[_playerID, _Column] >= 5)
    //    {
    //        m_GetPointIdx[_playerID, _Column] = 70;
    //    }
    //    else if (m_CountSpawner[_playerID, _Column] >= 3)
    //    {
    //        m_GetPointIdx[_playerID, _Column] = 40;
    //    }
    //    else if (m_CountSpawner[_playerID, _Column] >= 1)
    //    {
    //        m_GetPointIdx[_playerID, _Column] = 20;
    //    }

    //}
}
