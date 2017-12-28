using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public string levelName;

    public void LoadLevel()
    {
        GameManager.instance.LoadLevel(levelName);
    }
}
