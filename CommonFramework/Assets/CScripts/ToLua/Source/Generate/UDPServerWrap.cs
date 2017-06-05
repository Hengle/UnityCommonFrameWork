﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UDPServerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UDPServer), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("CloseReceiveSocket", CloseReceiveSocket);
		L.RegFunction("InitUDPServer", InitUDPServer);
		L.RegFunction("SetSendSucessCallback", SetSendSucessCallback);
		L.RegFunction("SetReceive", SetReceive);
		L.RegFunction("SendUDPMsg", SendUDPMsg);
		L.RegFunction("New", _CreateUDPServer);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUDPServer(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UDPServer obj = new UDPServer();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UDPServer.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseReceiveSocket(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			obj.CloseReceiveSocket();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitUDPServer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 5);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			string arg0 = ToLua.CheckString(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			string arg2 = ToLua.CheckString(L, 4);
			int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
			obj.InitUDPServer(arg0, arg1, arg2, arg3);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSendSucessCallback(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.SetSendSucessCallback(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetReceive(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.SetReceive(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendUDPMsg(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UDPServer obj = (UDPServer)ToLua.CheckObject(L, 1, typeof(UDPServer));
			byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
			obj.SendUDPMsg(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, UDPServer.Instance);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

