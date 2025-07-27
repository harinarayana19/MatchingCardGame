using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public GameObject cardPrefab;
//    public Transform cardParent;
//    public List<Sprite> cardSprites; 
//    public AudioSource audioSource;
//    public AudioClip flipSound, matchSound;

//    public bool IsLocked = false;
//    [Header("Winner Content")]
//    public TMP_Text winnerText;      
//    public TMP_Text timerText;
//    public GameObject magicEffect;

//    [Header("Gridsize Content")]
//    public GridLayoutGroup gridLayout;
//    public RectTransform panelRect;



//    private float timer = 0f;
//    private bool gameWon = false;

//    private Card firstCard, secondCard;
//    private int matchesFound = 0;
//    private int totalPairs;

//    private void Awake()
//    {
//        Instance = this;
//        totalPairs = cardSprites.Count;
//    }
//   private void Update()
//    {
//        if (!gameWon)
//        {
//            timer += Time.deltaTime;
//            timerText.text = $"Time: {timer:F1}s";
//        }
//    }

//    void Start()
//    {
//        GenerateGrid(totalPairs);

//    }
//    public void AdjustPanelSize(int totalCards, int columns, float leftMargin)
//    {
//        int rows = Mathf.CeilToInt((float)totalCards / columns);

//        // Calculate panel size
//        float width = (gridLayout.cellSize.x * columns) +
//                      (gridLayout.spacing.x * (columns - 1)) +
//                      gridLayout.padding.left +
//                      gridLayout.padding.right;

//        float height = (gridLayout.cellSize.y * rows) +
//                       (gridLayout.spacing.y * (rows - 1)) +
//                       gridLayout.padding.top +
//                       gridLayout.padding.bottom;

//        panelRect.sizeDelta = new Vector2(width, height);
//        SetMargins(panelRect, 0, 0, 0, 0);


//    }
//    public void SetMargins(RectTransform rect, float left, float top, float right, float bottom)
//    {
//        Vector2 offsetMin = rect.offsetMin; 
//        Vector2 offsetMax = rect.offsetMax; 

//        offsetMin.x = left;       
//        offsetMin.y = bottom;     
//        offsetMax.x = -right;     
//        offsetMax.y = -top;       

//        rect.offsetMin = offsetMin;
//        rect.offsetMax = offsetMax;
//    }
//    public void GenerateGrid(int totalPairs)
//    {
//        List<int> cardIds = new List<int>();


//        for (int i = 0; i < totalPairs; i++)
//        {
//            cardIds.Add(i);
//            cardIds.Add(i);
//        }


//        for (int i = 0; i < cardIds.Count; i++)
//        {
//            int rand = Random.Range(i, cardIds.Count);
//            int temp = cardIds[i];
//            cardIds[i] = cardIds[rand];
//            cardIds[rand] = temp;
//        }


//        for (int i = 0; i < cardIds.Count; i++)
//        {
//            GameObject newCard = Instantiate(cardPrefab, cardParent);
//            Card card = newCard.GetComponent<Card>();
//            card.InitializeCard(cardSprites[cardIds[i]], cardIds[i]);
//        }
//        GameObject.Find("CardButton").SetActive(false);

//        int totalCards = totalPairs*2;
//        int columns = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
//        Debug.Log(totalCards);
//        AdjustPanelSize(totalCards, columns, leftMargin: 50f);
//        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//        gridLayout.constraintCount = columns;
//    }

//    public void CardSelected(Card card)
//    {
//        if (IsLocked || card == firstCard) return;

//        if (firstCard == null)
//        {
//            firstCard = card;
//        }
//        else
//        {
//            secondCard = card;
//            StartCoroutine(CheckMatch());
//        }
//    }

//    private IEnumerator CheckMatch()
//    {
//        IsLocked = true;
//       // yield return new WaitForSeconds(0.7f);

//        if (firstCard.cardId == secondCard.cardId)
//        {
//            matchesFound++;
//            PlayMatchSound();


//            firstCard.DisableCard();
//            secondCard.DisableCard();

