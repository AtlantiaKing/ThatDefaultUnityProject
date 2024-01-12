using UnityEngine;

namespace that
{
    public static class SpawningUtils
    {
		// https://discussions.unity.com/t/instantiate-inactive-object/39830/3

		/// <summary>
		/// Will instantiate an object disabled preventing it from calling Awake/OnEnable.
		/// </summary>
		public static T InstantiateDisabled<T>(this T original, Transform parent = null, bool worldPositionStays = false) where T : Object
		{
			if (!GetActiveState(original))
			{
				return Object.Instantiate(original, parent, worldPositionStays);
			}

			(GameObject coreObject, Transform coreObjectTransform) = InstantiateDisabledRootObject(parent);
			T instance = Object.Instantiate(original, coreObjectTransform, worldPositionStays);
			SetActiveState(instance, false);
			SetParent(instance, parent, worldPositionStays);
			Object.Destroy(coreObject);
			return instance;
		}

		/// <summary>
		/// Will instantiate an object disabled preventing it from calling Awake/OnEnable.
		/// </summary>
		public static T InstantiateDisabled<T>(this T original, Vector3 position, Quaternion rotation, Transform parent = null) where T : Object
		{
			if (!GetActiveState(original))
			{
				return Object.Instantiate(original, position, rotation, parent);
			}

			(GameObject coreObject, Transform coreObjectTransform) = InstantiateDisabledRootObject(parent);
			T instance = Object.Instantiate(original, position, rotation, coreObjectTransform);
			SetActiveState(instance, false);
			SetParent(instance, parent, false);
			Object.Destroy(coreObject);
			return instance;
		}

		private static (GameObject coreObject, Transform coreObjectTransform) InstantiateDisabledRootObject(Transform parent = null)
		{
			GameObject rootObject = new();
			rootObject.SetActive(false);
			Transform rootTransform = rootObject.transform;
			rootTransform.SetParent(parent);

			return (rootObject, rootTransform);
		}

		private static bool GetActiveState<T>(T obj) where T : Object
		{
			switch (obj)
			{
				case GameObject gameObject:
					{
						return gameObject.activeSelf;
					}
				case Component component:
					{
						return component.gameObject.activeSelf;
					}
				default:
					{
						return false;
					}
			}
		}

		private static void SetActiveState<T>(T obj, bool state) where T : Object
		{
			switch (obj)
			{
				case GameObject gameObject:
					{
						gameObject.SetActive(state);

						break;
					}
				case Component component:
					{
						component.gameObject.SetActive(state);

						break;
					}
			}
		}

		private static void SetParent<T>(T obj, Transform parent, bool worldPositionStays) where T : Object
		{
			switch (obj)
			{
				case GameObject gameObject:
					{
						gameObject.transform.SetParent(parent, worldPositionStays);
						break;
					}
				case Component component:
					{
						component.transform.SetParent(parent, worldPositionStays);
						break;
					}
			}
		}

		/// <summary>
		/// Gives a random rotation based on the quaternion it is performed upon. The axis you choose to lock will be unnaffected
		/// </summary>
		/// <param name="quaternion">Original quaternion that will be used as fallback value if an axis is locked.</param>
		/// <param name="lockAxis">The axis you choose to lock will be unnaffected</param>
		/// <returns>Editted quaternion</returns>
		public static Quaternion GetRandomRotation(this Quaternion quaternion, LockAxis lockAxis = LockAxis.None)
		{
			// Actual Code
			if (lockAxis == LockAxis.All) return quaternion;

			quaternion = Quaternion.Euler(
				lockAxis.IsLockedOn(LockAxis.X) ? quaternion.eulerAngles.x : Random.Range(0, 360.0f),
				lockAxis.IsLockedOn(LockAxis.Y) ? quaternion.eulerAngles.y : Random.Range(0, 360.0f),
				lockAxis.IsLockedOn(LockAxis.Z) ? quaternion.eulerAngles.z : Random.Range(0, 360.0f)
				);
			return quaternion;
		}
	}
}
