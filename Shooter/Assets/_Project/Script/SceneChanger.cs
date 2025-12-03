using Script;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SceneChanger : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private SceneEnum ChangeSceneTo;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        _sceneLoader.ChangeScene(ChangeSceneTo);
    }
}
