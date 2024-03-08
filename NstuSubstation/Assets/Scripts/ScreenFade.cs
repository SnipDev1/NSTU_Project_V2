using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0f;

    public bool nextButton;
    public bool prevButton;
    public ElementObservation element;

    private void Awake() { element = FindObjectOfType<ElementObservation>(); }

    private IEnumerator NextFade()
    {
       Image fadeImage = GetComponent<Image>();
       Color fadeColor = fadeImage.color;
       element.StopAudio();

       while (fadeColor.a < 1f)
       {
           fadeColor.a += fadeSpeed * Time.deltaTime;
           fadeImage.color = fadeColor;
           yield return null;
       }
       
       element.NextPoint();
       element.ChangePlayerPosition();
       
       while (fadeColor.a >= 0f) 
       {
           fadeColor.a -= fadeSpeed * Time.deltaTime;
           fadeImage.color = fadeColor;
           yield return null;
       }

       gameObject.SetActive(false);
    }
    
    private IEnumerator PrevFade()
    {
        if (element.currentPoint != 0)
        {
            Image fadeImage = GetComponent<Image>();
            Color fadeColor = fadeImage.color;
            element.StopAudio();

            while (fadeColor.a < 1f)
            {
                fadeColor.a += fadeSpeed * Time.deltaTime;
                fadeImage.color = fadeColor;
                yield return null;
            }

            element.PreviousPoint();

            while (fadeColor.a >= 0f)
            {
                fadeColor.a -= fadeSpeed * Time.deltaTime;
                fadeImage.color = fadeColor;
                yield return null;
            }

            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnNextButton()
    {
        nextButton = true;
        prevButton = false;
    }

    public void OnPrevButton()
    {
        prevButton = true;
        nextButton = false;
    }
    
    private void OnEnable()
    {
        if(nextButton == true)
            StartCoroutine(nameof(NextFade));

        if (prevButton == true)
            StartCoroutine(nameof(PrevFade));
    }
}
