using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private RectTransform parent;
    [SerializeField] private List<MyDictionaryEntry> popupPrefabs;

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
            Popup popupInstance = GetNewPopup(name);
            popups.Add(name, popupInstance);
            popupInstance.Show();
        }
    }

    public void Hide(string name)
    {
        if (popups.TryGetValue(name, out Popup popup))
        {
            popup.Hide();
        }
    }

    private Popup GetNewPopup(string name)
    {
        Popup popupPrefab = null;

        foreach (var entry in popupPrefabs)
        {
            if (entry.key == name)
            {
                popupPrefab = entry.value;
            }
        }

        return Instantiate(popupPrefab, parent);
    }
}

[System.Serializable]
public class MyDictionaryEntry
{
    public string key;
    public Popup value;
}