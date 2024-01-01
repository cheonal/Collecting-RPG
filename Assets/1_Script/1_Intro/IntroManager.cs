using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject GO_OnLoading;
    [SerializeField] private GameObject GO_EndLoading;
    private void Start()
    {
        StartCoroutine(OnLoading());
    }
    private IEnumerator OnLoading()
    {
        GO_OnLoading.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(1.5f);
        GO_OnLoading.SetActive(false);
        GO_OnLoading.transform.DOKill();

        GO_EndLoading.SetActive(true);
        GO_EndLoading.GetComponent<TextMeshProUGUI>().DOFade(0, 1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutQuad);        
    }
    public void OnClickScreen()
    {
        LoadingManager.LoadScene("MainScene");
    }
}
