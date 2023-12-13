using System;
using System.Collections.Generic;
using UnityEngine;

namespace that
{
    public static class LerpExtensions
    {
		/// <summary>
		/// A coroutine function that lerps the rotation to the target position.
		/// </summary>
		/// <param name="transform">The transform to rotate</param>
		/// <param name="targetPosition">Target position to look at</param>
		/// <param name="lerpSpeed">Speed it rotates</param>
		/// <param name="onEndLerpAction">Action to perform on the end of the coroutine</param>
		/// <param name="tollerance">Compares the dot product of the forward and direction with this value. As long as it is under this value, it will keep rotating. A lower tollerance yields lower accuracy and duration.</param>
		/// <returns></returns>
		public static IEnumerator<Transform> LerpRotationToTargetPosition(this Transform transform,
	Vector3 targetPosition, float lerpSpeed = 1.0f, Action onEndLerpAction = null, float tollerance = 0.999f)
		{
			float elapsedTime = 0;
			Vector3 direction = (targetPosition - transform.position).normalized;
			while (Vector3.Dot(transform.forward, direction) < tollerance)
			{
				Quaternion toRotation = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, lerpSpeed * elapsedTime);
				yield return null;
				elapsedTime += Time.deltaTime;
			}
			onEndLerpAction?.Invoke();

		}

		public static Vector3 LerpPositions(this List<Vector3> path, float t)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Count > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Count - 1));
			int index2 = Mathf.CeilToInt(t * (path.Count - 1));

			index1 = Mathf.Clamp(index1, 0, path.Count - 1);
			index2 = Mathf.Clamp(index2, 0, path.Count - 1);

			Vector3 point1 = path[index1];
			Vector3 point2 = path[index2];
			return Vector3.Lerp(point1, point2, t - index1);
		}
		public static Vector3 LerpPositions(this Vector3[] path, float t)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Length > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Length - 1));
			int index2 = Mathf.CeilToInt(t * (path.Length - 1));

			index1 = Mathf.Clamp(index1, 0, path.Length - 1);
			index2 = Mathf.Clamp(index2, 0, path.Length - 1);

			Vector3 point1 = path[index1];
			Vector3 point2 = path[index2];
			return Vector3.Lerp(point1, point2, t - index1);
		}

		public static void LerpTransforms(this List<Transform> path, float t, Transform outVal)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Count > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Count - 1));
			int index2 = Mathf.CeilToInt(t * (path.Count - 1));

			index1 = Mathf.Clamp(index1, 0, path.Count - 1);
			index2 = Mathf.Clamp(index2, 0, path.Count - 1);

			float segmentT = t * (path.Count - 1) - index1;

			Transform point1 = path[index1];
			Transform point2 = path[index2];
			outVal.SetPositionAndRotation(
				Vector3.Lerp(point1.position, point2.position, segmentT),
				Quaternion.Lerp(point1.rotation, point2.rotation, segmentT)
				);
			outVal.localScale = Vector3.Lerp(point1.localScale, point2.localScale, segmentT);
		}
		public static void LerpTransforms(this Transform[] path, float t, Transform outVal)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Length > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Length - 1));
			int index2 = Mathf.CeilToInt(t * (path.Length - 1));

			index1 = Mathf.Clamp(index1, 0, path.Length - 1);
			index2 = Mathf.Clamp(index2, 0, path.Length - 1);

			float segmentT = t * (path.Length - 1) - index1;

			Transform point1 = path[index1];
			Transform point2 = path[index2];
			outVal.SetPositionAndRotation(
				Vector3.Lerp(point1.position, point2.position, segmentT),
				Quaternion.Lerp(point1.rotation, point2.rotation, segmentT)
				);
			outVal.localScale = Vector3.Lerp(point1.localScale, point2.localScale, segmentT);
		}

		public static Vector3 LerpPositions(this List<Transform> path, float t)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Count > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Count - 1));
			int index2 = Mathf.CeilToInt(t * (path.Count - 1));

			index1 = Mathf.Clamp(index1, 0, path.Count - 1);
			index2 = Mathf.Clamp(index2, 0, path.Count - 1);

			Vector3 point1 = path[index1].position;
			Vector3 point2 = path[index2].position;
			return Vector3.Lerp(point1, point2, t - index1);
		}
		public static Vector3 LerpPositions(this Transform[] path, float t)
		{
			Debug.Assert(path != null, "Path is null");
			Debug.Assert(path.Length > 0, "Path is empty");

			t = Mathf.Clamp01(t);

			int index1 = Mathf.FloorToInt(t * (path.Length - 1));
			int index2 = Mathf.CeilToInt(t * (path.Length - 1));

			index1 = Mathf.Clamp(index1, 0, path.Length - 1);
			index2 = Mathf.Clamp(index2, 0, path.Length - 1);

			Vector3 point1 = path[index1].position;
			Vector3 point2 = path[index2].position;
			return Vector3.Lerp(point1, point2, t - index1);
		}
	}
}
