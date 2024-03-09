using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiLevelController
{
    public class LevelInfo : MonoBehaviour
    {
        public string levelName = "DefaultName";
        public Sprite levelSprite;
        public int levelSceneId;
        public GameObject levelGameObject;
        [SerializeField] private TextMeshProUGUI levelNameTmp;
        [SerializeField] private Image levelImage;


        private void Start()
        {
            SetVariables();
        }

        private void SetVariables()
        {
            SetTextFromVariable();
            SetGameObject();
            SetImageFromVariable();
        }

        private void SetGameObject()
        {
            if (levelGameObject == null)
            {
                levelGameObject = gameObject;
            }
        }

        private void SetImageFromVariable()
        {
            levelImage.sprite = levelSprite;
        }

        private void SetTextFromVariable()
        {
            levelNameTmp.text = levelName;
        }
    }
}