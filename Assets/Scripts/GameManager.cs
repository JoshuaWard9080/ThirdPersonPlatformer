using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CoinCollection[] coins;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        coins = FindObjectsByType<CoinCollection>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (CoinCollection coin in coins)
            coin.OnCoinCollection.AddListener(incrementScore);
    }

    private void incrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