//            if (matchesFound == totalPairs)
//            {
//                Debug.Log(" You Win!");
//                gameWon = true;
//                winnerText.text = $" You Win!\nTime: {timer:F1}s";
//                if (magicEffect != null)
//                {
//                    magicEffect.SetActive(true);
//                    ParticleSystem ps = magicEffect.GetComponent<ParticleSystem>();
//                    if (ps != null) ps.Play();
//                }
//            }
//            else
//            {
//                yield return new WaitForSeconds(0.5f);
//                firstCard.FlipBack();
//                secondCard.FlipBack();
//            }

//            firstCard = null;
//            secondCard = null;
//            IsLocked = false;
//        }
//    }

//    public void PlayFlipSound()
//    {
//        if (flipSound != null) audioSource.PlayOneShot(flipSound);
//    }

//    public void PlayMatchSound()
//    {
//        if (matchSound != null) audioSource.PlayOneShot(matchSound);
//    }




//}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public TextMeshProUGUI winMessage;
    public GameObject magicEffect;

    [Header("Card Settings")]
    public GameObject cardPrefab;
    public Transform cardParent;
    public List<Sprite> cardSprites;

    [Header("Gridsize Content")]
    public GridLayoutGroup gridLayout;
    public RectTransform panelRect;

    public bool IsLocked = false;
    private List<Card> selectedCards = new List<Card>();
    private float timer = 0f;
    private bool gameActive = false;
    private int matchedPairs = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateGrid();
        gameActive = true;
    }

    private void Update()
    {
        if (gameActive)
        {
            timer += Time.deltaTime;
            timerText.text = $"Time: {timer:F1}s";
        }
    }
    public void AdjustPanelSize(int totalCards, int columns, float leftMargin)
    {
        int rows = Mathf.CeilToInt((float)totalCards / columns);

        // Calculate panel size
        float width = (gridLayout.cellSize.x * columns) +
                      (gridLayout.spacing.x * (columns - 1)) +
                      gridLayout.padding.left +
                      gridLayout.padding.right;

        float height = (gridLayout.cellSize.y * rows) +
                       (gridLayout.spacing.y * (rows - 1)) +
                       gridLayout.padding.top +
                       gridLayout.padding.bottom;

        panelRect.sizeDelta = new Vector2(width, height);
        SetMargins(panelRect, 0, 0, 0, 0);


    }
    public void SetMargins(RectTransform rect, float left, float top, float right, float bottom)
    {
        Vector2 offsetMin = rect.offsetMin;
        Vector2 offsetMax = rect.offsetMax;

        offsetMin.x = left;
        offsetMin.y = bottom;
        offsetMax.x = -right;
        offsetMax.y = -top;

        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }
    void GenerateGrid()
    {
        List<int> ids = new List<int>();

        for (int i = 0; i < cardSprites.Count; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);

        foreach (int id in ids)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            Card card = newCard.GetComponent<Card>();
            card.InitializeCard(cardSprites[id], id);
        }
        GameObject.Find("CardButton").SetActive(false);

        int totalCards = cardSprites.Count * 2;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
        Debug.Log(totalCards);
        AdjustPanelSize(totalCards, columns, leftMargin: 50f);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CardSelected(Card card)
    {
        if (selectedCards.Contains(card)) return;

        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        IsLocked = true;
        yield return new WaitForSeconds(0.5f);

        if (selectedCards[0].cardId == selectedCards[1].cardId)
        {
            selectedCards[0].DisableCard();
            selectedCards[1].DisableCard();
            matchedPairs++;

            if (matchedPairs == cardSprites.Count)
            {
                WinGame();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            selectedCards[0].FlipBack();
            selectedCards[1].FlipBack();
        }

        selectedCards.Clear();
        IsLocked = false;
    }

    void WinGame()
    {
        gameActive = false;
        winPanel.SetActive(true);
        winMessage.text = $" You Win!\nTime: {timer:F1}s";
        Debug.Log(" You Win!");
       
        if (magicEffect != null)
        {
            magicEffect.SetActive(true);
            ParticleSystem ps = magicEffect.GetComponent<ParticleSystem>();
            if (ps != null) ps.Play();
        }

        // Optional: Play particle effect
        // Instantiate(winEffect, Vector3.zero, Quaternion.identity);
    }
}