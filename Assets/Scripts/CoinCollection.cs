using UnityEngine;
using UnityEngine.Events;

public class CoinCollection : MonoBehaviour
{

    public UnityEvent OnCoinCollection = new();

    private void OnTriggerEnter(Collider triggeredObject)
    {

        OnCoinCollection?.Invoke();
        Destroy(this.gameObject);
    }
}
