using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RestartManager : MonoBehaviour
{


    // 重新开始游戏
    public void RestartGame()
    {
        // 获取当前场景的索引并重新加载
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.RestartLevel();
    }


}