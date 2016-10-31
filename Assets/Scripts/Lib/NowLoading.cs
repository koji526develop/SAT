using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NowLoading : MonoBehaviour
{
	// 経過時間
	int m_elapsedCount;

	// 遅らせる時間
	public int m_delayCount;

	int m_waitCount = 240;
	int m_elapsedWaitCount = 0;
	int m_count = 0;
	void Awake()
	{
		enabled = true;
	}
		
	void Update ()
	{
		m_elapsedCount += 1;

		if (m_elapsedCount - m_delayCount < 0) 
			return;

		// 経過時間が回転時間を超えたら
		if (m_count == 24) 
		{
			m_elapsedWaitCount += 1;

			if (m_elapsedWaitCount > m_waitCount - m_delayCount) {
				m_elapsedWaitCount = 0;
				m_elapsedCount = 0;
				m_count = 0;
				return;
			} else {
				return;
			}
		}
			
		// 割合から出した２点間の角度を入れる
		transform.Rotate(new Vector3(0,1,0),15);
		m_count++;
	}
}

