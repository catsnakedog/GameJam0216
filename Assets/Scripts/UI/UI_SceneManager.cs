using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_SceneManager : MonoBehaviour
{
    public GameObject Loading;
    public Text Loading_text;

    public CanvasGroup Fade_img;
    float fadeDuration = 2;
    public static UI_SceneManager Instance { get  { return instance; } }

    private static UI_SceneManager instance;

    private void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 제거*
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() =>
        {
            Loading.SetActive(false);
        })
        .OnComplete(() =>
        {
            Fade_img.blocksRaycasts = false;
        });
    }
    

    public void ChangeScene(string sceneName)
    {
        Debug.Log("진입확인");
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() =>
        {
            Fade_img.blocksRaycasts = true; //아래 레이캐스트 막기
        })
        .OnComplete(() =>
        {
            StartCoroutine("LoadScene", sceneName);
            //로딩화면 띄우며, 씬 로드 시작
        });
    }

    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true); //로딩 화면을 띄움

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; //퍼센트 딜레이용

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; //씬 전환 준비 완료
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            Loading_text.text = percentage.ToString("0") + "%"; //로딩 퍼센트 표기
        }
    }
     // [출처] [Asset] Unity3D 'DOTween' 5 : 씬 전환과 로딩 화면 / 페이드 인 아웃 모션 구현방법|작성자 두야



}



