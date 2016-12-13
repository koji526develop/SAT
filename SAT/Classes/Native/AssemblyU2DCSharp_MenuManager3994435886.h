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
#include "UnityEngine_UnityEngine_Color4194546905.h"
#include "AssemblyU2DCSharp_MenuManager_PlayerSetting651495662.h"

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
	SceneChanger_t3028664502 * ___sceneChanger_9;
	// System.Int32 MenuManager::m_touchScene
	int32_t ___m_touchScene_10;
	// UnityEngine.Color MenuManager::m_choiceColor
	Color_t4194546905  ___m_choiceColor_11;
	// UnityEngine.Color MenuManager::m_nonChoiceColor
	Color_t4194546905  ___m_nonChoiceColor_12;
	// UnityEngine.GameObject[] MenuManager::m_scenesButtonObj
	GameObjectU5BU5D_t2662109048* ___m_scenesButtonObj_13;
	// System.Boolean MenuManager::isGame
	bool ___isGame_15;

public:
	inline static int32_t get_offset_of_sceneChanger_9() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___sceneChanger_9)); }
	inline SceneChanger_t3028664502 * get_sceneChanger_9() const { return ___sceneChanger_9; }
	inline SceneChanger_t3028664502 ** get_address_of_sceneChanger_9() { return &___sceneChanger_9; }
	inline void set_sceneChanger_9(SceneChanger_t3028664502 * value)
	{
		___sceneChanger_9 = value;
		Il2CppCodeGenWriteBarrier(&___sceneChanger_9, value);
	}

	inline static int32_t get_offset_of_m_touchScene_10() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_touchScene_10)); }
	inline int32_t get_m_touchScene_10() const { return ___m_touchScene_10; }
	inline int32_t* get_address_of_m_touchScene_10() { return &___m_touchScene_10; }
	inline void set_m_touchScene_10(int32_t value)
	{
		___m_touchScene_10 = value;
	}

	inline static int32_t get_offset_of_m_choiceColor_11() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_choiceColor_11)); }
	inline Color_t4194546905  get_m_choiceColor_11() const { return ___m_choiceColor_11; }
	inline Color_t4194546905 * get_address_of_m_choiceColor_11() { return &___m_choiceColor_11; }
	inline void set_m_choiceColor_11(Color_t4194546905  value)
	{
		___m_choiceColor_11 = value;
	}

	inline static int32_t get_offset_of_m_nonChoiceColor_12() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_nonChoiceColor_12)); }
	inline Color_t4194546905  get_m_nonChoiceColor_12() const { return ___m_nonChoiceColor_12; }
	inline Color_t4194546905 * get_address_of_m_nonChoiceColor_12() { return &___m_nonChoiceColor_12; }
	inline void set_m_nonChoiceColor_12(Color_t4194546905  value)
	{
		___m_nonChoiceColor_12 = value;
	}

	inline static int32_t get_offset_of_m_scenesButtonObj_13() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_scenesButtonObj_13)); }
	inline GameObjectU5BU5D_t2662109048* get_m_scenesButtonObj_13() const { return ___m_scenesButtonObj_13; }
	inline GameObjectU5BU5D_t2662109048** get_address_of_m_scenesButtonObj_13() { return &___m_scenesButtonObj_13; }
	inline void set_m_scenesButtonObj_13(GameObjectU5BU5D_t2662109048* value)
	{
		___m_scenesButtonObj_13 = value;
		Il2CppCodeGenWriteBarrier(&___m_scenesButtonObj_13, value);
	}

	inline static int32_t get_offset_of_isGame_15() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___isGame_15)); }
	inline bool get_isGame_15() const { return ___isGame_15; }
	inline bool* get_address_of_isGame_15() { return &___isGame_15; }
	inline void set_isGame_15(bool value)
	{
		___isGame_15 = value;
	}
};

struct MenuManager_t3994435886_StaticFields
{
public:
	// MenuManager/PlayerSetting MenuManager::m_playerSetting
	PlayerSetting_t651495662  ___m_playerSetting_14;

public:
	inline static int32_t get_offset_of_m_playerSetting_14() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886_StaticFields, ___m_playerSetting_14)); }
	inline PlayerSetting_t651495662  get_m_playerSetting_14() const { return ___m_playerSetting_14; }
	inline PlayerSetting_t651495662 * get_address_of_m_playerSetting_14() { return &___m_playerSetting_14; }
	inline void set_m_playerSetting_14(PlayerSetting_t651495662  value)
	{
		___m_playerSetting_14 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
