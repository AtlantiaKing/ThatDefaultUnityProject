using System;
using System.Collections.Generic;
using UnityEngine;

namespace That
{
	public static class MonobehaviorExtensions
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

		/// <summary>
		/// Alternative for SetEnableAll. Set the active state of all in the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="state"></param>
		public static void SetActiveAll<T>(this IEnumerable<T> container, bool state) where T : Component
		{
			foreach (T item in container)
			{
				item.gameObject.SetActive(state);
			}
		}
		/// <summary>
		/// Alternative for SetEnableAll. Set the active state of all in the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="state"></param>
		public static void SetActiveAll(this IEnumerable<GameObject> container, bool state)
		{
			foreach (GameObject item in container)
			{
				item.SetActive(state);
			}
		}
		/// <summary>
		/// Alternative for SetActiveAll. Set the active state of all in the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="state"></param>
		public static void SetEnableAll<T>(this IEnumerable<T> container, bool state) where T : Component
		{
			SetActiveAll(container, state);
		}
		/// <summary>
		/// Alternative for SetActiveAll. Set the active state of all in the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="state"></param>
		public static void SetEnableAll(this IEnumerable<GameObject> container, bool state)
		{
			SetActiveAll(container, state);
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
