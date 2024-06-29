using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    // CHILD COMPONENTS
    private Animator titleCardAnimator;
    private Image startButtonImage;
    private Image transitionScreen;

    // BUTTON FADE
    private AnimatorStateInfo animatorStateInfo;
    private bool isAnimationFinished = false;
    private float normAnimTime; // normalised title card animation time
    private bool triggeredFade = false;

    // ENABLE/DISABLE UI
    private bool isActiveUI = false;

    // CAMERA MOVEMENT
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private float transitionTime = 1.5f;



    private void Start()
    {
        titleCardAnimator = transform.GetChild(0).GetComponent<Animator>();

        startButtonImage = transform.GetChild(1).GetComponent<Image>();
        startButtonImage.color = new Color(1, 1, 1, 0);

        transitionScreen = transform.GetChild(2).GetComponent<Image>();
        transitionScreen.color = new Color(1, 1, 1, 0);
        transitionScreen.enabled = false;
    }

    private void Update()
    {
        if(!isAnimationFinished) CheckTitleAnim();
        else if(!triggeredFade) StartCoroutine("FadeInButton");
    }

    private void CheckTitleAnim()
    {
        animatorStateInfo = titleCardAnimator.GetCurrentAnimatorStateInfo(0);
        normAnimTime = animatorStateInfo.normalizedTime;

        if(normAnimTime > 1.0f) isAnimationFinished = true;
    }

    private IEnumerator FadeInButton()
    {
        triggeredFade = true;

        for (float a = 0; a <= 1; a += Time.deltaTime)
        {
            startButtonImage.color = new Color(1, 1, 1, a);
            yield return null;
        }

        isActiveUI = true;
    }

    public void StartGame()
    {
        if(isActiveUI) StartCoroutine("GameTransition");
    }

    private IEnumerator GameTransition()
    {
        transitionScreen.enabled = true;
        cameraAnimator.SetTrigger("startGame");

        for (float a = 0; a <= 1; a += Time.deltaTime/transitionTime)
        {
            transitionScreen.color = new Color(1, 1, 1, a);
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

}
