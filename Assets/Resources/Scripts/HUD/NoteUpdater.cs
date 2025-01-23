using UnityEngine;
using System.Collections.Generic;


public class NoteUpdater : MonoBehaviour
{
    [SerializeField] float fontSize;
    [SerializeField] string[] questnames;
    [SerializeField] GameObject textPrefab;
    private Dictionary<string, GameObject> quests = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> questProgression = new Dictionary<string, bool>();
    private float height = 310f;
    private int maxGhosts = 0;
    private int ghostCounter = 0;
    private GameObject counterDisplay;

    private void Start()
    {
        PopulateQuests();
        CreateCouter();
    }

    private void Update()
    {
        counterDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = ghostCounter + " von " + maxGhosts + " Geistern gefuden";
    }

    public void CompleteQuest(string quest)
    {
        GameObject checkedbox = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        checkedbox.transform.SetParent(quests[quest].transform);
        checkedbox.transform.localEulerAngles = new Vector3(0, 0, 0);
        checkedbox.transform.localScale = new Vector3(1, 1, 1);
        checkedbox.transform.localPosition = new Vector3(4f, 5.5f, 0);
        checkedbox.GetComponent<TMPro.TextMeshProUGUI>().text = "/";


        questProgression[quest] = true;

        bool temp = true;
        foreach (var questProgress in questProgression)
        {
            if (!questProgress.Value)
            {
                temp = false;
            }
        }
        if (temp)
        {
            //Win Game
        }
    }

    public void UpdateGhostCounter()
    {
        ghostCounter += 1;
        counterDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = ghostCounter + " von " + maxGhosts + " Geistern gefuden";
    }

    private void PopulateQuests()
    {
        foreach (string quest in questnames)
        {
            GameObject newQuest = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            newQuest.transform.SetParent(gameObject.transform);
            newQuest.transform.localEulerAngles = new Vector3(0, 0, 0);
            newQuest.transform.localScale = new Vector3(1, 1, 1);
            newQuest.transform.localPosition = new Vector3(0, height, 0);
            newQuest.GetComponent<TMPro.TextMeshProUGUI>().text = quest;

            GameObject checkbox = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            checkbox.transform.SetParent(newQuest.transform);
            checkbox.transform.localEulerAngles = new Vector3(0, 0, 0);
            checkbox.transform.localScale = new Vector3(1, 1, 1);
            checkbox.transform.localPosition = new Vector3(420, -15, 0);
            checkbox.GetComponent<TMPro.TextMeshProUGUI>().text = "□";
            checkbox.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 46;


            height = height - 50;
            quests.Add(quest, checkbox);
            questProgression.Add(quest, false);
        }
    }

    private void CreateCouter()
    {
        counterDisplay = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        counterDisplay.transform.SetParent(gameObject.transform);
        counterDisplay.transform.localEulerAngles = new Vector3(0, 0, 0);
        counterDisplay.transform.localScale = new Vector3(1, 1, 1);
        counterDisplay.transform.localPosition = new Vector3(0, -300, 0);
        counterDisplay.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 30;
        counterDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = ghostCounter + "/" + maxGhosts + " Geistern gefuden";
    }

    public void UpdateMaxGhosts()
    {
        maxGhosts++;
    }
}
