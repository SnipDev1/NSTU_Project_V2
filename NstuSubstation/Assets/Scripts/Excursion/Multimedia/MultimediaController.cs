using System.Collections;
using Excursion.ElementObservation;
using PostProcessingScripts;
using UnityEngine;

namespace Multimedia
{
    public class MultimediaController : MonoBehaviour
    {
        [SerializeField] private ElementObservation elementObservation;
        [SerializeField] private FadeEffect fadeEffect;
        private bool _isClipCoroutineActive = false;
        private void Awake()
        {
            if (elementObservation == null)
            {
                elementObservation = FindObjectOfType<ElementObservation>();
            }
        }
        

        public IEnumerator NextClip()
        {
            if (_isClipCoroutineActive) yield break; // Чтобы не было наложения клипов
            _isClipCoroutineActive = true;
            elementObservation.StopAudio();
            yield return StartCoroutine(fadeEffect.DecreaseOpacity());
            elementObservation.NextPoint();
            yield return StartCoroutine(fadeEffect.IncreaseOpacity());
            _isClipCoroutineActive = false;

        }
        public IEnumerator PreviousClip()
        {
            if (_isClipCoroutineActive) yield break; 
            _isClipCoroutineActive = true;
            elementObservation.StopAudio();
            yield return StartCoroutine(fadeEffect.DecreaseOpacity());
            elementObservation.PreviousPoint();
            yield return StartCoroutine(fadeEffect.IncreaseOpacity());
            _isClipCoroutineActive = false;
        }

        public void PauseAudio()
        {
            elementObservation.PauseAudio();
        }
        public void ContinueAudio()
        {
            elementObservation.ContinueAudio();
        }
        public void ResetAudio()
        {
            elementObservation.ResetAudio();
        }
        public void StopAudio()
        {
            elementObservation.StopAudio();
        }
    }
}
