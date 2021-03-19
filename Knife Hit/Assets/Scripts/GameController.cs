using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{

    //Основной контроллер игры. Тут собраны все основные функции и механики.
    public static GameController Instance { get; private set; }
    [SerializeField] private int knifeCount;

    [Header("Knife Spawning")]
    [SerializeField] private Vector2 knifeSpawnPosition;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject Target;
    [SerializeField] private Knife_Behaviour Knife;

    [Header("Target break parts")]
    [SerializeField] private GameObject TargetPart1;
    [SerializeField] private GameObject TargetPart2;
    [SerializeField] private GameObject TargetPart3;

    public SpriteRenderer currColor;

    public GameUI GameUI { get; private set; }
    private void Awake()
    {
        Instance = this;  
        GameUI = GetComponent<GameUI>();
        Target = GameObject.FindGameObjectWithTag("Target");
        Knife = Knife.GetComponent<Knife_Behaviour>();
        knifeCount = Random.Range(6, 10);
    }

    private void Start()
    {
        Vibration.Init();

        GameUI.SetInitialDisplayedKnifeCount(knifeCount);
        SpawnKnife();
    }

    public void OnSuccessfullKnifeHit()
    {
        StartCoroutine(KnifeHitEffectSequence(currColor, Color.white, 0.2f));
        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCorutine", win);
    }

    private IEnumerator GameOverSequenceCorutine(bool win)
    {
        if (win)
        {
            Vibration.Vibrate(400);

            Target.GetComponentInChildren<SpriteRenderer>().enabled = false;
            TextureSwap();

            foreach (EndGameExplosion childScript in Target.GetComponentsInChildren<EndGameExplosion>())
            {
                childScript.enabled = true;
            }
           
            Target.transform.DetachChildren();
            Knife.attachedToWood = false;

            yield return new WaitForSecondsRealtime(1f);
            RestartGame();
            GameManager.Instance.MaxScore += 10;
        }
        else
        {
            foreach (Renderer childImage in Target.GetComponentsInChildren<Renderer>())
            {
                childImage.enabled = false;
            }
            if(GameManager.Instance.MaxScore > GameManager.Instance.TotatlScore)
            {
                GameManager.Instance.TotatlScore = GameManager.Instance.MaxScore;
            }
            GameUI.ShowRestartButton();
        }
    }

    IEnumerator KnifeHitEffectSequence(SpriteRenderer currColor, Color knifeHitColor, float duration)
    {
        Color originColor = currColor.color;
        currColor.color = knifeHitColor;
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            currColor.color = Color.Lerp(knifeHitColor, originColor, t);

            yield return null;
        }
        currColor.color = originColor;
    }

    private void TextureSwap()
    {
        TargetPart1.SetActive(true);
        TargetPart2.SetActive(true);
        TargetPart3.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void EraseMaxScore()
    {
        GameManager.Instance.MaxScore = 0;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        GameManager.Instance.MaxScore = 0;
    }
}
