using System;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Excursion.ElementObservation
{
    public class ElementObservation : MonoBehaviour
    {
        [Serializable]
        public class Element
        {
            public GameObject elementObject;
            public Outlinable elementOutlinable;
            public GameObject elementObservationPoint;

            public string elementName;
            public int elementId;

            public string elementHeaderText;
            public string elementBodyText;

            public AudioClip elementAudio;
        }

        public List<Element> kElements = new();
  
        private AudioSource audioSource;
        private Transform playerCamera;
        
        private bool isFirstElement = true;
        private bool isLastElement;
        
        public int currentPoint;

        private void Start()
        {
            audioSource = Player.instance.GetComponent<AudioSource>(); // Определение аудиосоурса у игрока
            playerCamera = Player.instance.GetComponentInChildren<Camera>().transform; // Определение камеры игрока
        }

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
                kElements[currentPoint].elementOutlinable.OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта
        
            isFirstElement = false; // Проверка первый ли элемент (1?)

            currentPoint = (currentPoint + 1) % kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта

            Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
            playerCamera.transform.LookAt(kElements[currentPoint].elementObject.transform); // Резкий переход камеры на объект ??

            audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
            audioSource.Play();

            kElements[currentPoint].elementOutlinable.OutlineParameters.Enabled = true; // Включение аутлайна новой точки
        }

        public void PreviousPoint() // Переход к предыдущей точке осмотра объекта    
        {
            // ВАЖНО понимать, что из-за этой логики телепорт игрока начинается с 1-й точки, а не с 0-й.

            if (currentPoint != 0)
            {
                if (currentPoint < kElements.Count)
                    kElements[currentPoint].elementOutlinable.OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта

                currentPoint = (currentPoint - 1) % kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта

                Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
                playerCamera.transform.LookAt(kElements[currentPoint].elementObject.transform); // Резкий переход камеры на объект ??
            
                audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
                audioSource.Play(); // Проигрыш аудио

                kElements[currentPoint].elementOutlinable.OutlineParameters.Enabled = true; // Включение аутлайна новой точки
            }
        }

        private bool CheckAudioState() // Проверка окончания аудио
        {
            switch (audioSource.isPlaying)
            {
                case true:
                    return true; // Если аудио играет = TRUE
                case false when (audioSource.time == 0f):
                    return false; // Если аудио не играет = FALSE
                default:
                    return false;
            }
        }

        public void PauseAudio() // Пауза аудио
        {
            Player.instance.GetComponent<AudioSource>().Pause();
        } 

        public void ContinueAudio() // Продолжение аудио
        {
            Player.instance.GetComponent<AudioSource>().UnPause();
        } 

        public void ResetAudio() // Начать аудио сначала
        {
            audioSource.UnPause();
            audioSource.Play();
        }

        public void StopAudio() // Стоп аудио
        {
            audioSource.Stop();
        } 

        public void ExitToMenu() // Выход в меню (хаб)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        } 
    }
}