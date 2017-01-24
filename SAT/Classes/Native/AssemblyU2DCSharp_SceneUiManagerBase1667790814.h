#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// System.Collections.Generic.List`1<UnityEngine.GameObject>
struct List_1_t747900261;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SceneUiManagerBase
struct  SceneUiManagerBase_t1667790814  : public MonoBehaviour_t667441552
{
public:
	// System.Collections.Generic.List`1<UnityEngine.GameObject> SceneUiManagerBase::allGameObjectList
	List_1_t747900261 * ___allGameObjectList_2;

public:
	inline static int32_t get_offset_of_allGameObjectList_2() { return static_cast<int32_t>(offsetof(SceneUiManagerBase_t1667790814, ___allGameObjectList_2)); }
	inline List_1_t747900261 * get_allGameObjectList_2() const { return ___allGameObjectList_2; }
	inline List_1_t747900261 ** get_address_of_allGameObjectList_2() { return &___allGameObjectList_2; }
	inline void set_allGameObjectList_2(List_1_t747900261 * value)
	{
		___allGameObjectList_2 = value;
		Il2CppCodeGenWriteBarrier(&___allGameObjectList_2, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
