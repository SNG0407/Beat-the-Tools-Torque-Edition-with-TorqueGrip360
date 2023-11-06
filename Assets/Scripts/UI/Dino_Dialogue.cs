using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.Audio;

public class Dino_Dialogue : MonoBehaviour
{

    [SerializeField] private DialogueSystem[] dialog;
    [SerializeField] private GameObject StarGen;
    [SerializeField] private GameObject RewardUI;
    [SerializeField] private GameObject Unicorn;
    [SerializeField] private GameObject Portal;
    [SerializeField] private AudioMixerGroup MixerName;

    public AudioClip SuccessSound;
    private static Dino_Dialogue _instance;
    public bool interactionStart = false;

    public static Dino_Dialogue Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Dino_Dialogue>();
            if (_instance == null)
            {
                GameObject container = new GameObject("DialogManager");
                _instance = container.AddComponent<Dino_Dialogue>();
            }

            return _instance;
        }
    }

    public void SetInterStart()
    {
        interactionStart = true;
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        //씬 입장 대사 출력
        yield return new WaitUntil(() => dialog[0].UpdateDialog());
        //첫 대화 끝난 후 운석 생성기 작동
        yield return new WaitUntil(() => interactionStart);
        StarGen.SetActive(true);

        yield return new WaitUntil(() => Dinosaur_UImanager.Instance.Successed || Dinosaur_UImanager.Instance.Failed);
        
        StarGen.SetActive(false);
        yield return new WaitForSeconds(1);
        //성공 후 다이얼로그 진행
        if (Dinosaur_UImanager.Instance.Successed)
        {
            VRUtils.Instance.PlaySpatialClipAt(SuccessSound, RewardUI.transform.position, 1f, 1f, 0f, MixerName);
            yield return new WaitUntil(() => dialog[1].UpdateDialog());
        }
        else if (Dinosaur_UImanager.Instance.Failed)
            yield return new WaitUntil(() => dialog[2].UpdateDialog());
        //보상 선택
        RewardUI.SetActive(true);
        yield return new WaitUntil(() => (PlayerPrefs.GetString("Dino_Reward") != ""));
        yield return new WaitUntil(() => dialog[3].UpdateDialog());
        
        //대화 끝난 후 자신감 상승, 이동 튜토리얼 진행
        yield return new WaitForSeconds(2f);
        GameManager.Instance.Confidence(0.5f);
        Unicorn.SetActive(true);
        Portal.SetActive(true);
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
