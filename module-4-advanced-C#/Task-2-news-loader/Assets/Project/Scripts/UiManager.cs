using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private float _newsLoadingTime = 2f;
    [SerializeField] private GameObject _loadingAnimation;
    [SerializeField] private NewsTextViewConfig _config;
    [SerializeField] private ScrollRect _scrollRect;

    private List<NewsItem> _news = new();
    private NewsLoader _newsLoader;
    private Coroutine _newsCoroutine;
    private int _currentNewsIndex;

    public void Init(NewsLoader newsLoader) => _newsLoader = newsLoader;

    public async void LoadNews()
    {
        StopNewsCoroutine();

        _tmp.text = string.Empty;
        _currentNewsIndex = 0;

        await TryLoadNews();
        if (_news == null || _news.Count == 0) return;

        _newsCoroutine = StartCoroutine(ShowNewsCoroutine());
    }
    public void ShowAllNews()
    {
        StopNewsCoroutine();
        if (_news == null || _news.Count == 0) return;

        AppendFromIndex(_currentNewsIndex);
    }

    private async Task TryLoadNews()
    {
        try
        {
            _loadingAnimation.SetActive(true);
            _news = await _newsLoader.LoadNewsAsync() ?? new List<NewsItem>();
            _loadingAnimation.SetActive(false);
        }
        catch (NewsLoadException e)
        {
            _news = new List<NewsItem>();
            _tmp.text = e.Message;
        }

    }

    private IEnumerator ShowNewsCoroutine()
    {
        while (_news != null && _currentNewsIndex < _news.Count)
        {
            Append(_news[_currentNewsIndex]);
            _currentNewsIndex++;

            yield return new WaitForSecondsRealtime(_newsLoadingTime);
        }

        _newsCoroutine = null;
    }

    private void AppendFromIndex(int index)
    {
        if (_news == null || _news.Count == 0) return;

        if (index < 0) index = 0;
        if (index > _news.Count) index = _news.Count;

        for (int i = index; i < _news.Count; i++)
        {
            Append(_news[i]);
        }

        _currentNewsIndex = _news.Count;
        _newsCoroutine = null;
    }

    private void StopNewsCoroutine()
    {
        if (_newsCoroutine == null) return;

        StopCoroutine(_newsCoroutine);
        _newsCoroutine = null;
    }

    private void Append(NewsItem item)
    {
        _tmp.text += $"{item.TimeTamp.ToString(_config.DateFormat)} ";
        _tmp.text += $"{item.Title ?? _config.NoTitleText} ";
        _tmp.text += $"{item.Content ?? _config.NoContentText} ";

        for (int i = 0; i < _config.EmptyLineCountAfterItem; i++)
            _tmp.text += "\n";

        _scrollRect.verticalNormalizedPosition = 0f;
    }
}

