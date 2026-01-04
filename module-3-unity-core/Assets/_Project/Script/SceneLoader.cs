using Script;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SceneLoader : IInitializable
{
    public void Initialize()
    {
        ChangeScene(SceneEnum.Menu);
    }
    public void ChangeScene(SceneEnum sceneEnum)
    {
        switch (sceneEnum)
        {
            case SceneEnum.Menu:
                SceneManager.LoadSceneAsync(sceneBuildIndex: 0);
                break;
            case SceneEnum.Game:
                SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
                break;
        }
    }
    
}
