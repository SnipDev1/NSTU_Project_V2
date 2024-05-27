using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public class SceneController: MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        private const int LoadSceneId = 4;

        public void LoadSceneAsync(int sceneId)
        {
            SceneManager.LoadSceneAsync(LoadSceneId);
            SceneManager.LoadSceneAsync(sceneId);
            // StartCoroutine(LoadSceneCoroutine(LoadSceneId));
            // StartCoroutine(LoadSceneCoroutine(sceneId));
        }

        private static IEnumerator LoadSceneCoroutine(int sceneId)
        {
            var operation = SceneManager.LoadSceneAsync(sceneId);
            while(!operation.isDone)
            {
                yield return null;
            }
        }
        
    }
}
