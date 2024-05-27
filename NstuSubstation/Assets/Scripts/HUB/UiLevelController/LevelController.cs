using System;
using System.Collections.Generic;
using UnityEngine;


namespace UiLevelController
{
    public class LevelController : MonoBehaviour
    {
        [Serializable]
        public class LevelPanels
        {
            public LevelInfo levelInfo;
        }

        [SerializeField] private List<LevelPanels> levelPanelsList = new();
        private int _currentLevelIndex;
        private int _amountOfLevels;

        private void Start()
        {
            DisplayAllLevels();
            ActivateFirstLevel();
            _amountOfLevels = GetAmountOfLevels();
        }

        private void DisplayAllLevels()
        {
            foreach (var levelPanel in levelPanelsList)
            {
                Debug.Log($"Name - {levelPanel.levelInfo.levelName}");
            }
        }

        private void DeactivateAllLevels()
        {
            foreach (var levelPanel in levelPanelsList)
            {
                levelPanel.levelInfo.levelGameObject.SetActive(false);
            }
        }

        private void SetStateOfLevelByIndex(int index, bool state)
        {
            levelPanelsList[index].levelInfo.levelGameObject.SetActive(state);
        }

        private int GetAmountOfLevels()
        {
            return levelPanelsList.Count;
        }

        private int GetSceneIdByIndex(int index)
        {
            return levelPanelsList[index].levelInfo.levelSceneId;
        }

        private void DisplayCurrentIndex()
        {
            Debug.Log(_currentLevelIndex);
        }

        private void ActivateFirstLevel()
        {
            DeactivateAllLevels();
            SetStateOfLevelByIndex(0, true);
        }

        public void PreviousLevel()
        {
           DeactivateAllLevels();
            if (_currentLevelIndex - 1 >= 0)
            {
                _currentLevelIndex--;
            }
            else
            {
                _currentLevelIndex = GetAmountOfLevels() - 1;
            }

            SetStateOfLevelByIndex(_currentLevelIndex, true);
            DisplayCurrentIndex();
        }

        public void NextLevel()
        {
            DeactivateAllLevels();
            if (_currentLevelIndex + 1 < _amountOfLevels)
            {
                _currentLevelIndex++;
            }
            else
            {
                _currentLevelIndex = 0;
            }

            SetStateOfLevelByIndex(_currentLevelIndex, true);
            DisplayCurrentIndex();
        }

        public void PlayLevel()
        {
            SceneController.SceneController.Instance.LoadSceneAsync(GetSceneIdByIndex(_currentLevelIndex));
        }
    }
}