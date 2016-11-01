using UnityEngine;
using System.Collections;

public class TapEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_tapEffect;
    [SerializeField]
    Camera m_camera;

    void Start()
    {

    }

    void Update()
    {
        TouchInfo touch = TouchManager.GetTouchInfo(0);
        if (touch == TouchInfo.Began)
        {
            var touchPos = m_camera.ScreenToWorldPoint(TouchManager.GetTouchPosition(0) + m_camera.transform.forward * 10);
            m_tapEffect.transform.position = touchPos;
            m_tapEffect.Emit(1);
            Debug.Log("タップ");
        }
    }
}
