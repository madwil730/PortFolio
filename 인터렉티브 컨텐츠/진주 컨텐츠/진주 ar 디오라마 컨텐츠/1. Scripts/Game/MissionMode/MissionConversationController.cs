﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MissionConversationController : MonoBehaviour
{
    [SerializeField] MissionModeController _MissionModeController;

    [Header("Conversation")]
    [SerializeField] Text TitleText;
    [SerializeField] Text Content;

    [Header("Dialogue")]
    [SerializeField] string[] TitleNames;
    [TextArea(1, 3)] [SerializeField] string[] DialogueContent;
    [SerializeField] float[] TextSpeed;

    [Header("Narration")]
    AudioSource _AudioSource;
    [SerializeField] AudioClip[] _NarrationClip;

    Coroutine _ChatCoroutine;
    Coroutine _ChatAutoNext;

    int DialogueMaxIndex;
    int DialogueIndex = -1;
    bool IsChating = false;         // 글씨가써지고 있는지

    private void OnEnable()
    {
        CommunicationInitialize();
    }

    private void CommunicationInitialize()
    {
        if (_AudioSource == null)
        {
            _AudioSource = GetComponent<AudioSource>();
        }
        DialogueMaxIndex = TitleNames.Length;
        DialogueIndex = -1;
        IsChating = false;
        TitleText.text = "";
        Content.text = "";
        NextChat();
    }

    public void NextChat()
    {
        DialogueIndex++;
        if (DialogueIndex < DialogueMaxIndex)
        {
            if (_ChatCoroutine != null) StopCoroutine(_ChatCoroutine);
            _ChatCoroutine = StartCoroutine(StartChating());
        }
        else
        {
            _MissionModeController.StartMissionMode();
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartChating()
    {
        IsChating = true;
        TitleText.text = TitleNames[DialogueIndex];
        int DialogueCharacterCount = DialogueContent[DialogueIndex].Length;
        Content.text = "";
        _AudioSource.clip = _NarrationClip[DialogueIndex];
        _AudioSource.Play();
        StringBuilder _SB = new StringBuilder();
        while (true)
        {
            for (int i = 0; i < DialogueCharacterCount; i++)
            {
                _SB.Append(DialogueContent[DialogueIndex][i]);
                Content.text = _SB.ToString();
                yield return new WaitForSeconds(TextSpeed[DialogueIndex]);
            }
            break;
        }
        IsChating = false;
        yield return new WaitForSeconds(3f);
        if (!IsChating && DialogueIndex != 0) NextChat();
    }

    // 넘어가기 및 다음 버튼 클릭
    public void NextButtonClick()
    {
        if (IsChating)
        {
            StopCoroutine(_ChatCoroutine);
            TitleText.text = TitleNames[DialogueIndex];
            Content.text = DialogueContent[DialogueIndex];
            _ChatAutoNext = StartCoroutine(AutoNext());
            IsChating = false;
        }
        else
        {
            StopCoroutine(_ChatCoroutine);
            if (_ChatAutoNext != null) StopCoroutine(_ChatAutoNext);
            NextChat();
        }
    }

    IEnumerator AutoNext()
    {
        yield return new WaitForSeconds(3f);
        if (!IsChating) NextChat();
    }
}
