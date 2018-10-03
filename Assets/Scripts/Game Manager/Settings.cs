using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    [HideInInspector]
    public bool _isPaused;
    public GameObject settings;
    [HideInInspector]
    public bool isDead;

	void Start () {
        _isPaused = false;
        Cursor.visible = false;
        settings.SetActive(false);
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!_isPaused) {
                GamePaused();
            }
            else {
                GameUnpaused();
            }
        }

    }

    public void GamePaused() {
        _isPaused = true;
        Cursor.visible = true;
        settings.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameUnpaused() {
        _isPaused = false;
        Cursor.visible = false;
        settings.SetActive(false);
        if (!isDead) {
            Time.timeScale = 1;
        }
    }
}
