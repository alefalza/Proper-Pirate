using UnityEngine;

[CreateAssetMenu(fileName = "NewPopup", menuName = "UI/Popup Definition")]
public class PopupDefinition : ScriptableObject
{
    public string popupName;
    public Popup prefab;
}