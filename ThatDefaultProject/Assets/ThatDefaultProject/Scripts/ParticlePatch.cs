using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace That
{
	[CreateAssetMenu(menuName = "ThatRFX/ParticlePatch")]
	public class ParticlePatch : ScriptableObject
	{
		[SerializeField] private GameObject[] _particlePrefabs;
		private List<GameObject> _uniqueParicles;
		[SerializeField] private float _longestEffectTime = 1.0f;
		[SerializeField] private bool _supressWarnings = false;

		private void Awake()
		{
			if (_particlePrefabs == null) return;

			Debug.Assert(_particlePrefabs.Length > 0, $"ParticlePrefabs is empty on ParticlePatch {name}");
			if (_particlePrefabs != null && _particlePrefabs.Length > 0)
			{
				_uniqueParicles = _particlePrefabs.ToList();
			}
		}

		public void PlayAttached(Transform transform)
		{
			if (!transform.gameObject.activeInHierarchy)
			{
#if UNITY_EDITOR
				if (_supressWarnings)
				{
					Debug.LogWarning($"[ParticlePatch] {name} is played on an inactive transform and is refusing to playattached.");
				}
#endif
				return;
			}
			GameObject obj = Instantiate(ReturnRandomPrefab(), transform);
			DestroyTimer destroyTimer = obj.AddComponent<DestroyTimer>();
			destroyTimer.Time = _longestEffectTime;
			destroyTimer.StartTimer();
		}

		public void PlayTrailing(Transform transform)
		{
			GameObject trailing = SpawnTrailing(transform);
			PlayAttached(trailing.transform);
		}

		public void PlayAttached(IHasTransform hasTransform) => PlayAttached(hasTransform.GetTransform());
		public void PlayTrailing(IHasTransform hasTransform) => PlayTrailing(hasTransform.GetTransform());

		private GameObject ReturnRandomPrefab()
		{
			if (_particlePrefabs.Length == 1)
			{
				return _particlePrefabs[0];
			}
			Debug.Assert(_particlePrefabs.Length != 0, $"ParticlePatch ({name}) is empty!");
			if (_uniqueParicles.Count <= 0)
			{
				_uniqueParicles = _particlePrefabs.ToList();
			}
			int randomIndex = Random.Range(0, _uniqueParicles.Count);
			GameObject randomPrefab = _uniqueParicles[randomIndex];
			_uniqueParicles.RemoveAt(randomIndex);

			return randomPrefab;
		}

		private GameObject SpawnTrailing(Transform transform)
		{
			GameObject obj = new("[ParticlePatch] Trailing Particles");
			obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
			DestroyTimer destroyTimer = obj.AddComponent<DestroyTimer>();
			destroyTimer.Time = _longestEffectTime;
			destroyTimer.StartTimer();
			return obj;
		}
	}
}
