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

// System.Linq.OrderedSequence`2<System.Int32,System.Guid>
struct OrderedSequence_2_t2918240050;
// System.Collections.Generic.IEnumerable`1<System.Int32>
struct IEnumerable_1_t159784161;
// System.Func`2<System.Int32,System.Guid>
struct Func_2_t998693422;
// System.Collections.Generic.IComparer`1<System.Guid>
struct IComparer_1_t1142801175;
// System.Linq.SortContext`1<System.Int32>
struct SortContext_1_t103321601;

#include "codegen/il2cpp-codegen.h"
#include "System_Core_System_Linq_SortDirection313822039.h"

// System.Void System.Linq.OrderedSequence`2<System.Int32,System.Guid>::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Linq.SortDirection)
extern "C"  void OrderedSequence_2__ctor_m2047510087_gshared (OrderedSequence_2_t2918240050 * __this, Il2CppObject* ___source0, Func_2_t998693422 * ___key_selector1, Il2CppObject* ___comparer2, int32_t ___direction3, const MethodInfo* method);
#define OrderedSequence_2__ctor_m2047510087(__this, ___source0, ___key_selector1, ___comparer2, ___direction3, method) ((  void (*) (OrderedSequence_2_t2918240050 *, Il2CppObject*, Func_2_t998693422 *, Il2CppObject*, int32_t, const MethodInfo*))OrderedSequence_2__ctor_m2047510087_gshared)(__this, ___source0, ___key_selector1, ___comparer2, ___direction3, method)
// System.Linq.SortContext`1<TElement> System.Linq.OrderedSequence`2<System.Int32,System.Guid>::CreateContext(System.Linq.SortContext`1<TElement>)
extern "C"  SortContext_1_t103321601 * OrderedSequence_2_CreateContext_m3709390779_gshared (OrderedSequence_2_t2918240050 * __this, SortContext_1_t103321601 * ___current0, const MethodInfo* method);
#define OrderedSequence_2_CreateContext_m3709390779(__this, ___current0, method) ((  SortContext_1_t103321601 * (*) (OrderedSequence_2_t2918240050 *, SortContext_1_t103321601 *, const MethodInfo*))OrderedSequence_2_CreateContext_m3709390779_gshared)(__this, ___current0, method)
// System.Collections.Generic.IEnumerable`1<TElement> System.Linq.OrderedSequence`2<System.Int32,System.Guid>::Sort(System.Collections.Generic.IEnumerable`1<TElement>)
extern "C"  Il2CppObject* OrderedSequence_2_Sort_m72507442_gshared (OrderedSequence_2_t2918240050 * __this, Il2CppObject* ___source0, const MethodInfo* method);
#define OrderedSequence_2_Sort_m72507442(__this, ___source0, method) ((  Il2CppObject* (*) (OrderedSequence_2_t2918240050 *, Il2CppObject*, const MethodInfo*))OrderedSequence_2_Sort_m72507442_gshared)(__this, ___source0, method)
