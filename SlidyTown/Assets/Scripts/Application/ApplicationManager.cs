using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : SingletonBehaviour<ApplicationManager> {

    
    protected override void Awake() {
        StartCoroutine(LoadGameTask());
    }

    public void Restart() {
        StartCoroutine(RestartTask());
    }

    private IEnumerator RestartTask() {
        UIManager.SetCurrentPanel("LoadingPanel");
        LogoWidget.Instance.Show();
        yield return new WaitUntil(() => UIManager.CurrentPanel != null && UIManager.CurrentPanel.name == "LoadingPanel" && LogoWidget.Instance.FullyVisible);
        SceneManager.UnloadSceneAsync(GameManager.Instance.gameObject.scene);
        yield return LoadGameTask();
    }

    private IEnumerator LoadGameTask() {
        var loadProcess = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        yield return loadProcess;
        yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        yield return new WaitUntil(() => LogoWidget.Instance.FullyVisible);
        UIManager.SetCurrentPanel("StartPanel");
        CameraArm.Instance.SnapToTarget();
    }
}
