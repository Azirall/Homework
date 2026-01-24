
using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
     [SerializeField] private UiManager _uiManager;
     private NewsLoader _newsLoader;

     private void Awake()
     {
          _newsLoader = new NewsLoader();
          _uiManager.Init(_newsLoader);
     }
     
}
