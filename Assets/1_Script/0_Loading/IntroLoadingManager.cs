using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class LoadingManager : MonoBehaviour
{
    static string NextScene;

    [SerializeField] private Image IMG_ProgressBar;
    [SerializeField] private GameObject[] TXT_Text;
    public static void LoadScene(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextAnimation());

        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator TextAnimation()
    {
        for (int i = 0; i < TXT_Text.Length; i++)
        {
            TXT_Text[i].transform.DOLocalMoveY(-230, 0.5f).SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op =SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false;

        float curTime = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                IMG_ProgressBar.fillAmount = op.progress;
            }
            else
            {
                curTime += Time.unscaledDeltaTime;
                IMG_ProgressBar.fillAmount = Mathf.Lerp(0.9f, 1f, curTime);
                if(op.progress>= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
