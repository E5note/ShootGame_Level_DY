// LevelManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject levelButtonPrefab;
    public Transform buttonsParent;
    public int totalLevels = 5;

    [Header("清除缓存配置")]
    public Button clearCacheButton;

    [Header("返回首页配置")]
    public Button ReturnToStartButton;

    void Start()
    {
        GenerateLevelButtons();
        clearCacheButton.onClick.AddListener(ClearPlayerPrefs);
        ReturnToStartButton.onClick.AddListener(ReturnStartGame);
    }

    //添加关卡按钮
    void GenerateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject button = Instantiate(levelButtonPrefab, buttonsParent);
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = i.ToString();

            bool isUnlocked = PlayerPrefs.GetInt("Level" + i, 0) == 1;
            if (i == 1) isUnlocked = true; // 第一关默认解锁

            button.GetComponent<Button>().interactable = isUnlocked;
            button.transform.Find("Lock").gameObject.SetActive(!isUnlocked);

            int levelIndex = i;
            button.GetComponent<Button>().onClick.AddListener(() => LoadGameScene(levelIndex));
        }
    }
    public void ReturnStartGame()
    {
        SceneManager.LoadScene("StartScenes");
    }

    public void ClearPlayerPrefs()
    {
        // // 删除所有关卡解锁状态
        // for (int i = 1; i <= totalLevels; i++)
        // {
        //     PlayerPrefs.DeleteKey("Level" + i + "Unlocked");
        // }

        // // 重新初始化第一关
        // PlayerPrefs.SetInt("Level1Unlocked", 1);
        // PlayerPrefs.Save(); // 强制保存

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnlockedLevel", 1); // 默认解锁第一关
        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        // 刷新UI
        RefreshLevelButtons();

        Debug.Log("进度已重置");
    }

    void RefreshLevelButtons()
    {
        // 销毁旧按钮
        foreach (Transform child in buttonsParent)
        {
            Destroy(child.gameObject);
        }
        // if (currentLevel != null)
        // {
        //     Destroy(currentLevel);
        //     LoadLevel(PlayerPrefs.GetInt("CurrentLevel", 1));
        // }
        // 重新生成
        GenerateLevelButtons();
    }

    public static void UnlockNextLevel(int currentLevel)
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel <= PlayerPrefs.GetInt("TotalLevels", 5))
        {
            PlayerPrefs.SetInt("Level" + nextLevel, 1);
        }
    }

    void LoadGameScene(int levelNumber)
    {
        Debug.Log("关卡：" + levelNumber);
        PlayerPrefs.SetInt("CurrentLevel", levelNumber);
        SceneManager.LoadScene("GameScenes");
    }

}