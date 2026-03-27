using System.Collections.Generic;
using UnityEngine;

public interface ISpawnController
{
    void Init(IReadOnlyList<BoxCollider2D> spawnZones, IGameObjectPoolFactory poolFactory, float spawnInterval);
    void Tick();
}
