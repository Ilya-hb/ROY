using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : Entity
{
    // Start is called before the first frame update


   public void Restart()
    {
        SceneManager.LoadScene("SampleScene"); //RELOAD SCENE
        Time.timeScale = 1;
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
