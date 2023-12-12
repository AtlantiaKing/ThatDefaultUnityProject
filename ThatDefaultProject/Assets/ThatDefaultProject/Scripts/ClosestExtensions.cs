using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace that
{
	public static partial class ClosestExtensions
	{
		public static T FindClosest<T>(this IEnumerable<T> collection, Vector3 position) where T : Component
		{
			return collection
				.Where(t => t != null)
				.OrderBy(t => (t.transform.position - position).sqrMagnitude)
				.FirstOrDefault();
		}
		public static T FindFurthest<T>(this IEnumerable<T> collection, Vector3 position) where T : Component
		{
			return collection
				.Where(t => t != null)
				.OrderBy(t => (t.transform.position - position).sqrMagnitude)
				.LastOrDefault();
		}
		public static GameObject FindClosest(this IEnumerable<GameObject> collection, Vector3 position)
		{
			return collection
				.Where(t => t != null)
				.OrderBy(t => (t.transform.position - position).sqrMagnitude)
				.FirstOrDefault();
		}
		public static GameObject FindFurthest(this IEnumerable<GameObject> collection, Vector3 position)
		{
			return collection
				.Where(t => t != null)
				.OrderBy(t => (t.transform.position - position).sqrMagnitude)
				.LastOrDefault();
		}
		public static Vector3 FindClosest(this IEnumerable<Vector3> collection, Vector3 position)
		{
			return collection
				.OrderBy(pos => (pos - position).sqrMagnitude)
				.FirstOrDefault();
		}
		public static Vector3 FindFurthest(this IEnumerable<Vector3> collection, Vector3 position)
		{
			return collection
				.OrderBy(pos => (pos - position).sqrMagnitude)
				.LastOrDefault();
		}
		public static IEnumerable<T> SortByDistance<T>(this IEnumerable<T> collection, Vector3 targetPosition) where T : Component
		{
			return collection
				.OrderBy(obj => Vector3.Distance(obj.transform.position, targetPosition));
		}
	}
}
