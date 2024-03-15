using UnityEngine;

namespace Multimedia
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private MultimediaController multimediaController;
        public void PauseAudio()
        {
            multimediaController.PauseAudio();
        }
        public void ContinueAudio()
        {
            multimediaController.ContinueAudio();
        }
        public void ResetAudio()
        {
            multimediaController.ResetAudio();
        }
        public void StopAudio()
        {
            multimediaController.StopAudio();
        }

        public void OnNext()
        {
            StartCoroutine(multimediaController.NextClip());
            PointerController.Instance.ZeroRot();

        }

        public void OnPrevious()
        {
            StartCoroutine(multimediaController.PreviousClip());
            PointerController.Instance.ZeroRot();

        }
    }
}