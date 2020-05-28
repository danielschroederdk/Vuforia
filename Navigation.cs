using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
