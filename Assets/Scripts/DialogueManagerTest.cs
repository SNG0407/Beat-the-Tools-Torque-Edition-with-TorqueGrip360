using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueManagerTest : MonoBehaviour
{
    public Text dialogueText;
    public GameObject NextText;
    public CanvasGroup UICanvas_DialogueGroup;
    public Queue<string> sentences;
    //public Queue<AudioClip> Audios;
    public AudioClip[] Audios;
    private string currentSentence;
    private AudioSource currentAudio;

    public float TypeSpeed = 0.1f;

    private bool isTyping = true;

    public static DialogueManagerTest instance;

    public int DialogueIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        currentAudio = this.GetComponent<AudioSource>();
    }

    public void Ondialogue(string[] lines, AudioClip[] audios)
    {
        //sentences.Clear();
        //Audios.Clear(); 
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        int i = 0;
        Audios = new AudioClip[audios.Length];
        foreach (AudioClip audio in audios)
        {
            if (audio != null)
            {
                //Debug.Log($"A single Audio Clip : {audio}");
                //Audios.Enqueue(audio);
                Audios[i] = audio;
                i++;
            }
            else
                Debug.Log("Audio Clip is empty");
        }
        
        UICanvas_DialogueGroup.alpha = 1;
        UICanvas_DialogueGroup.blocksRaycasts = true;
        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            Debug.Log($"A single Audio Clip : { Audios[DialogueIndex]}");
            currentAudio.clip = Audios[DialogueIndex];
            isTyping = true;
            NextText.SetActive(false);
            StartCoroutine(Typing(currentSentence, currentAudio));
            DialogueIndex++;
        }
        else
        {
            UICanvas_DialogueGroup.alpha = 0;
            UICanvas_DialogueGroup.blocksRaycasts = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueText.text.Equals(currentSentence))
        {
            NextText.SetActive(true);
            isTyping = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R: next text");
            NextSentenceBtn();
        }
    }

    IEnumerator Typing(string line, AudioSource audio)
    {
        audio.Play();
        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(TypeSpeed);
        }
    }

    public void NextSentenceBtn()
    {
        Debug.Log("Next Btn pressed");
        if (!isTyping)
            NextSentence();
    }
}
