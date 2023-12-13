using System;
using System.Collections.Generic;
using UnityEngine;

namespace that
{
	public static partial class MonobehaviorExtensions
	{
		/// <summary>
		/// if null, tries to get component, if still null, adds component. Returns component.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mb"></param>
		/// <param name="comp"></param>
		/// <returns></returns>
		public static T GetOrAddComponent<T>(this MonoBehaviour mb, T comp = null) where T : Component
		{
			if (comp != null)
			{
				return comp;
			}
			comp = mb.GetComponent<T>();
			if (comp != null)
			{
				return comp;
			}
			return mb.gameObject.AddComponent<T>();
		}
		public static void SetActiveAll<T>(this IEnumerable<T> container, bool state) where T : Component
		{
			foreach (T item in container)
			{
				item.gameObject.SetActive(state);
			}
		}
		public static void SetActiveAll(this IEnumerable<GameObject> container, bool state)
		{
			foreach (GameObject item in container)
			{
				item.SetActive(state);
			}
		}
		public static void EnableScript<T>(this T obj) where T : MonoBehaviour
		{
			obj.enabled = true;
		}
		public static void DisableScript<T>(this T obj) where T : MonoBehaviour
		{
			obj.enabled = false;
		}
	}
}
