using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // 첫 번째 씬으로 돌아가기
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
