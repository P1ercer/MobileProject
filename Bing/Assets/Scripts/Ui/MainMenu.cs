using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public string loadToLevel;
    public string levelToLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LevelOne()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene(loadToLevel);
    }
    public void LevelThree()
    {
        SceneManager.LoadScene(levelToLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
