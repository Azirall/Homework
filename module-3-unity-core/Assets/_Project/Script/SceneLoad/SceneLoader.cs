using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using IInitializable = Unity.VisualScripting.IInitializable;

public class SceneLoader : IInitializable
{
    private SaveSystem _saveSystem;

    [Inject]
    private void Construct(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        ChangeScene(SceneEnum.Menu);
    }
    public void ChangeScene(SceneEnum sceneEnum, TargetStats stats = null)
    {
        switch (sceneEnum)
        {
            case SceneEnum.Menu:
                SceneManager.LoadSceneAsync(sceneBuildIndex: 0);
                _saveSystem.Save(stats);
                break;
            case SceneEnum.Game:
                SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
                break;
            case SceneEnum.Quit:
                Application.Quit();
                _saveSystem.Save(stats);
                break;
        }
    }
    
}
