#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>
#include <assert.h>
#include <exception>

// System.Linq.SortSequenceContext`2<System.Int32,System.Guid>
struct SortSequenceContext_2_t2425040920;
// System.Func`2<System.Int32,System.Guid>
struct Func_2_t998693422;
// System.Collections.Generic.IComparer`1<System.Guid>
struct IComparer_1_t1142801175;
// System.Linq.SortContext`1<System.Int32>
struct SortContext_1_t103321601;
// System.Int32[]
struct Int32U5BU5D_t3230847821;

#include "codegen/il2cpp-codegen.h"
#include "System_Core_System_Linq_SortDirection313822039.h"

// System.Void System.Linq.SortSequenceContext`2<System.Int32,System.Guid>::.ctor(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Linq.SortDirection,System.Linq.SortContext`1<TElement>)
extern "C"  void SortSequenceContext_2__ctor_m480771317_gshared (SortSequenceContext_2_t2425040920 * __this, Func_2_t998693422 * ___selector0, Il2CppObject* ___comparer1, int32_t ___direction2, SortContext_1_t103321601 * ___child_context3, const MethodInfo* method);
#define SortSequenceContext_2__ctor_m480771317(__this, ___selector0, ___comparer1, ___direction2, ___child_context3, method) ((  void (*) (SortSequenceContext_2_t2425040920 *, Func_2_t998693422 *, Il2CppObject*, int32_t, SortContext_1_t103321601 *, const MethodInfo*))SortSequenceContext_2__ctor_m480771317_gshared)(__this, ___selector0, ___comparer1, ___direction2, ___child_context3, method)
// System.Void System.Linq.SortSequenceContext`2<System.Int32,System.Guid>::Initialize(TElement[])
extern "C"  void SortSequenceContext_2_Initialize_m53796281_gshared (SortSequenceContext_2_t2425040920 * __this, Int32U5BU5D_t3230847821* ___elements0, const MethodInfo* method);
#define SortSequenceContext_2_Initialize_m53796281(__this, ___elements0, method) ((  void (*) (SortSequenceContext_2_t2425040920 *, Int32U5BU5D_t3230847821*, const MethodInfo*))SortSequenceContext_2_Initialize_m53796281_gshared)(__this, ___elements0, method)
// System.Int32 System.Linq.SortSequenceContext`2<System.Int32,System.Guid>::Compare(System.Int32,System.Int32)
extern "C"  int32_t SortSequenceContext_2_Compare_m4219812324_gshared (SortSequenceContext_2_t2425040920 * __this, int32_t ___first_index0, int32_t ___second_index1, const MethodInfo* method);
#define SortSequenceContext_2_Compare_m4219812324(__this, ___first_index0, ___second_index1, method) ((  int32_t (*) (SortSequenceContext_2_t2425040920 *, int32_t, int32_t, const MethodInfo*))SortSequenceContext_2_Compare_m4219812324_gshared)(__this, ___first_index0, ___second_index1, method)
