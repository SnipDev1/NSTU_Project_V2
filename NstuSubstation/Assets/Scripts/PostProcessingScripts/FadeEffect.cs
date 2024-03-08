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
        private ColorAdjustments _colorAdjustments;

        private void Start()
        {
            volume.profile.TryGet(out _colorAdjustments);
        }
        
        
        public IEnumerator IncreaseOpacity()
        {
            while (GetOpacity() < endOpacity)
            {
                // Debug.Log(GetOpacity());
                _colorAdjustments.postExposure.value += fadeSpeed * Time.deltaTime;
                yield return null;
            }

            _colorAdjustments.postExposure.value = endOpacity;
            
        }

        public IEnumerator DecreaseOpacity()
        {
            while (GetOpacity() >= startOpacity)
            {
                // Debug.Log(GetOpacity());
                _colorAdjustments.postExposure.value -= fadeSpeed * Time.deltaTime;
                yield return null;
            }


            _colorAdjustments.postExposure.value = startOpacity;
            
        }
        
        /*
        private IEnumerator SetZeroOpacity()
        {
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

            // StopCoroutine(nameof(SetZeroOpacity));
        }
        */
        private float GetOpacity()
        {
            return _colorAdjustments.postExposure.value;
        }
    }
}