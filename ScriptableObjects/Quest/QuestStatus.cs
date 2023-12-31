using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{

    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);

            }
        }
    }

}
