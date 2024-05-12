using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ResultsController : MonoBehaviour
{
    private List<BrokenElementsController.BrokenElement> BrokenElement;

    private float correctAnswers;
    private float accuracyAnswers;
    
    private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    
    private void Start()
    {
        BrokenElement = BrokenElementsController.Instance.brokenElements;
        CheckResults();
    }

    public void CheckResults()
    {
        CreateFile();
        CreateTextFile();
    }

    private void CreateFile()
    {
        if(File.Exists("Лабораторная работа №1.json"))
        {
            File.Delete("Лабораторная работа №1.json");
            
            for (int i = 0; i < BrokenElement.Count(); i++)
            {
                File.AppendAllText("Лабораторная работа №1.json", JsonUtility.ToJson(BrokenElement[i], true));
            }
        }
        else
        {
            for (int i = 0; i < BrokenElement.Count(); i++)
            {
                File.AppendAllText("Лабораторная работа №1.json", JsonUtility.ToJson(BrokenElement[i], true));
            }
        }
    }

    private void CreateTextFile()
    {
        correctAnswers = 0;
        
        for (int i = 0; i < BrokenElement.Count(); i++)
        {
            if (BrokenElement[i].isBroken && BrokenElement[i].isToggle)
            {
                correctAnswers++;
            }
        }

        Debug.Log(correctAnswers);
        Debug.Log(BrokenElementsController.Instance.BrokenElementsNum);
        
        accuracyAnswers = (correctAnswers / BrokenElementsController.Instance.BrokenElementsNum) * 100;
        double answer = Math.Round(accuracyAnswers);
        
        Debug.Log(accuracyAnswers);
        Debug.Log(answer);
        
        if(File.Exists(desktopPath + "/Лабораторная работа №1 Результаты.txt"))
        {
            File.Delete(desktopPath + "/Лабораторная работа №1 Результаты.txt");

            File.WriteAllText(desktopPath + "/Лабораторная работа №1 Результаты.txt", $"Участник верно выбрал: {correctAnswers} / {BrokenElementsController.Instance.BrokenElementsNum} неисправных объектов. Процент выполнения работы: {answer}%" );
        }
        else
        {
            File.WriteAllText(desktopPath+ "/Лабораторная работа №1 Результаты.txt", $"Участник верно выбрал: {correctAnswers} / {BrokenElementsController.Instance.BrokenElementsNum} неисправных объектов. Процент выполнения работы: {answer}%" );
        }
    }
}
