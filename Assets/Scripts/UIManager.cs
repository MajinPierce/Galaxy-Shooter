using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Sprite[] livesSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private TextMeshProUGUI restartText;
    // Start is called before the first frame update
    private void Start()
    {
        scoreText.text = "Score: 0";
        gameOverText.enabled = false;
        restartText.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = livesSprites[currentLives];
    }

    public void GameOver()
    {
        restartText.enabled = true;
        gameOverText.enabled = true;
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        var originalText = gameOverText.text;
        gameOverText.text = "";

        var charsRevealed = 0;
        while(charsRevealed < originalText.Length)
        {
            charsRevealed++;
            gameOverText.text = originalText[..charsRevealed];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        while (true)
        {
            gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            gameOverText.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    
}
