using System;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;
using UnityEngine.Serialization;
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

        public AudioSource elementAudio;    
    }
    
    public List<Element> kElements = new();
    private int currentPoint = 0;

    public void NextPoint() // Переход к следующей точке осмотра объекта    
     {       
         // ВАЖНО понимать, что из-за этой логики телепорт игрока начинается с 1-й точки, а не с 0-й.
         
         if (currentPoint < kElements.Count)
             kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = false; // Выключение предыдущего OUTLINE объекта
 
         currentPoint = (currentPoint + 1) % kElements.Count; // Увеличиваем индекс на 1 либо сбрашиваем до 0, если достигнули последнего объекта
             
         Player.instance.transform.position = kElements[currentPoint].elementObservationPoint.transform.position; // Телепорт к следующей точке
         
         kElements[currentPoint].elementObject.GetComponent<Outlinable>().OutlineParameters.Enabled = true; // Включение аутлайна новой точки
         
     }   
 }
