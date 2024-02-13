using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuTweenAnimator : MonoBehaviour
{
    [SerializeField] public RectTransform title;
    [SerializeField] public RectTransform playButton;
    [SerializeField] public RectTransform quitButton;
    [SerializeField] float duration;

    bool isCompleted;

    void Start()
    {
        title.transform.localPosition = new Vector2(0f, 400f);
        playButton.transform.localPosition = new Vector2(0f, 400f);
        quitButton.transform.localPosition = new Vector2(0f, 400f);

        playButton.GetComponent<Button>().enabled = false;
        quitButton.GetComponent<Button>().enabled = false;

        StartCoroutine(MMTweenAnim());

    }

    private void Update()
    {
        if (isCompleted)
        {
            Debug.Log(isCompleted);
        }
    }

    IEnumerator MMTweenAnim()
    {
        title.DOAnchorPos(new Vector2(0f, 0f), duration, false)
            .SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1f);

        playButton.DOAnchorPos(new Vector2(0f, -100f), duration, false)
            .SetEase(Ease.OutBounce)
            .OnComplete(ActivatePlayButton);

        yield return new WaitForSeconds(1f);

        quitButton.DOAnchorPos(new Vector2(0f, -175f), duration, false)
            .SetEase(Ease.OutBounce)
            .OnComplete(ActivateQuitButton)
            .OnComplete(AfterTween);
    }

    public void ActivatePlayButton()
    {
        playButton.GetComponent<Button>().enabled = true;
    }

    public void ActivateQuitButton()
    {
        quitButton.GetComponent<Button>().enabled = true;
    }

    public void AfterTween()
    {
        isCompleted = true;
    }

}
