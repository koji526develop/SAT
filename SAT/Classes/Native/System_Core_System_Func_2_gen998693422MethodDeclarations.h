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

// System.Func`2<System.Int32,System.Guid>
struct Func_2_t998693422;
// System.Object
struct Il2CppObject;
// System.IAsyncResult
struct IAsyncResult_t2754620036;
// System.AsyncCallback
struct AsyncCallback_t1369114871;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_Object4170816371.h"
#include "mscorlib_System_IntPtr4010401971.h"
#include "mscorlib_System_Guid2862754429.h"
#include "mscorlib_System_AsyncCallback1369114871.h"

// System.Void System.Func`2<System.Int32,System.Guid>::.ctor(System.Object,System.IntPtr)
extern "C"  void Func_2__ctor_m1544651747_gshared (Func_2_t998693422 * __this, Il2CppObject * ___object0, IntPtr_t ___method1, const MethodInfo* method);
#define Func_2__ctor_m1544651747(__this, ___object0, ___method1, method) ((  void (*) (Func_2_t998693422 *, Il2CppObject *, IntPtr_t, const MethodInfo*))Func_2__ctor_m1544651747_gshared)(__this, ___object0, ___method1, method)
// TResult System.Func`2<System.Int32,System.Guid>::Invoke(T)
extern "C"  Guid_t2862754429  Func_2_Invoke_m1752772915_gshared (Func_2_t998693422 * __this, int32_t ___arg10, const MethodInfo* method);
#define Func_2_Invoke_m1752772915(__this, ___arg10, method) ((  Guid_t2862754429  (*) (Func_2_t998693422 *, int32_t, const MethodInfo*))Func_2_Invoke_m1752772915_gshared)(__this, ___arg10, method)
// System.IAsyncResult System.Func`2<System.Int32,System.Guid>::BeginInvoke(T,System.AsyncCallback,System.Object)
extern "C"  Il2CppObject * Func_2_BeginInvoke_m390231142_gshared (Func_2_t998693422 * __this, int32_t ___arg10, AsyncCallback_t1369114871 * ___callback1, Il2CppObject * ___object2, const MethodInfo* method);
#define Func_2_BeginInvoke_m390231142(__this, ___arg10, ___callback1, ___object2, method) ((  Il2CppObject * (*) (Func_2_t998693422 *, int32_t, AsyncCallback_t1369114871 *, Il2CppObject *, const MethodInfo*))Func_2_BeginInvoke_m390231142_gshared)(__this, ___arg10, ___callback1, ___object2, method)
// TResult System.Func`2<System.Int32,System.Guid>::EndInvoke(System.IAsyncResult)
extern "C"  Guid_t2862754429  Func_2_EndInvoke_m1901584513_gshared (Func_2_t998693422 * __this, Il2CppObject * ___result0, const MethodInfo* method);
#define Func_2_EndInvoke_m1901584513(__this, ___result0, method) ((  Guid_t2862754429  (*) (Func_2_t998693422 *, Il2CppObject *, const MethodInfo*))Func_2_EndInvoke_m1901584513_gshared)(__this, ___result0, method)
