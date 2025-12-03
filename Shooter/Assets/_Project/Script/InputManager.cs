
using Script;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        this._sceneLoader = sceneLoader;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sceneLoader.ChangeScene(SceneEnum.Menu);
        }
    }
}
