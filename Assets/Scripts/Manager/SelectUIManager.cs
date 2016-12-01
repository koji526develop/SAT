using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class SelectUIManager : MonoBehaviour
{
    //現在何Pが兵士を選択中か
    public static int PlayerID;

    int m_soldierTotalNum;
    int m_gaugeCount;

    List<int> m_soldierNumList = new List<int>();

    Text[] m_soldierText = new Text[4];
    List<GameObject> gaugeList = new List<GameObject>();

    public static int SWORD_NUM_1;
    public static int SPEAR_NUM_1;
    public static int AX_NUM_1;
    public static int SHIELD_NUM_1;

    public static int SWORD_NUM_2;
    public static int SPEAR_NUM_2;
    public static int AX_NUM_2;
    public static int SHIELD_NUM_2;

    GameObject sceneChangerObj;
    SceneChanger sceneChanger;

    enum PlusOrMinus
    {
        Plus,
        Minus
    }

    string[] WEAPON_NAME = { "剣", "槍", "斧", "盾" };
    string[] IconPath = { "UI/Select/sword_icon", "UI/Select/spear_icon", "UI/Select/ax_icon", "UI/Select/shield_icon" };

    void Awake()
    {

        //背景
        MyUtility.CreateImage(
            "SelectBackGround",
            "UI/Select/select_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            transform);

        for (int i = 0; i < 4; i++)
            m_soldierNumList.Add(0);

        // 武器の名前がついた丸４つ作成
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = MyUtility.CreateImage(
                WEAPON_NAME[i],
                IconPath[i],
                new Vector2(3 / 32.0f, (21 - i * 4) / 25.0f),
                new Vector2(6 / 32.0f, (24 - i * 4) / 25.0f),
                transform
            );
            //MyUtility.AddText(WEAPON_NAME[i], obj.transform);
        }

        float[,] value = {
			{ MyUtility.SWORD_LIFE, MyUtility.SWORD_ATTACK, MyUtility.SWORD_ATTACKDISTANCE, MyUtility.SWORD_MOVESPEED,0 },
			{ MyUtility.SPEAR_LIFE, MyUtility.SPEAR_ATTACK, MyUtility.SPEAR_ATTACKDISTANCE, MyUtility.SPEAR_MOVESPEED,0 },
			{ MyUtility.SHIELD_LIFE, MyUtility.SHIELD_ATTACK, MyUtility.SHIELD_ATTACKDISTANCE, MyUtility.SHIELD_MOVESPEED,0 },
			{ MyUtility.AX_LIFE, MyUtility.AX_ATTACK, MyUtility.AX_ATTACKDISTANCE, MyUtility.AX_MOVESPEED,0 }
        };
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Text text = MyUtility.CreateText(
                    "Text",
                    transform,
                    35,
                    Vector3.zero,
                    new Vector2((6 + i * 3) / 32.0f, (21 - j * 4) / 25.0f),
                    new Vector2((9 + i * 3) / 32.0f, (24 - j * 4) / 25.0f));
                text.text = value[j, i].ToString();
                if (i == 4)
                    m_soldierText[j] = text;
            }
        }

        // +ボタン４つ作成
        for (int i = 0; i < 4; i++)
        {
            GameObject buttonObj = MyUtility.CreateButton(
				"Plus",
				"UI/Select/plus",
                new Vector2(27 / 32.0f, (21 - i * 4) / 25.0f),
                new Vector2(30 / 32.0f, (24 - i * 4) / 25.0f),
                transform
            );
            AddButtonEvent(buttonObj.GetComponent<Button>(), i, PlusOrMinus.Plus);
        }

        // -ボタン４つ作成
        for (int i = 0; i < 4; i++)
        {
            GameObject buttonObj = MyUtility.CreateButton(
				"Minus",
				"UI/Select/minus",
                new Vector2(23 / 32.0f, (21 - i * 4) / 25.0f),
                new Vector2(26 / 32.0f, (24 - i * 4) / 25.0f),
                transform
            );
            AddButtonEvent(buttonObj.GetComponent<Button>(), i, PlusOrMinus.Minus);
        }

        // ゲージ９つ作成
        for (int i = 0; i < 9; i++)
        {
            GameObject obj = MyUtility.CreateImage
                (
                    "Gauge",
                    "UI/Select/select_gauge0",
                    new Vector2((6 + i * 2) / 32.0f, 5 / 25.0f),
                    new Vector2((8 + i * 2) / 32.0f, 8 / 25.0f),
                    transform
                );
            gaugeList.Add(obj);
        }

        // 戻るボタン作成
        GameObject backObj = MyUtility.CreateButton(
			"Back",
			"UI/Select/return",
            new Vector2(3 / 32.0f, 1 / 25.0f),
            new Vector2(9 / 32.0f, 4 / 25.0f),
            transform
        );
        
		backObj.GetComponent<Button>().onClick.AddListener(Back);

        // 決定ボタン作成
        GameObject enterObj = MyUtility.CreateButton(
			"Decision",
			"UI/Select/decision",
            new Vector2(23 / 32.0f, 1 / 25.0f),
            new Vector2(29 / 32.0f, 4 / 25.0f),
            transform
        );
        enterObj.GetComponent<Button>().onClick.AddListener(EnterProces);

        sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();

    }

	void Back()
	{
		AudioManager.m_instance.PlaySE ("button_SE");
		sceneChanger.ChangeToOperating ();
	}

    public void EnterProces()
    {
		AudioManager.m_instance.PlaySE ("button_SE");

        if (m_soldierTotalNum == 18)
        {
            if (PlayerID == 1)
            {
                //最終的な兵士の値を保存
                SWORD_NUM_1 = m_soldierNumList[0];
                SPEAR_NUM_1 = m_soldierNumList[1];
                AX_NUM_1 = m_soldierNumList[2];
                SHIELD_NUM_1 = m_soldierNumList[3];
                PlayerID = 2;

                //リザルトように別で兵士の数を保存
                for (int i = 0; i < 4; i++)
                {
                    ResultManager.ResultSoldierNum[i] = m_soldierNumList[i];
                }
            }
            else
            {
                //最終的な兵士の値を保存
                SWORD_NUM_2 = m_soldierNumList[0];
                SPEAR_NUM_2 = m_soldierNumList[1];
                AX_NUM_2 = m_soldierNumList[2];
                SHIELD_NUM_2 = m_soldierNumList[3];
                //3Pはいないが次に進めるために3を振る
                PlayerID = 3;

                //リザルトように別で兵士の数を保存
                for (int i = 0; i < 4; i++)
                {
                    ResultManager.ResultSoldierNum[i + 4] = m_soldierNumList[i];
                }
            }

            //シーン遷移
            sceneChanger.ChangeToSelectSpecial();
        }
    }


    Sprite GetGaugeSprite(PlusOrMinus _plusOrMinus)
    {
        if (_plusOrMinus == PlusOrMinus.Plus)
        {
            switch (m_soldierTotalNum % 2)
            {
                case 0:
                    return Resources.Load("UI/Select/select_gauge1", typeof(Sprite)) as Sprite;
                case 1:
                    return Resources.Load("UI/Select/select_gauge2", typeof(Sprite)) as Sprite;
            }
        }
        else
        {
            switch (m_soldierTotalNum % 2)
            {
                case 0:
                    return Resources.Load("UI/Select/select_gauge1", typeof(Sprite)) as Sprite;
                case 1:
                    return Resources.Load("UI/Select/select_gauge0", typeof(Sprite)) as Sprite;
            }
        }
        return null;
    }

    // ボタンに機能を付与する
    void AddButtonEvent(Button button, int num, PlusOrMinus _plusOrMinus)
    {
        if (_plusOrMinus == PlusOrMinus.Plus)
            button.onClick.AddListener(() => { this.Plus(num); });
        else
            button.onClick.AddListener(() => { this.Minus(num); });
    }

    void Plus(int num)
    {
		AudioManager.m_instance.PlaySE ("button_SE");

        // 兵士の合計数が兵士最大数より多かったら抜ける
        if (m_soldierTotalNum >= 18)
            return;

        // 兵士の合計数が偶数かつ兵士合計数が０でなかったら
        if (m_soldierTotalNum % 2 == 0 && m_soldierTotalNum != 0)
        {
            m_gaugeCount++;
            gaugeList[m_gaugeCount].GetComponent<Image>().sprite = GetGaugeSprite(PlusOrMinus.Plus);
        }
        else
        {
            gaugeList[m_gaugeCount].GetComponent<Image>().sprite = GetGaugeSprite(PlusOrMinus.Plus);
        }

        m_soldierNumList[num]++;

        m_soldierTotalNum = m_soldierNumList.Sum();

        m_soldierText[num].text = m_soldierNumList[num].ToString();
    }

    void Minus(int num)
    {
		AudioManager.m_instance.PlaySE ("button_SE");

        // 兵士の合計数が０以下または各兵士の数が０以下だったら抜ける
        if (m_soldierTotalNum <= 0 || m_soldierNumList[num] <= 0)
            return;

        // 兵士の合計数が偶数または兵士の合計数が1だったら
        if (m_soldierTotalNum % 2 == 0 || m_soldierTotalNum == 1)
        {
            gaugeList[m_gaugeCount].GetComponent<Image>().sprite = GetGaugeSprite(PlusOrMinus.Minus);
        }
        else
        {
            gaugeList[m_gaugeCount].GetComponent<Image>().sprite = GetGaugeSprite(PlusOrMinus.Minus);
            m_gaugeCount--;
        }

        m_soldierNumList[num]--;

        m_soldierTotalNum = m_soldierNumList.Sum();

        m_soldierText[num].text = m_soldierNumList[num].ToString();
    }
}
