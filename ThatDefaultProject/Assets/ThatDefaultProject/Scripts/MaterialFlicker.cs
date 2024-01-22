using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace That
{
    public class MaterialFlicker : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshRenderers;
        [SerializeField] private Material _matToFlickerTo;
        [SerializeField] private float _flickerLength = 0.5f;
        private Coroutine _resetMaterialCoroutine;

        public void FlickerForSeconds(float length)
        {
            List<Tuple<MeshRenderer, Material>> originalMeshRendMats = new();
            foreach (MeshRenderer meshRenderer in _meshRenderers) 
            {
                originalMeshRendMats.Add(new(meshRenderer, meshRenderer.material));
                meshRenderer.material = _matToFlickerTo;
            }
            if (_resetMaterialCoroutine != null)
            {
                StopCoroutine(_resetMaterialCoroutine);
            }
            _resetMaterialCoroutine = StartCoroutine(ResetMaterialCoroutine(originalMeshRendMats, length));
        }

        public void Flicker()
        {
            FlickerForSeconds(_flickerLength);
        }

        private IEnumerator ResetMaterialCoroutine(List<Tuple<MeshRenderer, Material>> originalMeshRendMats, float length)
        {
            yield return new WaitForSeconds(length);
            foreach (var meshRendMat in originalMeshRendMats)
            {
                meshRendMat.Item1.material = meshRendMat.Item2;
            }
        }
    }
}
