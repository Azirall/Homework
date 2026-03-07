using System.Collections;
using UnityEngine;

public class WeaponMagazine
{
    private int _currentMagazineSize;
    private readonly int _maxMagazineSize;
    private readonly float _reloadTime;
    private readonly EventBus _eventBus;
    private readonly MonoBehaviour _runner;
    private readonly AmmoChangeEvent _ammoChangeEvent;
    private Coroutine _reloadCoroutine;
    public WeaponMagazine(GunConfig config, EventBus eventBus, MonoBehaviour runner)
    {
        _currentMagazineSize = config.MagazineSize;
        _maxMagazineSize = config.MagazineSize;
        _reloadTime = config.ReloadTime;
        _eventBus = eventBus;
        _runner = runner;
        _ammoChangeEvent = new AmmoChangeEvent(_currentMagazineSize, _maxMagazineSize);
        _eventBus.RaiseGameEvent(_ammoChangeEvent);
    }

    public bool TryConsumeAmmo()
    {
        if(_currentMagazineSize <= 0) return false;
        _currentMagazineSize--;
        _ammoChangeEvent.Set(_currentMagazineSize, _maxMagazineSize);
        _eventBus.RaiseGameEvent(_ammoChangeEvent);
        return true;
    }

    public void Reload()
    {
        if (_currentMagazineSize >= _maxMagazineSize) return; 
        if (_reloadCoroutine != null) return;
        _reloadCoroutine = _runner.StartCoroutine(StartReload());
    }

    private IEnumerator StartReload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _currentMagazineSize = _maxMagazineSize;
        _ammoChangeEvent.Set(_currentMagazineSize, _maxMagazineSize);
        _eventBus.RaiseGameEvent(_ammoChangeEvent);
        _reloadCoroutine = null;
    }
    
}
