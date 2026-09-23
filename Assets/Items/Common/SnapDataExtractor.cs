using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

// Script to extract the root GameObject of a snapped item and send it to a listener
public class SnapDataExtractor : MonoBehaviour
{
    [SerializeField] 
    private SnapInteractable _snapInteractable;

    public UnityEvent<GameObject> OnItemRootSnapped;
    public UnityEvent OnItemRootUnsnapped;

    private void Awake()
    {
        if (_snapInteractable == null)
        {
            _snapInteractable = GetComponent<SnapInteractable>();
        }
    }

    private void OnEnable()
    {
        _snapInteractable.WhenSelectingInteractorAdded.Action += OnItemSnapped;
        _snapInteractable.WhenSelectingInteractorRemoved.Action += OnItemUnsnapped;
    }

    private void OnDisable()
    {
        _snapInteractable.WhenSelectingInteractorAdded.Action -= OnItemSnapped;
        _snapInteractable.WhenSelectingInteractorRemoved.Action -= OnItemUnsnapped;
    }

    private void OnItemSnapped(SnapInteractor interactor)
    {
        if (interactor == null || interactor.transform.parent == null) return;

        // Pass the Root GameObject
        GameObject itemRoot = interactor.transform.parent.gameObject;
        
        Debug.Log($"[EXTRACTOR] Item root '{itemRoot.name}' detected. Sending to listener.");
        OnItemRootSnapped?.Invoke(itemRoot);
    }

    private void OnItemUnsnapped(SnapInteractor interactor)
    {
        OnItemRootUnsnapped?.Invoke();
    }
}
