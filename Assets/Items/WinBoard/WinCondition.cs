using UnityEngine;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public TextMeshPro statusTextObject;
    private string _weaponName = "Cassette";
    private string _victimName = "Cassette";
    private string _murdererName = "Cassette";

    private bool _isWeaponCorrect = false;
    private bool _isVictimCorrect = false;
    private bool _isMurdererCorrect = false;

    public void UpdateWeapon (GameObject snappedObject)
    {
        if (snappedObject == null) return;

        if (snappedObject.name == _weaponName)
        {
            Debug.Log("Weapon is correct!");
            _isWeaponCorrect = true;
            CheckIfWon();
        }
    }

    public void UpdateVictim (GameObject snappedObject)
    {
        if (snappedObject == null) return;

        if (snappedObject.name == _victimName)
        {
            Debug.Log("Victim is correct!");
            _isVictimCorrect = true;
            CheckIfWon();
        }
    }

    public void UpdateMurderer (GameObject snappedObject)
    {
        if (snappedObject == null) return;

        if (snappedObject.name == _murdererName)
        {
            Debug.Log("Murderer is correct!");
            _isMurdererCorrect = true;
            CheckIfWon();
        }
    }

    public void RemoveValue (string value)
    {
        switch (value)
        {
            case "weapon":
                _isWeaponCorrect = false;
                break;
            case "victim":
                _isVictimCorrect = false;
                break;
            case "murderer":
                _isMurdererCorrect = false;
                break;
        }
    }

    private void CheckIfWon()
    {
        if (_isWeaponCorrect && _isVictimCorrect && _isMurdererCorrect)
        {
            statusTextObject.text = "You solved the case!";
        }
    }
}
