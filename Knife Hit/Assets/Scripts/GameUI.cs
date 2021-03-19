using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [SerializeField] private GameObject endGameScreen;

    [Header("Player progress")]
    [SerializeField] private Text totalOranges;
    [SerializeField] private Text totalScore;
    [SerializeField] private Text maxScore;

    [SerializeField] private GameObject pannelKnives;
    [SerializeField] private GameObject iconKnife;
    [SerializeField] private Color usedKnifeIconColor;
    [SerializeField] private int knifeIconIndexToChange = 0;
    public void ShowRestartButton()
    {
        endGameScreen.SetActive(true);
    }

    public void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(iconKnife, pannelKnives.transform);
    }

    
    public void DecrementDisplayedKnifeCount()
    {
        pannelKnives.transform.GetChild(knifeIconIndexToChange++)
            .GetComponent<Image>().color = usedKnifeIconColor;
    }
    private void Update()
    {
        totalOranges.text = GameManager.Instance.TotatlOranges.ToString();
        totalScore.text = GameManager.Instance.TotatlScore.ToString();
        maxScore.text = GameManager.Instance.MaxScore.ToString();
    }
}
