using UnityEngine;

[RequireComponent(typeof(SceneContext))]
[RequireComponent(typeof(UiContext))]
[RequireComponent(typeof(GunContext))]

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private UiContext _uiContext;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private GunContext _gunContext;

    private GameCompositionRoot _compositionRoot;

    private void Awake()
    {
        if (_sceneContext == null || _uiContext == null || _gameConfig == null || _gunContext == null)
        {
            Debug.LogError("Bootstrap contexts/config are not set");
            return;
        }

        _compositionRoot = new GameCompositionRoot(_sceneContext, _uiContext, _gameConfig, _gunContext, this);
        _compositionRoot.Init();
    }

    private void OnDestroy()
    {
        _compositionRoot?.Dispose();
        _compositionRoot = null;
    }

    private void OnValidate()
    {
        if (_sceneContext == null)
        {
            Debug.LogError("SceneContext in Bootstrap is null");
        }

        if (_gameConfig == null)
        {
            Debug.LogError("GameConfig in Bootstrap is null");
        }

        if (_gunContext == null)
        {
            Debug.LogError("Gun context in Bootstrap is null");
        }

        if (_uiContext == null)
        {
            Debug.LogError("UiContext in Bootstrap is null");
        }
    }
}
