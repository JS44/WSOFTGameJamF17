using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    string currentLevel = "";
    public static GameManager instance = null;
	public AudioSource restartNoise;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);


        //Call the InitGame function to initialize the first level 
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
			restartNoise.Play ();
            SceneManager.LoadScene(currentLevel);
        }
	}

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        currentLevel = levelName;

    }

	public void startMusic() {
		restartNoise.enabled = true;
		this.GetComponent<AudioSource> ().enabled = true;
	}
}
