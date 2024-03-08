using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessingScripts
{
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField] private Volume volume;
        [SerializeField] private float startOpacity = -10f;
        [SerializeField] private float endOpacity = 10f;
        [SerializeField] private float fadeSpeed = 1f;
        [SerializeField] private float currentOpacity;
        private ColorAdjustments _colorAdjustments;

        private void Start()
        {
            volume.profile.TryGet(out _colorAdjustments);
        }

        private void Update()
        {
            currentOpacity = GetOpacity();
        }

        private IEnumerator IncreaseOpacity()
        {
            while (GetOpacity() < endOpacity)
            {
                // Debug.Log(GetOpacity());
                _colorAdjustments.postExposure.value += fadeSpeed * Time.deltaTime;
                yield return null;
            }

            _colorAdjustments.postExposure.value = endOpacity;
            StopCoroutine(nameof(IncreaseOpacity));
        }

        private IEnumerator DecreaseOpacity()
        {
            while (GetOpacity() >= startOpacity)
            {
                // Debug.Log(GetOpacity());
                _colorAdjustments.postExposure.value -= fadeSpeed * Time.deltaTime;
                yield return null;
            }

            _colorAdjustments.postExposure.value = startOpacity;
            StopCoroutine(nameof(DecreaseOpacity));
        }

        private IEnumerator SetZeroOpacity()
        {
            StopCoroutine(nameof(IncreaseOpacity));
            StopCoroutine(nameof(DecreaseOpacity));
            if (GetOpacity() < 0f)
            {
                while (GetOpacity() <= 0f)
                {
                    _colorAdjustments.postExposure.value += fadeSpeed * Time.deltaTime;
                    yield return null;
                }

                _colorAdjustments.postExposure.value = 0f;
            }
            else if (GetOpacity() > 0f)
            {
                while (GetOpacity() >= 0f)
                {
                    _colorAdjustments.postExposure.value -= fadeSpeed * Time.deltaTime;
                    yield return null;
                }

                _colorAdjustments.postExposure.value = 0f;
            }

            StopCoroutine(nameof(SetZeroOpacity));
        }

        public void OnIncreaseButton()
        {
            StartCoroutine(nameof(IncreaseOpacity));
        }

        public void OnDecreaseButton()
        {
            StartCoroutine(nameof(DecreaseOpacity));
        }

        public void OnResetButton()
        {
            StartCoroutine(nameof(SetZeroOpacity));
        }

        private float GetOpacity()
        {
            return _colorAdjustments.postExposure.value;
        }
    }
}