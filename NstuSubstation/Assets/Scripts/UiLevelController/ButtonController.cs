using UnityEngine;

namespace UiLevelController
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private LevelController levelController;
        public void NextLevel()
        {
            levelController.NextLevel();
        }

        public void PreviousLevel()
        {
            levelController.PreviousLevel();
        }

        public void PlayLevel()
        {
            levelController.PlayLevel();
        }
        
    }
}
