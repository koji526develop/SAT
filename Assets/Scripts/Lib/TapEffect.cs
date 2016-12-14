using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TapEffect : MonoBehaviour
{
    [SerializeField]
    Camera m_camera;
    [SerializeField]
    ParticleSystem  m_tapEffect;


    void Start()
    {
        m_tapEffect.GetComponent<Renderer>().sortingLayerName = "tap";
    }

    void Update()
    {
        TouchManager.Update();

        for (int i = 0; i < TouchManager.touchCount; i++)
        {
			TouchInfo touch = TouchManager.GetTouchInfo (i);
			if (touch == TouchInfo.Began)
            {
				var touchPos = m_camera.ScreenToWorldPoint (TouchManager.GetTouchPosition (i) + m_camera.transform.forward * 10);
				m_tapEffect.transform.position = touchPos;
				m_tapEffect.Emit (1);
            }
		}
    }
}
