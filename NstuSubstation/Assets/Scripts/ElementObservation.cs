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
        public Outlinable outlinable;
        public GameObject elementObservationPoint;

        public string elementName;
        public int elementId;

        public string elementHeaderText;
        public string elementBodyText;

        public AudioClip elementAudio;
    }

    public List<Element> kElements = new();

    public int currentPoint;
    private bool _isFirstElement = true;
    private bool _isLastElement;
    private AudioSource _audioSource;
    private Transform _playerCamera;
    // [SerializeField] private GameObject imageFade;

    private void Start()
    {
        _audioSource = Player.instance.GetComponent<AudioSource>(); /* Определение аудиосоурса */
        _playerCamera = Player.instance.GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        if (_isFirstElement == false && _isLastElement == false)
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
            _isLastElement = true; // Проверка последний ли элемент (0?)

        if (currentPoint < kElements.Count)
            kElements[currentPoint].outlinable.OutlineParameters.Enabled =
                false; // Выключение предыдущего OUTLINE объекта
        // kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта
        _isFirstElement = false; // Проверка первый ли элемент (1?)

        currentPoint =
            (currentPoint + 1) %
            kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта

        Player.instance.transform.position =
            kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке

        _playerCamera.transform.LookAt(kElements[currentPoint].elementObject
            .transform); // Резкий переход камеры на объект ??

        // Player.instance.GetComponentInChildren<Camera>().transform.LookAt(kElements[currentPoint].elementObject.transform); // Резкий переход камеры на объект ??
        _audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
        _audioSource.Play();

        kElements[currentPoint].outlinable.OutlineParameters.Enabled = true; // Включение аутлайна новой точки
        // kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = true; // Включение аутлайна новой точки
    }

    public void PreviousPoint() // Переход к предыдущей точке осмотра объекта    
    {
        // ВАЖНО понимать, что из-за этой логики телепорт игрока начинается с 1-й точки, а не с 0-й.
        if (currentPoint == 0)
            return;

        if (currentPoint != 0)
        {
            if (currentPoint < kElements.Count)
                kElements[currentPoint].outlinable.OutlineParameters.Enabled =
                    false; // Выключение предыдущего OUTLINE объекта

            currentPoint =
                (currentPoint - 1) %
                kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта

            Player.instance.transform.position =
                kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
            _playerCamera.transform.LookAt(kElements[currentPoint].elementObject
                .transform); // Резкий переход камеры на объект ??
            _audioSource.clip = kElements[currentPoint].elementAudio; // Задаем клип аудиосоурсу, получая его с объекта
            _audioSource.Play(); // Проигрыш аудио

            kElements[currentPoint].outlinable.OutlineParameters.Enabled = true; // Включение аутлайна новой точки


            // kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = true; // Включение аутлайна новой точки
        }
    }

    public void ChangePlayerPosition()
    {
        Player.instance.transform.position =
            kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
    } // Вызывается в Screenfade.cs

    private bool CheckAudioState() // Проверка окончания аудио
    {
        switch (_audioSource.isPlaying)
        {
            case true:
                return true; // Если аудио играет = TRUE
            case false when (_audioSource.time == 0f):
                return false; // Если аудио не играет = FALSE
            default:
                return false;
        }
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
        _audioSource.UnPause();
        _audioSource.Play();
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    } // Стоп аудио

    public void ExitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    } // Выход в меню (хаб)
}