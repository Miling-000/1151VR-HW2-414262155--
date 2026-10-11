
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;

    private bool ended = false;

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ended || !other.CompareTag("Player"))
            return;

        if (gameOverText == null)
        {
            Debug.LogError("請在 Inspector 指定 Game Over Text！");
            return;
        }

        ended = true;
        gameOverText.SetActive(true);
        Time.timeScale = 0f;
    }
}
