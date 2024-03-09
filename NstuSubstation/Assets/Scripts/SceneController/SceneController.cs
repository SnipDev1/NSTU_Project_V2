using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public abstract class SceneController: MonoBehaviour
    {
        public static void LoadSceneAsync(int sceneId)
        {
            SceneManager.LoadSceneAsync(sceneId);
        }
    }
}
