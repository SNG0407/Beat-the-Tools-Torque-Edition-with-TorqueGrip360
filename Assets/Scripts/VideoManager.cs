using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    private VideoPlayer video;
    [SerializeField] private GameObject video1;
    [SerializeField] private GameObject transformRef;
    [SerializeField] private AudioClip audio1;
    [SerializeField] private AudioClip audio2;
    [SerializeField] private AudioMixerGroup mixerName;
    
    private bool played;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        video = GetComponent<VideoPlayer>();
        yield return new WaitForSeconds(5);
        video1.SetActive(true);
        video.Play();
        played = true;
        yield return new WaitForSeconds(3);
        VRUtils.Instance.PlaySpatialClipAt(audio1, transformRef.transform.position, 1f, 1f, 0f, mixerName);
        yield return new WaitForSeconds(3);
        VRUtils.Instance.PlaySpatialClipAt(audio2, transformRef.transform.position, 1f, 1f, 0f, mixerName);


    }

    // Update is called once per frame
    void Update()
    {
        if (played == true)
        {
            if (video.isPaused == true)
            {
                video1.SetActive(false);
            }
        }
    }
}
