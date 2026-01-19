using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour, IEventManagerConsumer
{
    [SerializeField] private List<GameObject> _enemyList;
    private EventManager _eventManager;
    
    public void Initialize(EventManager eventManager)
    {
        _eventManager = eventManager;
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
        _eventManager.OnGameEvent -= ChangeEnemyState;
    }
}
