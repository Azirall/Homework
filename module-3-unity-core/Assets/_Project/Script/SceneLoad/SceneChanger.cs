using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SceneChanger : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private SceneEnum _changeSceneTo;
    private SceneLoader _sceneLoader;
    private TargetStats _targetStats;

    [Inject]
    public void Construct(SceneLoader sceneLoader, [InjectOptional] TargetStats targetStats = null)
    {
        _sceneLoader = sceneLoader;
        _targetStats = targetStats;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        _sceneLoader.ChangeScene(_changeSceneTo, _targetStats);
    }
}
