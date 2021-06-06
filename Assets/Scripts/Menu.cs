using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : Entity
{
    [SerializeField]
    GameObject options;
    [SerializeField]
    GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene(1);

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {

        options.SetActive(true);
        mainMenu.SetActive(false);
        
    }
    public void Back()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
}
