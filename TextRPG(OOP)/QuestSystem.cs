using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class QuestSystem
    {
        public struct Quest
        {
            public Quest(string _quest_text)
            {
                completed = false;
                active = true;
                quest_text = _quest_text;
            }

            public bool completed;
            public bool active;
            public string quest_text;
        }

        private List<Quest> quests;

        public QuestSystem()
        {
            quests = new List<Quest>();
            add_quest("Kill 5 slimes");
            add_quest("Collect 7 coins");
            add_quest("Kill 5 Goblin Folks");
        }

        private int quest_text_start_cursor_x = 0, quest_text_start_cursor_y = 0;


        void on_slime_kill_callback()
        {

        }
        public int add_quest(string _quest_text)
        {
            quests.Add(new Quest(_quest_text));
            return quests.Count - 1;
        }

        public void complete_quest(int index)
        {
            Quest quest = quests[index];
            quest.completed = true;
            quests[index] = quest;
            on_render_update(quest_text_start_cursor_x, quest_text_start_cursor_y);
        }

        public void on_render_update(int cursor_x, int cursor_y)
        {
            quest_text_start_cursor_x = cursor_x;
            quest_text_start_cursor_y = cursor_y;

            for (int current = 0; current < quests.Count; current++)
            {
                Console.SetCursorPosition(cursor_x, cursor_y + current);

                Console.Write(quests[current].quest_text);

                if (!quests[current].active)
                    Console.Write(" = Inactive");
                else if (quests[current].completed)
                    Console.Write(" = Completed");
                else
                    Console.Write(" = Incomplete");
            }
        }

    }
}
