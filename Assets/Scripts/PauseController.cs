using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;

    private void Update()
    {
        if(Input.GetButtonDown("Cancel")) pausePanel.SetActive(!pausePanel.activeSelf);

        if(pausePanel.activeSelf) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    public void AppQuit() => Application.Quit();
}
