using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class QuestSystem
    {
        public struct Quest
        {
            public Quest(bool _active, string _quest_text, string _extra_text)
            {
                completed = false;
                quest_text = _quest_text;
                extra_text = _extra_text;
                active = _active;
            }

            public bool completed;
            public bool active;
            public string quest_text;
            public string extra_text;
        }
        private int slimes_killed = 0;
        private int goblins_killed = 0;
        private int coins_collected = 0;

        private List<Quest> quests;

        int kill_5_slimes_quest_index = 0;
        int collect_7_coins_quest_index = 0;
        int kill_5_goblins_quest_index = 0;
        int game_finish_quest_index = 0;
        public QuestSystem()
        {
            quests = new List<Quest>();
            kill_5_slimes_quest_index = add_quest(true, "Kill 5 slimes", "[Remaining : 5 ]");
            collect_7_coins_quest_index = add_quest(true, "Collect 10 coins", "[Remaining : 10 ]");
            kill_5_goblins_quest_index = add_quest(true, "Kill 3 Goblins", "[Remaining : 3 ]");
            game_finish_quest_index = add_quest(false, "Win the game", "");
        }

        private int quest_text_start_cursor_x = 0, quest_text_start_cursor_y = 0;

        public void on_death_callback(Character character)
        {
            switch (character.name)
            {
                case "Slime":
                    {
                        if (!quests[kill_5_slimes_quest_index].completed)
                        {
                            slimes_killed += 1;

                            if (slimes_killed == 5)
                                update_quest(true, kill_5_slimes_quest_index, "               ", true);
                            else
                                update_quest(true, kill_5_slimes_quest_index, "[Remaining : " + (5 - slimes_killed) + "]", false);
                        }

                        break;
                    }
                case "Goblin":
                    {
                        if (!quests[kill_5_goblins_quest_index].completed)
                        {
                            goblins_killed += 1;
                            if (goblins_killed == 3)
                                update_quest(true, kill_5_goblins_quest_index, "               ", true);
                            else
                                update_quest(true, kill_5_goblins_quest_index, "[Remaining : " + (3 - goblins_killed) + "]", false);
                        }
                        break;
                    }
            }

            if (quests[collect_7_coins_quest_index].completed &&
    quests[kill_5_goblins_quest_index].completed &&
    quests[kill_5_slimes_quest_index].completed)
                update_quest(true, game_finish_quest_index, "                   ", false);
        }
        public void on_coin_collect()
        {
            if (!quests[collect_7_coins_quest_index].completed)
            {
                coins_collected += 1;

                if (coins_collected == 10)
                    update_quest(true, collect_7_coins_quest_index, "               ", true);
                else
                    update_quest(true, collect_7_coins_quest_index, "[Remaining : " + (10 - coins_collected) + "]", false);
            }

            if (quests[collect_7_coins_quest_index].completed &&
                quests[kill_5_goblins_quest_index].completed &&
                quests[kill_5_slimes_quest_index].completed)
                update_quest(true, game_finish_quest_index, "                   ", false);


        }

        public void on_game_win()
        {
            update_quest(true, game_finish_quest_index, "                   ", true);
        }

        public int add_quest(bool active, string _quest_text, string _extra_text)
        {
            quests.Add(new Quest(active, _quest_text, _extra_text));
            return quests.Count - 1;
        }

        public void update_quest(bool active, int index, string _extra_text, bool completed)
        {
            Quest quest = quests[index];
            quest.completed = completed;
            quest.active = active;
            quest.extra_text = _extra_text;

            quests[index] = quest;
            on_render_update(quest_text_start_cursor_x, quest_text_start_cursor_y);
        }

        public void on_render_update(int cursor_x, int cursor_y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            quest_text_start_cursor_x = cursor_x;
            quest_text_start_cursor_y = cursor_y;

            for (int current = 0; current < quests.Count; current++)
            {
                Console.SetCursorPosition(cursor_x, cursor_y + current);

                Console.ForegroundColor = ConsoleColor.White;


                if (quests[current].completed)
                {
                    Console.Write(quests[current].quest_text + " = ");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Completed " + quests[current].extra_text + "  ");
                }
                else if (quests[current].active)
                {
                    Console.Write(quests[current].quest_text + " = ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Incomplete " + quests[current].extra_text + "  ");
                }

            }
        }

    }
}
