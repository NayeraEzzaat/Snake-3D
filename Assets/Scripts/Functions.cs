using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Functions : MonoBehaviour {

    public void ReloadLevel() {   
        SceneManager.LoadScene("Level 01");
    }
    public void ExitLevel() {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
