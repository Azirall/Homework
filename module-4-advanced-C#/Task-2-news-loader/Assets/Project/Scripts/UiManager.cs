
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
    
    public void Init(NewsLoader newsLoader)
    {
        _newsLoader = newsLoader;
    }

    public async void LoadNews()
    {
        StopNewsCoroutine();
        await TryLoadNews();

        if (_news == null || _news.Count == 0) return;
         
        _currentNewsIndex = 0;
        _newsCoroutine = StartCoroutine(ShowNewsCoroutine());
    }

    private async Task TryLoadNews()
    {
        _loadingAnimation.SetActive(true);
        try
        {
            _news = await _newsLoader.LoadNewsAsync();
            
        }
        catch (NewsLoadException e)
        {
            _tmp.text = e.Message;
        }
        finally
        {
            _loadingAnimation.SetActive(false);
        }
    }

    public async void ShowAllNews()
    {
        StopNewsCoroutine();
        
        await TryLoadNews();
        
        if (_news == null || _news.Count == 0) return;

        int nextNewsIndex = _currentNewsIndex+1 > _news.Count ? 0 : _currentNewsIndex+1;
        AppendFromIndex(nextNewsIndex);
    }

    private void AppendFromIndex(int index)
    {
        for (int i = index; i < _news.Count; i++)
        {
            Append(_news[i]);
            
            _currentNewsIndex++;
        }
        
    }

    private IEnumerator ShowNewsCoroutine()
    {
        
        foreach (var newsItem in _news)
        {
            Append(newsItem);
            _currentNewsIndex++;
            yield return new WaitForSecondsRealtime(_newsLoadingTime);
        }
    }

    private void StopNewsCoroutine()
    {
        if (_newsCoroutine != null)
        {
            StopCoroutine(_newsCoroutine);
            _newsCoroutine = null;
        }
    }
    private void Append(NewsItem item)
    {
        _tmp.text += $"{item.TimeTamp.ToString(_config.DateFormat)} ";
        _tmp.text += $"{item.Title ?? _config.NoTitleText} ";
        _tmp.text += $"{item.Content ?? _config.NoContentText} ";

        for (int i = 0; i < _config.EmptyLineCountAfterItem; i++)
            _tmp.text += "\n";

        _scrollRect.verticalNormalizedPosition = 0;
    }
}
