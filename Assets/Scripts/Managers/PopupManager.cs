using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupFactory popupFactory;

    private Dictionary<string, Popup> popups = new Dictionary<string, Popup>();

    public static PopupManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void Show(string name)
    {
        if (popups.TryGetValue(name, out Popup popup))
        {
            popup.Show();
        }
        else
        {
            Popup popupInstance = popupFactory.CreatePopup(name);
            
            if (popupInstance != null)
            {
                popups.Add(name, popupInstance);
                popupInstance.Show();
            }
        }
    }

    public void Hide(string name)
    {
        if (popups.TryGetValue(name, out Popup popup))
        {
            popup.Hide();
        }
    }
}