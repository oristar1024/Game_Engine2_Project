using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public void Retry()
    {
        GameManager.instance.Retry();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
