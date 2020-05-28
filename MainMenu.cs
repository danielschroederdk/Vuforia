using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  public GameObject numberOfGamesInput;
  public void PlayGame()
  {
    PlayerPrefs.SetInt("numberOfGames", numberOfGamesInput.GetComponent<InputField>().text.Length > 0 ? int.Parse(numberOfGamesInput.GetComponent<InputField>().text) : 5);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void QuitGame()
  {
    Debug.Log("QUIT");
    Application.Quit();
  }
}
 