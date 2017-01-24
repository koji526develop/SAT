#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// SceneChanger
struct SceneChanger_t3028664502;
// UnityEngine.GameObject[]
struct GameObjectU5BU5D_t2662109048;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"
#include "AssemblyU2DCSharp_MenuManager_PlayerSetting651495662.h"
#include "UnityEngine_UnityEngine_Color4194546905.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// MenuManager
struct  MenuManager_t3994435886  : public MonoBehaviour_t667441552
{
public:
	// SceneChanger MenuManager::sceneChanger
	SceneChanger_t3028664502 * ___sceneChanger_3;
	// System.Boolean MenuManager::isGame
	bool ___isGame_5;
	// UnityEngine.GameObject[] MenuManager::m_PlayerButton
	GameObjectU5BU5D_t2662109048* ___m_PlayerButton_6;
	// UnityEngine.Color MenuManager::m_buttonClearColor
	Color_t4194546905  ___m_buttonClearColor_7;

public:
	inline static int32_t get_offset_of_sceneChanger_3() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___sceneChanger_3)); }
	inline SceneChanger_t3028664502 * get_sceneChanger_3() const { return ___sceneChanger_3; }
	inline SceneChanger_t3028664502 ** get_address_of_sceneChanger_3() { return &___sceneChanger_3; }
	inline void set_sceneChanger_3(SceneChanger_t3028664502 * value)
	{
		___sceneChanger_3 = value;
		Il2CppCodeGenWriteBarrier(&___sceneChanger_3, value);
	}

	inline static int32_t get_offset_of_isGame_5() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___isGame_5)); }
	inline bool get_isGame_5() const { return ___isGame_5; }
	inline bool* get_address_of_isGame_5() { return &___isGame_5; }
	inline void set_isGame_5(bool value)
	{
		___isGame_5 = value;
	}

	inline static int32_t get_offset_of_m_PlayerButton_6() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_PlayerButton_6)); }
	inline GameObjectU5BU5D_t2662109048* get_m_PlayerButton_6() const { return ___m_PlayerButton_6; }
	inline GameObjectU5BU5D_t2662109048** get_address_of_m_PlayerButton_6() { return &___m_PlayerButton_6; }
	inline void set_m_PlayerButton_6(GameObjectU5BU5D_t2662109048* value)
	{
		___m_PlayerButton_6 = value;
		Il2CppCodeGenWriteBarrier(&___m_PlayerButton_6, value);
	}

	inline static int32_t get_offset_of_m_buttonClearColor_7() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_buttonClearColor_7)); }
	inline Color_t4194546905  get_m_buttonClearColor_7() const { return ___m_buttonClearColor_7; }
	inline Color_t4194546905 * get_address_of_m_buttonClearColor_7() { return &___m_buttonClearColor_7; }
	inline void set_m_buttonClearColor_7(Color_t4194546905  value)
	{
		___m_buttonClearColor_7 = value;
	}
};

struct MenuManager_t3994435886_StaticFields
{
public:
	// System.Boolean MenuManager::isTutorial
	bool ___isTutorial_2;
	// MenuManager/PlayerSetting MenuManager::m_playerSetting
	PlayerSetting_t651495662  ___m_playerSetting_4;

public:
	inline static int32_t get_offset_of_isTutorial_2() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886_StaticFields, ___isTutorial_2)); }
	inline bool get_isTutorial_2() const { return ___isTutorial_2; }
	inline bool* get_address_of_isTutorial_2() { return &___isTutorial_2; }
	inline void set_isTutorial_2(bool value)
	{
		___isTutorial_2 = value;
	}

	inline static int32_t get_offset_of_m_playerSetting_4() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886_StaticFields, ___m_playerSetting_4)); }
	inline PlayerSetting_t651495662  get_m_playerSetting_4() const { return ___m_playerSetting_4; }
	inline PlayerSetting_t651495662 * get_address_of_m_playerSetting_4() { return &___m_playerSetting_4; }
	inline void set_m_playerSetting_4(PlayerSetting_t651495662  value)
	{
		___m_playerSetting_4 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
