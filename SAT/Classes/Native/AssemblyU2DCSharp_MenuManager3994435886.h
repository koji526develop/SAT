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
	SceneChanger_t3028664502 * ___sceneChanger_2;
	// System.Boolean MenuManager::isGame
	bool ___isGame_4;
	// UnityEngine.GameObject[] MenuManager::m_PlayerButton
	GameObjectU5BU5D_t2662109048* ___m_PlayerButton_5;

public:
	inline static int32_t get_offset_of_sceneChanger_2() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___sceneChanger_2)); }
	inline SceneChanger_t3028664502 * get_sceneChanger_2() const { return ___sceneChanger_2; }
	inline SceneChanger_t3028664502 ** get_address_of_sceneChanger_2() { return &___sceneChanger_2; }
	inline void set_sceneChanger_2(SceneChanger_t3028664502 * value)
	{
		___sceneChanger_2 = value;
		Il2CppCodeGenWriteBarrier(&___sceneChanger_2, value);
	}

	inline static int32_t get_offset_of_isGame_4() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___isGame_4)); }
	inline bool get_isGame_4() const { return ___isGame_4; }
	inline bool* get_address_of_isGame_4() { return &___isGame_4; }
	inline void set_isGame_4(bool value)
	{
		___isGame_4 = value;
	}

	inline static int32_t get_offset_of_m_PlayerButton_5() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886, ___m_PlayerButton_5)); }
	inline GameObjectU5BU5D_t2662109048* get_m_PlayerButton_5() const { return ___m_PlayerButton_5; }
	inline GameObjectU5BU5D_t2662109048** get_address_of_m_PlayerButton_5() { return &___m_PlayerButton_5; }
	inline void set_m_PlayerButton_5(GameObjectU5BU5D_t2662109048* value)
	{
		___m_PlayerButton_5 = value;
		Il2CppCodeGenWriteBarrier(&___m_PlayerButton_5, value);
	}
};

struct MenuManager_t3994435886_StaticFields
{
public:
	// MenuManager/PlayerSetting MenuManager::m_playerSetting
	PlayerSetting_t651495662  ___m_playerSetting_3;

public:
	inline static int32_t get_offset_of_m_playerSetting_3() { return static_cast<int32_t>(offsetof(MenuManager_t3994435886_StaticFields, ___m_playerSetting_3)); }
	inline PlayerSetting_t651495662  get_m_playerSetting_3() const { return ___m_playerSetting_3; }
	inline PlayerSetting_t651495662 * get_address_of_m_playerSetting_3() { return &___m_playerSetting_3; }
	inline void set_m_playerSetting_3(PlayerSetting_t651495662  value)
	{
		___m_playerSetting_3 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
