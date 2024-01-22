using System;
using System.Collections.Generic;
using UnityEngine;

namespace That
{
    public static class ChildrenExtensions
    {
		public static void ForEachChild(this Transform transform, Action<Transform, int> action)
		{
			for (int i = 0; i < transform.childCount; ++i)
			{
				action(transform.GetChild(i), i);
			}
		}
		public static void ForEachChild(this MonoBehaviour mb, Action<Transform, int> action)
		{
			ForEachChild(mb.transform, action);
		}
		public static void ForEachChild(this GameObject go, Action<Transform, int> action)
		{
			ForEachChild(go.transform, action);
		}
		public static void ForEachChild(this Transform transform, Action<Transform> action)
		{
			foreach (Transform child in transform)
			{
				action(child);
			}
		}
		public static void ForEachChild(this MonoBehaviour mb, Action<Transform> action)
		{
			ForEachChild(mb.transform, action);
		}
		public static void ForEachChild(this GameObject go, Action<Transform> action)
		{
			ForEachChild(go.transform, action);
		}
		public static List<Transform> AllChildren(this Transform transform)
		{
			List<Transform> list = new();
			foreach (Transform t in transform)
			{
				list.Add(t);
			}
			return list;
		}
	}
}
