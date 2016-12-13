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

// System.Linq.QuickSort`1<System.Int32>
struct QuickSort_1_t1017357995;
// System.Collections.Generic.IEnumerable`1<System.Int32>
struct IEnumerable_1_t159784161;
// System.Linq.SortContext`1<System.Int32>
struct SortContext_1_t103321601;
// System.Int32[]
struct Int32U5BU5D_t3230847821;

#include "codegen/il2cpp-codegen.h"

// System.Void System.Linq.QuickSort`1<System.Int32>::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Linq.SortContext`1<TElement>)
extern "C"  void QuickSort_1__ctor_m1400684173_gshared (QuickSort_1_t1017357995 * __this, Il2CppObject* ___source0, SortContext_1_t103321601 * ___context1, const MethodInfo* method);
#define QuickSort_1__ctor_m1400684173(__this, ___source0, ___context1, method) ((  void (*) (QuickSort_1_t1017357995 *, Il2CppObject*, SortContext_1_t103321601 *, const MethodInfo*))QuickSort_1__ctor_m1400684173_gshared)(__this, ___source0, ___context1, method)
// System.Int32[] System.Linq.QuickSort`1<System.Int32>::CreateIndexes(System.Int32)
extern "C"  Int32U5BU5D_t3230847821* QuickSort_1_CreateIndexes_m3147495802_gshared (Il2CppObject * __this /* static, unused */, int32_t ___length0, const MethodInfo* method);
#define QuickSort_1_CreateIndexes_m3147495802(__this /* static, unused */, ___length0, method) ((  Int32U5BU5D_t3230847821* (*) (Il2CppObject * /* static, unused */, int32_t, const MethodInfo*))QuickSort_1_CreateIndexes_m3147495802_gshared)(__this /* static, unused */, ___length0, method)
// System.Void System.Linq.QuickSort`1<System.Int32>::PerformSort()
extern "C"  void QuickSort_1_PerformSort_m111689944_gshared (QuickSort_1_t1017357995 * __this, const MethodInfo* method);
#define QuickSort_1_PerformSort_m111689944(__this, method) ((  void (*) (QuickSort_1_t1017357995 *, const MethodInfo*))QuickSort_1_PerformSort_m111689944_gshared)(__this, method)
// System.Int32 System.Linq.QuickSort`1<System.Int32>::CompareItems(System.Int32,System.Int32)
extern "C"  int32_t QuickSort_1_CompareItems_m1490950384_gshared (QuickSort_1_t1017357995 * __this, int32_t ___first_index0, int32_t ___second_index1, const MethodInfo* method);
#define QuickSort_1_CompareItems_m1490950384(__this, ___first_index0, ___second_index1, method) ((  int32_t (*) (QuickSort_1_t1017357995 *, int32_t, int32_t, const MethodInfo*))QuickSort_1_CompareItems_m1490950384_gshared)(__this, ___first_index0, ___second_index1, method)
// System.Int32 System.Linq.QuickSort`1<System.Int32>::MedianOfThree(System.Int32,System.Int32)
extern "C"  int32_t QuickSort_1_MedianOfThree_m2024826850_gshared (QuickSort_1_t1017357995 * __this, int32_t ___left0, int32_t ___right1, const MethodInfo* method);
#define QuickSort_1_MedianOfThree_m2024826850(__this, ___left0, ___right1, method) ((  int32_t (*) (QuickSort_1_t1017357995 *, int32_t, int32_t, const MethodInfo*))QuickSort_1_MedianOfThree_m2024826850_gshared)(__this, ___left0, ___right1, method)
// System.Void System.Linq.QuickSort`1<System.Int32>::Sort(System.Int32,System.Int32)
extern "C"  void QuickSort_1_Sort_m3602586079_gshared (QuickSort_1_t1017357995 * __this, int32_t ___left0, int32_t ___right1, const MethodInfo* method);
#define QuickSort_1_Sort_m3602586079(__this, ___left0, ___right1, method) ((  void (*) (QuickSort_1_t1017357995 *, int32_t, int32_t, const MethodInfo*))QuickSort_1_Sort_m3602586079_gshared)(__this, ___left0, ___right1, method)
// System.Void System.Linq.QuickSort`1<System.Int32>::InsertionSort(System.Int32,System.Int32)
extern "C"  void QuickSort_1_InsertionSort_m276243008_gshared (QuickSort_1_t1017357995 * __this, int32_t ___left0, int32_t ___right1, const MethodInfo* method);
#define QuickSort_1_InsertionSort_m276243008(__this, ___left0, ___right1, method) ((  void (*) (QuickSort_1_t1017357995 *, int32_t, int32_t, const MethodInfo*))QuickSort_1_InsertionSort_m276243008_gshared)(__this, ___left0, ___right1, method)
// System.Void System.Linq.QuickSort`1<System.Int32>::Swap(System.Int32,System.Int32)
extern "C"  void QuickSort_1_Swap_m1519267018_gshared (QuickSort_1_t1017357995 * __this, int32_t ___left0, int32_t ___right1, const MethodInfo* method);
#define QuickSort_1_Swap_m1519267018(__this, ___left0, ___right1, method) ((  void (*) (QuickSort_1_t1017357995 *, int32_t, int32_t, const MethodInfo*))QuickSort_1_Swap_m1519267018_gshared)(__this, ___left0, ___right1, method)
// System.Collections.Generic.IEnumerable`1<TElement> System.Linq.QuickSort`1<System.Int32>::Sort(System.Collections.Generic.IEnumerable`1<TElement>,System.Linq.SortContext`1<TElement>)
extern "C"  Il2CppObject* QuickSort_1_Sort_m1876265865_gshared (Il2CppObject * __this /* static, unused */, Il2CppObject* ___source0, SortContext_1_t103321601 * ___context1, const MethodInfo* method);
#define QuickSort_1_Sort_m1876265865(__this /* static, unused */, ___source0, ___context1, method) ((  Il2CppObject* (*) (Il2CppObject * /* static, unused */, Il2CppObject*, SortContext_1_t103321601 *, const MethodInfo*))QuickSort_1_Sort_m1876265865_gshared)(__this /* static, unused */, ___source0, ___context1, method)
