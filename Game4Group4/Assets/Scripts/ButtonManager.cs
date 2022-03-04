using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetButton()
    {
        Manager.ResetButton();
    }

    public void ConfirmButton()
    {
        Manager.ConfirmButton();
        SceneManager.LoadScene("Julien Scene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void MenuButton()
    {
        Manager.paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void AddMoneyButton()
    {
        Manager.AddMoney(10);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
