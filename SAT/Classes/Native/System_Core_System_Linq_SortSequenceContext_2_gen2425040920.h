﻿#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// System.Func`2<System.Int32,System.Guid>
struct Func_2_t998693422;
// System.Collections.Generic.IComparer`1<System.Guid>
struct IComparer_1_t1142801175;
// System.Guid[]
struct GuidU5BU5D_t3249500912;

#include "System_Core_System_Linq_SortContext_1_gen103321601.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Linq.SortSequenceContext`2<System.Int32,System.Guid>
struct  SortSequenceContext_2_t2425040920  : public SortContext_1_t103321601
{
public:
	// System.Func`2<TElement,TKey> System.Linq.SortSequenceContext`2::selector
	Func_2_t998693422 * ___selector_2;
	// System.Collections.Generic.IComparer`1<TKey> System.Linq.SortSequenceContext`2::comparer
	Il2CppObject* ___comparer_3;
	// TKey[] System.Linq.SortSequenceContext`2::keys
	GuidU5BU5D_t3249500912* ___keys_4;

public:
	inline static int32_t get_offset_of_selector_2() { return static_cast<int32_t>(offsetof(SortSequenceContext_2_t2425040920, ___selector_2)); }
	inline Func_2_t998693422 * get_selector_2() const { return ___selector_2; }
	inline Func_2_t998693422 ** get_address_of_selector_2() { return &___selector_2; }
	inline void set_selector_2(Func_2_t998693422 * value)
	{
		___selector_2 = value;
		Il2CppCodeGenWriteBarrier(&___selector_2, value);
	}

	inline static int32_t get_offset_of_comparer_3() { return static_cast<int32_t>(offsetof(SortSequenceContext_2_t2425040920, ___comparer_3)); }
	inline Il2CppObject* get_comparer_3() const { return ___comparer_3; }
	inline Il2CppObject** get_address_of_comparer_3() { return &___comparer_3; }
	inline void set_comparer_3(Il2CppObject* value)
	{
		___comparer_3 = value;
		Il2CppCodeGenWriteBarrier(&___comparer_3, value);
	}

	inline static int32_t get_offset_of_keys_4() { return static_cast<int32_t>(offsetof(SortSequenceContext_2_t2425040920, ___keys_4)); }
	inline GuidU5BU5D_t3249500912* get_keys_4() const { return ___keys_4; }
	inline GuidU5BU5D_t3249500912** get_address_of_keys_4() { return &___keys_4; }
	inline void set_keys_4(GuidU5BU5D_t3249500912* value)
	{
		___keys_4 = value;
		Il2CppCodeGenWriteBarrier(&___keys_4, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
