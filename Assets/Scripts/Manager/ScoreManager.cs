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

    public int GetPointLevel(int _playerID, int _Column)
    {
        //配列に合わせるためすべて-1にする（修正したい）
        _playerID--;
        _Column--;

        int checkCount = m_countSpawner[_playerID, _Column];
        int countStage = m_countAreaStage - 1;

        for (int i = 0; i <= m_countAreaStage; i++)
        {
            if (m_countArea[i] <= checkCount)
            {
                countStage = i;
                break;
            }
        }

        return countStage;
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
        int checkCount = m_countSpawner[enemyPlayerID, _Column-1];
        int countStage = m_countAreaStage-1;

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

        m_countSpawner[enemyPlayerID, _Column - 1] = 0;
    }

}
