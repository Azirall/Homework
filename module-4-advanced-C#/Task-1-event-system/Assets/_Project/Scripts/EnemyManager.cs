using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameServicesConsumer
{
    [SerializeField] private List<GameObject> _enemyList;
    private EventManager _eventManager;

    public void Initialize(GameServices services)
    {
        _eventManager = services.EventManager;
        _eventManager.OnGameEvent += ChangeEnemyState;
    }

    private void ChangeEnemyState(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.OnEnemyActiveStateChanged)
        {
            _enemyList.ForEach(go => go.SetActive(!go.activeSelf));
        }
    }

    private void OnDestroy()
    {
        if (_eventManager != null)
        {
            _eventManager.OnGameEvent -= ChangeEnemyState;
        }
    }
}
