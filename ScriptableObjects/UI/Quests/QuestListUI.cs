using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] QuestItemUI questPrefab;
    QuestList questList;

    // Start is called before the first frame update
    void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.onQuestsUpdated += UpdateQuestListUI;
        UpdateQuestListUI();
    }

    void UpdateQuestListUI()
    {
        transform.DetachChildren();
        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(status);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
