using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public float typingSpeed = 0.05f;
    public float delayBeforeStart = 1f;

    public TextMeshPro textMeshPro;
    private string fullText;
    private string currentText;

    private void Awake()
    {
        fullText = textMeshPro.text;
        textMeshPro.text = string.Empty;
    }

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
