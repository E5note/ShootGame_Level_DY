using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// LevelButton.cs
public class LevelButton : MonoBehaviour {
    private int levelIndex;

    public int level;
    public Text levelText;
   
    void Start()
    {
        levelText.text= level.ToString();
        levelIndex = level-1;
    }
    public void OnClick() {
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        SceneManager.LoadScene("GameScenes");
    }
}
