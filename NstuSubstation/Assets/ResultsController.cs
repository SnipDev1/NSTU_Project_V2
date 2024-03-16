using UnityEngine;
using System.IO;
using System.Linq;

public class ResultsController : MonoBehaviour
{
    public static ResultsController Instance { get; private set; }

    public void CheckResults()
    {
        for (int i = 0; i < BrokenElementsController.Instance.brokenElements.Count; i++)
        {
            if (BrokenElementsController.Instance.brokenElements[i].isBroken &&
                BrokenElementsController.Instance.brokenElements[i].isToggle)
            {
                Debug.Log("АХАХАХАХ");
            }
            else
            {
                Debug.Log("ХУЙ САСИ");
                CreateFile();
            }
        }
    }

    private void CreateFile()
    {
        string path = Application.dataPath + "/Results.txt";
        string[] content = new string[BrokenElementsController.Instance.brokenElements.Count];
        

        for (int i = 0; i < content.Length; i++)
        {   
            content[i] = "Я хуесос";

            
        }
    }
}
