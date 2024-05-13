using UnityEngine;

public class KeycardSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _keycardPrefab;

    private bool _isUsed;

    public bool IsUsed => _isUsed;

    public void Spawn(KeycardType type) 
    {
        GameObject keycard = Instantiate(_keycardPrefab, transform.position, Quaternion.identity);

        Keycard keycardComponent = keycard.GetComponent<Keycard>();

        keycardComponent.SetType(type);
    }
}
