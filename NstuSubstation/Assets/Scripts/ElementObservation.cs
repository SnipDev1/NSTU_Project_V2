using System;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ElementObservation : MonoBehaviour
{   
    [Serializable]
    public class Element
    {
        public GameObject elementObject;
        public GameObject elementObservationPoint;

        public string elementName;
        public int elementId;

        public string elementHeaderText;
        public string elementBodyText;

        public AudioClip elementAudio;    
    }
    
    public List<Element> kElements = new();
    
    private int currentPoint = 0;
    private bool isFirstElement = true;
    private bool isLastElement = false;
    private AudioSource audioSource;
    [SerializeField] private GameObject imageFade;

    private void Start() { audioSource = Player.instance.GetComponent<AudioSource>(); /* Определение аудиосоурса */ }

    private void Update()
    {
        if (isFirstElement == false && isLastElement == false)
        {
            if (CheckAudioState() == false)
            {
                NextPoint();
            }
        }
    }
    
    public void NextPoint() // Переход к следующей точке осмотра объекта    
     {       
         // ВАЖНО понимать, что из-за этой логики телепорт игрока начинается с 1-й точки, а не с 0-й.

         if (currentPoint == 0)
             isLastElement = true; // Проверка последний ли элемент (0?)
         
         if (currentPoint < kElements.Count)
             kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта
         isFirstElement = false; // Проверка первый ли элемент (1?)
 
         currentPoint = (currentPoint + 1) % kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта
         
         Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
         Player.instance.GetComponentInChildren<Camera>().transform.LookAt(kElements[currentPoint].elementObject.transform); // Резкий переход камеры на объект ??
         audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
         audioSource.Play(); // Проигрыш аудио
         
         kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = true; // Включение аутлайна новой точки
     }
    
    public void PreviousPoint() // Переход к предыдущей точке осмотра объекта    
    {       
        // ВАЖНО понимать, что из-за этой логики телепорт игрока начинается с 1-й точки, а не с 0-й.

        if (currentPoint != 0)
        {
            if (currentPoint < kElements.Count)
                kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта

            currentPoint = (currentPoint - 1) % kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта

            Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
            Player.instance.transform.LookAt(kElements[currentPoint].elementObject.transform); // Резкий переход камеры на объект ??
            audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
            audioSource.Play(); // Проигрыш аудио

            kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = true; // Включение аутлайна новой точки
        }
    }

    public void ChangePlayerPosition()
    {
        Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
    } // Вызывается в Screenfade.cs
    
    public bool CheckAudioState() // Проверка окончания аудио
    {
        if (audioSource.isPlaying) return true; // Если аудио играет = TRUE
        if (!audioSource.isPlaying && (audioSource.time == 0f)) return false; // Если аудио не играет = FALSE
        
        return false;
    }

    public void PauseAudio()
    {
        Player.instance.GetComponent<AudioSource>().Pause();
    } // Пауза аудио

    public void ContinueAudio()
    {
        Player.instance.GetComponent<AudioSource>().UnPause();
    } // Продолжение аудио

    public void ResetAudio() // Начать аудио сначала
    {
        audioSource.UnPause();
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    } // Стоп аудио
    
    public void ExitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    } // Выход в меню (хаб)
}
