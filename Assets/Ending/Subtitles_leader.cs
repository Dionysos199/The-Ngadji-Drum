using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles_leader : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public GameObject textBox;

    private void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "First line";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Second line";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}
