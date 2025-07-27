using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

//public class Card : MonoBehaviour
//{
//    public int cardId;
//    public Image frontImage;
//    public GameObject front;
//    public GameObject back;

//    private bool isFlipped = false;
//    private bool isAnimating = false;


//    public void InitializeCard(Sprite sprite, int id)
//    {
//        cardId = id;
//        frontImage.sprite = sprite;


//        front.SetActive(false);
//        back.SetActive(true);
//        isFlipped = false;


//        frontImage.enabled = false;
//        frontImage.enabled = true;


//    }
//    public void OnCardClicked()
//    {
//        if (isFlipped || GameManager.Instance.IsLocked || isAnimating) return;

//        GameManager.Instance.PlayFlipSound();
//        StartCoroutine(FlipAnimation());
//        GameManager.Instance.CardSelected(this);
//        Canvas.ForceUpdateCanvases();
//        LayoutRebuilder.ForceRebuildLayoutImmediate(frontImage.rectTransform);
//    }

//    public IEnumerator FlipAnimation()
//    {
//        isAnimating = true;

//        float duration = 0.125f;
//        float time = 0f;
//        Vector3 startRotation = transform.localEulerAngles;
//        Vector3 midRotation = new Vector3(0, 90, 0);
//        Vector3 endRotation = new Vector3(0, 0, 0);


//        while (time < duration)
//        {
//            transform.localEulerAngles = Vector3.Lerp(startRotation, midRotation, time / duration);
//            time += Time.deltaTime;
//            yield return null;
//        }

//        transform.localEulerAngles = midRotation;
//        isFlipped = !isFlipped;
//        front.SetActive(isFlipped);
//        back.SetActive(!isFlipped);


//        time = 0f;
//        while (time < duration)
//        {
//            transform.localEulerAngles = Vector3.Lerp(midRotation, endRotation, time / duration);
//            time += Time.deltaTime;
//            yield return null;
//        }

//        transform.localEulerAngles = endRotation;
//        isAnimating = false;
//    }

//    public void FlipBack()
//    {
//        if (isFlipped)
//            StartCoroutine(FlipAnimation());
//    }


//    public void DisableCard()
//    {
//        GetComponent<Button>().interactable = false;
//    }
//}
public class Card : MonoBehaviour
{
    public int cardId;
    public Image frontImage;
    public GameObject front;
    public GameObject back;

    private bool isFlipped = false;
    private bool isAnimating = false;

    public void InitializeCard(Sprite sprite, int id)
    {
        cardId = id;
        frontImage.sprite = sprite;
        front.SetActive(false);
        back.SetActive(true);
        isFlipped = false;
    }

    public void OnCardClicked()
    {
        if (isFlipped || GameManager.Instance.IsLocked || isAnimating) return;

        StartCoroutine(FlipAnimation());
        GameManager.Instance.CardSelected(this);
    }

    private IEnumerator FlipAnimation()
    {
        isAnimating = true;

        float duration = 0.125f;
        float time = 0f;
        Vector3 startRotation = transform.localEulerAngles;
        Vector3 midRotation = new Vector3(0, 90, 0);
        Vector3 endRotation = new Vector3(0, 0, 0);

        while (time < duration)
        {
            transform.localEulerAngles = Vector3.Lerp(startRotation, midRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = midRotation;
        isFlipped = !isFlipped;
        front.SetActive(isFlipped);
        back.SetActive(!isFlipped);

        time = 0f;
        while (time < duration)
        {
            transform.localEulerAngles = Vector3.Lerp(midRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = endRotation;
        isAnimating = false;
    }

    public void FlipBack()
    {
        if (isFlipped)
            StartCoroutine(FlipAnimation());
    }

    public void DisableCard()
    {
        GetComponent<Button>().interactable = false;
    }
}

