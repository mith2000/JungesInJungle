using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    private static DialogSystem instance;

    public TextMeshProUGUI textDisplay;
    public List<string> sentences;
    public GameObject continueButton;
    public GameObject background;
    public DialogContainer container;
    private int index = 0;
    private float typeSpeed = 0.02f;
    private Animator anim;
    private GameObject playerController;

    [HideInInspector]
    public bool isInDialog = false;

    private void Awake()
    {
        if (instance == null)
        {
            anim = GetComponent<Animator>();
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static DialogSystem GetInstance()
    {
        return instance;
    }

    public void StartText()
    {
        isInDialog = true;
        continueButton.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player");
        playerController.GetComponent<PlayerController>().UnableControl();
        StartCoroutine(Type());
    }

    public void AddSentences(List<string> newSen)
    {
        foreach (string sentence in newSen)
        {
            sentences.Add(sentence);
        }
    }

    IEnumerator Type()
    {
        anim.SetTrigger("Change");
        background.SetActive(true);

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        //enable continue button at the end of sentence
        continueButton.SetActive(true);
    }

    public void ContinueDialog()
    {
        continueButton.SetActive(false);

        if (index < sentences.Count - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            isInDialog = false;
            index++;
            textDisplay.text = "";
            continueButton.SetActive(false);
            background.SetActive(false);
            playerController.GetComponent<PlayerController>().EnableControl();
        }
    }
}
