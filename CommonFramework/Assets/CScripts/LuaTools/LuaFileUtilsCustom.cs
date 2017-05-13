﻿using UnityEngine;
using System.Collections;
using System.IO;
namespace LuaInterface
{
	public class LuaFileUtilsCustom:LuaFileUtils
	{
		public LuaFileUtilsCustom():base()
		{
			instance = this;
		}

		public override byte[] ReadFile (string fileName)
		{
//			Debug.LogError ("fileName  " + fileName);
			if (!beZip)
			{
				string path = FindFile(fileName);
				byte[] str = null;

				if (!string.IsNullOrEmpty(path) && File.Exists(path))
				{
					#if !UNITY_WEBPLAYER
					str = File.ReadAllBytes(path);
					#else
					throw new LuaException("can't run in web platform, please switch to other platform");
					#endif
				}

				return str;
			}
			else
			{
				return ReadZipFile(fileName);
			}
		}

		private byte[] ReadZipFile(string fileName)
		{
//			Debug.LogError ("LuaFileUtilsCustom  ReadZipFile  " + fileName);
			byte[] buffer = new byte[0];

			AssetBundle ab = AssetsManager.Instance.GetLuaBundle (fileName);
			TextAsset ta = ab.LoadAsset<TextAsset>(fileName);
			if (ta != null)
			{
				buffer = ta.bytes;
				Resources.UnloadAsset(ta);
			}
			return buffer;
		}




	}
}

