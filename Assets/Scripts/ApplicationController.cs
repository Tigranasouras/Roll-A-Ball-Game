using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
    public void quitApp()
    {
           Application.Quit();
           Debug.Log("Application qutting...");
    }
}
