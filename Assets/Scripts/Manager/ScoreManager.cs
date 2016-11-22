using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int m_maxScore = MyUtility.MAX_WARPOTENTIAL; //最大ゲージ数値
    public  int m_Score;    //現在のスコア数値

    private int m_totalPlayer = 2;  //総プレイヤー数
    private int m_totalColumn = 6;  //列

    private int[,] m_countSpawner;  //それぞれの出現させた回数
    private int[] m_countArea= { 0, 1, 3, 5, 7 };   //フェーズの推移
    private int[] m_scoreArea = { 10, 20, 40, 70, 100 };    //フェーズごとの得点情報
    private int m_countAreaStage;

    void Awake()
    {
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

    public int GetPointLevel(int _playerID, int _Column)
    {
        ////配列に合わせるためすべて-1にする（修正したい）
        _playerID--;
        _Column--;

        int checkCount = m_countSpawner[_playerID, _Column];
        int allCountStage = m_countAreaStage - 1;
    
        for(int i=0; i < m_countAreaStage; i++)
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
        return m_countAreaStage - 1;
    }

    public void DirectGetPoint(int _playerID, int _point)
    {
        if (_playerID == 1)
        {
            m_Score += _point;
        }
        else
        {
            m_Score -= _point;
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
        else if(_playerID == 2)
        {
            enemyPlayerID = 0;
        }

        if (_playerID == 1)
        {
            m_Score += m_scoreArea[GetPointLevel(enemyPlayerID+1,_Column)];
        }
        else
        {
            m_Score -= m_scoreArea[GetPointLevel(enemyPlayerID+1,_Column)];
        }

        m_countSpawner[enemyPlayerID, _Column - 1] = 0;
    }

}
