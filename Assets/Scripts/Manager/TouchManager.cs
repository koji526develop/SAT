using UnityEngine;
using System.Collections;

//タッチ処理

public static class TouchManager
{
    private static Vector3 m_TouchPosition = Vector3.zero;
    private static Vector3 m_PreviousPosition = Vector3.zero;
    private static Vector2 m_FirstTouchPosition = Vector2.zero;

    // タッチ情報を取得(エディタと実機を考慮)
    // <returns>タッチ情報。タッチされていない場合は null</returns>

    public static TouchInfo GetTouchInfo(int touchCount)
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_FirstTouchPosition = Input.mousePosition;
                return TouchInfo.Began;
            }
            if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                m_FirstTouchPosition = touch.position;
                return (TouchInfo)((int)Input.GetTouch(touchCount).phase);
            }
        }
        return TouchInfo.None;
    }

    // タッチポジションを取得(エディタと実機を考慮)
    // <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>

    public static Vector3 GetTouchPosition(int touchCount)
    {
        if (Application.isEditor)
        {
            TouchInfo touch = TouchManager.GetTouchInfo(touchCount);
            if (touch != TouchInfo.None) { return Input.mousePosition; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                m_TouchPosition.x = touch.position.x;
                m_TouchPosition.y = touch.position.y;
                return m_TouchPosition;
            }
        }
        return Vector3.zero;
    }

    // 前回のタッチポジションを取得(エディタと実機を考慮)

    public static Vector3 GetDeltaPosition(int touchCount)
    {
        if (Application.isEditor)
        {
            TouchInfo info = TouchManager.GetTouchInfo(touchCount);
            if (info != TouchInfo.None)
            {
                Vector3 currentPosition = Input.mousePosition;
                Vector3 delta = currentPosition - m_PreviousPosition;
                m_PreviousPosition = currentPosition;
                return delta;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                m_PreviousPosition.x = touch.deltaPosition.x;
                m_PreviousPosition.y = touch.deltaPosition.y;
                return m_PreviousPosition;
            }
        }
        return Vector3.zero;
    }

    //　最初にタッチした地点からの移動距離
    //　X座標
    public static float GetTouchMoveDistanceX(int touchCount)
    {
        //現在のタッチのポジションを取得
        float touchPosX = GetTouchPosition(touchCount).x;

        //距離を算出
        float distance = touchPosX - m_FirstTouchPosition.x;

        return distance;
    }

    //　最初にタッチした地点からの移動距離
    //　Y座標
    public static float GetTouchMoveDistanceY(int touchCount)
    {
        //現在のタッチのポジションを取得
        float touchPosY = GetTouchPosition(touchCount).y;

        //距離を算出
        float distance = touchPosY - m_FirstTouchPosition.y;

        return distance;
    }

    // タッチワールドポジションを取得(エディタと実機を考慮)
    // <param name='camera'>カメラ</param>
    // <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>

    public static Vector3 GetTouchWorldPosition(Camera camera, int touchCount)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition(touchCount));
    }

    // Raycast
    public static GameObject GetRaycastHitObject(int touchCount)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(TouchManager.GetTouchPosition(touchCount));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}

public enum TouchInfo
{
    // タッチなし
    None = 99,

    // 以下は UnityEngine.TouchPhase の値に対応

    // タッチ開始
    Began = 0,
    // タッチ移動
    Moved = 1,
    // タッチ静止
    Stationary = 2,
    // タッチ終了
    Ended = 3,
    // タッチキャンセル
    Canceled = 4,
}
