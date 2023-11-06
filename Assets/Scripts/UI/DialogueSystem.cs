using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using BNG;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private int branch;
    [SerializeField] private Dialog_script dialogScript;
    [SerializeField] private Speaker[] speakers;
    [SerializeField] private DialogData[] Dialogs;
    [SerializeField] private bool isAutoStart = true;
    [SerializeField] private AudioClip[] TTSs;
    [SerializeField] private AudioMixerGroup MixerName;
    [SerializeField] private GameObject transformRef;

    [SerializeField] private bool isFirst = false;
    private int currentDialogIndex = -1;
    private int currentSpeakerIndex = 0;
    
    [SerializeField]
    private float typingSpeed = 0.1f;
    private bool isTypingEffect = false;
    // Start is called before the first frame update
    private void Awake()
    {
        int index = 0;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Earth_plane")
        {
            for (int i = 0; i < dialogScript.Earth.Count; ++i)
            {
                if (dialogScript.Earth[i].branch == branch)
                {
                    Dialogs[index].name = dialogScript.Earth[i].name;
                    Dialogs[index].dialogue = dialogScript.Earth[i].Dialog;
                    index++;
                }
            }
        }
        else if (scene.name == "Dino_Sample")
        {
            for (int i = 0; i < dialogScript.Dino.Count; ++i)
            {
                if (dialogScript.Dino[i].branch == branch)
                {
                    Dialogs[index].name = dialogScript.Dino[i].name;
                    Dialogs[index].dialogue = dialogScript.Dino[i].Dialog;
                    index++;
                }
            }
        }
        else if (scene.name == "PathInDesert_Sample")
        {
            for (int i = 0; i < dialogScript.Desert.Count; ++i)
            {
                if (dialogScript.Desert[i].branch == branch)
                {
                    Dialogs[index].name = dialogScript.Desert[i].name;
                    Dialogs[index].dialogue = dialogScript.Desert[i].Dialog;
                    index++;
                }
            }
        }
        
        else if (scene.name == "FinalScene")
        {
            for (int i = 0; i < dialogScript.Earth_Ending.Count; ++i)
            {
                if (dialogScript.Earth_Ending[i].branch == branch)
                {
                    Dialogs[index].name = dialogScript.Earth_Ending[i].name;
                    Dialogs[index].dialogue = dialogScript.Earth_Ending[i].Dialog;
                    index++;
                }
            }
        }
    }

    private void Setup()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
            
        }
    }
    // Update is called once per frame
    public bool UpdateDialog()
    {
        if (isFirst == true)
        {
            Setup();

            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }

        if (InputBridge.Instance.RightTriggerDown || Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect == true)
            {
                isTypingEffect = false;
                
                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIndex].textDialogue.text = Dialogs[currentDialogIndex].dialogue;
                speakers[currentSpeakerIndex].objectArrow.SetActive(true);

                return false;
            }
            if (Dialogs.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            else
            {
                for (int i = 0; i < speakers.Length; i++)
                {
                    SetActiveObjects(speakers[i], false);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        SetActiveObjects(speakers[currentSpeakerIndex], false);
        currentDialogIndex++;
        currentSpeakerIndex = Dialogs[currentDialogIndex].speakerIndex;
        SetActiveObjects(speakers[currentSpeakerIndex], true);
        speakers[currentSpeakerIndex].textName.text = Dialogs[currentDialogIndex].name;
        speakers[currentSpeakerIndex].textDialogue.text = Dialogs[currentDialogIndex].dialogue;
        StartCoroutine("OnTypingText");
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialogue.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);
        
        speaker.objectArrow.SetActive(false);
        
    }
    
    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;
        VRUtils.Instance.PlaySpatialClipAt(TTSs[currentDialogIndex], transformRef.transform.position, 1.0f, 1.0f, 0, MixerName);
        while (index <= Dialogs[currentDialogIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = Dialogs[currentDialogIndex].dialogue.Substring(0, index);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }
}



[System.Serializable]
public struct Speaker
{
    public Image imageDialogue;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    public GameObject objectArrow;
}

[System.Serializable]
public struct DialogData
{
    public int speakerIndex;
    public string name;
    [TextArea(3, 5)] public string dialogue;
} 