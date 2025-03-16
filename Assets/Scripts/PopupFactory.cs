using System.Collections.Generic;
using UnityEngine;

public class PopupFactory : MonoBehaviour
{
    [SerializeField] private RectTransform parent;
    [SerializeField] private List<PopupDefinition> popupDefinitions;

    private Dictionary<string, Popup> popupDictionary;

    private void Awake()
    {
        popupDictionary = new Dictionary<string, Popup>();

        foreach (var definition in popupDefinitions)
        {
            if (!popupDictionary.ContainsKey(definition.popupName))
            {
                popupDictionary.Add(definition.popupName, definition.prefab);
            }
        }
    }

    public Popup CreatePopup(string popupName)
    {
        if (popupDictionary.TryGetValue(popupName, out Popup popupPrefab))
        {
            return Instantiate(popupPrefab, parent);
        }

        Debug.LogError($"Popup prefab '{popupName}' not found!");
        return null;
    }
}