using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour
{

    public string m_nextScene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            MySceneManager.m_instance.LoadLevel(m_nextScene, 1.0f);
        }
    }
}
