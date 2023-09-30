using System.Collections;
using UnityEngine;

namespace that
{
    public class MaterialFlicker : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Material _matToFlickerTo;
        [SerializeField] private float _flickerLength = 0.5f;
        private Coroutine _resetMaterialCoroutine;

        private void Awake()
        {
            if (_skinnedMeshRenderer == null)
            {
                _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            }
        }

        public void FlickerForSeconds(float length)
        {
            var originalMat = _skinnedMeshRenderer.material;
            _skinnedMeshRenderer.material = _matToFlickerTo;
            if (_resetMaterialCoroutine != null)
            {
                StopCoroutine(_resetMaterialCoroutine);
            }
            _resetMaterialCoroutine = StartCoroutine(ResetMaterialCoroutine(originalMat, length));
        }

        public void Flicker()
        {
            FlickerForSeconds(_flickerLength);
        }

        private IEnumerator ResetMaterialCoroutine(Material mat, float length)
        {
            yield return new WaitForSeconds(length);
            _skinnedMeshRenderer.material = mat;
        }
    }
}
