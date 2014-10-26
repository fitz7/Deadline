using UnityEngine;
using System.Collections;

public class UIcontroller : MonoBehaviour
{

    public void Play()
    {
        Application.LoadLevel("Scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
