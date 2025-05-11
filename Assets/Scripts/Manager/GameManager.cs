using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalBullets = 10;

    private int currentBullets;
    public Text bulletsText;
    public GameObject gameOverPanel;

    public Text resultText;

    private int enemiesRemaining;

    public Button NextLevelButton;

    private GameObject currentLevel;

    [Header("Level System")]
    public GameObject[] levelPrefabs;



    private int currentLevelIndex = 0;

    void Start()
    {
        Instance = this;
        currentBullets = totalBullets;
                
        // 初始化关卡预设
        InitLevelPrefabs();
        
        LoadSelectedLevel();
        NextLevelButton.onClick.AddListener(NextLevel);

    }


    public void LoadSelectedLevel()
    {
        // int selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 0);
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);
        InstantiateLevel(currentLevelIndex);
        InitializeGame();
    }

    void InitializeGame()
    {
        // 初始化子弹和敌人数量
        ResetBullet();
        // enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesRemaining = currentLevel.GetComponentsInChildren<Enemy>().Length;
        Debug.Log("enemy:" + enemiesRemaining);
        gameOverPanel.SetActive(false);
    }
    
    // 加载关卡
    void InstantiateLevel(int levelNumber)
    {
        // 销毁旧关卡
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (levelNumber <= levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[levelNumber - 1]);
        }
        else
        {
            Debug.LogError("Level prefab not found!");
        }
    }

    // 通关时调用
    public void CompleteLevel()
    {
        LevelManager.UnlockNextLevel(currentLevelIndex);
    }

    public void UseBullet()
    {
        currentBullets--;
        UpdateUI();
        CheckGameOver();
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        CheckGameOver();
    }

    public bool HasBullets()
    {
        return currentBullets > 0;
    }

    // 供GameManager调用的重置方法
    public void ResetBullet()
    {
        currentBullets = totalBullets;
        UpdateUI(); // 更新UI显示
    }
    void UpdateUI()
    {
        bulletsText.text = $"Bullets: {currentBullets}";
    }

    void CheckGameOver()
    {
        // 当敌人全部死亡时
        if (enemiesRemaining <= 0)
        {
            GameManager.Instance.EndGame(true); // 胜利
            CompleteLevel();
        }

        // 当子弹用尽时
        if (currentBullets <= 0 && enemiesRemaining > 0)
        {
            GameManager.Instance.EndGame(false); // 失败
        }
    }

    public void EndGame(bool isWin)
    {
        // 显示面板
        gameOverPanel.SetActive(true);

        // 设置结果文本
        resultText.text = isWin ? "You Win!" : "Game Over!";

        if (isWin == true)
        {
            NextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            NextLevelButton.gameObject.SetActive(false);
        }

    }

    public void NextLevel()
    {
        currentLevelIndex++;
        InstantiateLevel(currentLevelIndex);
        InitializeGame();
    }

    public void RestartLevel()
    {
        InstantiateLevel(currentLevelIndex);
        InitializeGame();
    }

    private void InitLevelPrefabs()
    {
        levelPrefabs = Resources.LoadAll<GameObject>("Levels");
    }

}
