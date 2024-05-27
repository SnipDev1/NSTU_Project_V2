using System.Collections;
using PostProcessingScripts;
using UnityEngine;

public class PointsTeleport : MonoBehaviour
{
    private int currentPoint;
    
    [SerializeField] private GameObject[] teleportPoints;
    [SerializeField] private GameObject playerVR;
    [SerializeField] private GameObject playerVRCamera;

    [SerializeField] private FadeEffect fadeEffect;

    private void NextTeleportPoint()
    {
        currentPoint = (currentPoint + 1) % teleportPoints.Length;

        playerVR.transform.position = teleportPoints[currentPoint].transform.position;
        playerVRCamera.transform.LookAt(teleportPoints[currentPoint].transform);
    }

    private void PreviousTeleportPoint()
    {
        currentPoint = (currentPoint - 1) % teleportPoints.Length;

        playerVR.transform.position = teleportPoints[currentPoint].transform.position;
        playerVRCamera.transform.LookAt(teleportPoints[currentPoint].transform); 
    }

    #region TooMuch~~~code
    private IEnumerator NextPointShader()
    {
        yield return StartCoroutine(fadeEffect.DecreaseOpacity());
        NextTeleportPoint();
        yield return StartCoroutine(fadeEffect.IncreaseOpacity());
    }

    private IEnumerator PreviousPointShader()
    {
        yield return StartCoroutine(fadeEffect.DecreaseOpacity());
        PreviousTeleportPoint();
        yield return StartCoroutine(fadeEffect.IncreaseOpacity());
    }

    public void OnNextPoint() => StartCoroutine(nameof(NextPointShader));
    public void OnPreviousPoint() => StartCoroutine(nameof(PreviousPointShader));
    
    #endregion
}
