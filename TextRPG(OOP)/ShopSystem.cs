using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class ShopSystem
    {
        public enum shops
        {
            shop_inactive,
            shop_attack_power,
            shop_armor,
            shop_health,
        }

        public bool is_shop_active = false;
        public shops active_shop = shops.shop_inactive;
        public void on_player_move(char character)
        {
            Console.SetCursorPosition(Map.mapX + 1, 11);

            switch (character)
            {
                case Map.shopAttackPower:
                    {
                        Console.Write("[SHOP] Selling 1 Attack Power for 2 Coins ");
                        active_shop = shops.shop_attack_power;
                        return;
                    }
                case Map.shopHealth:
                    {

                        Console.Write("[SHOP] Selling 1 Health for 2 Coins ");
                        active_shop = shops.shop_health;
                        return;
                    }
                case Map.shopArmor:
                    {
                        Console.Write("[SHOP] Selling 1 Armor for 2 Coins . Press B to buy");
                        active_shop = shops.shop_armor;
                        return;
                    }
                default:
                    break;
            }

            Console.SetCursorPosition(Map.mapX + 1, 11);
            Console.Write("                                                   ");

            Console.SetCursorPosition(Map.mapX + 1, 12);
            Console.Write("                                                   ");

            active_shop = shops.shop_inactive;
        }

        public void on_frame(ConsoleKeyInfo playerInput)
        {
            if (active_shop == shops.shop_inactive)
                return;

            Console.SetCursorPosition(Map.mapX + 1, 12);

            ConsoleKeyInfo read_key = Console.ReadKey(true);

            if (DungeonExplorer.gameManager.gameMap.mainPlayer.playerCoins < 2)
            {
                Console.Write("Insufficient coins                                 ");
                return;
            }

            if (read_key.Key == ConsoleKey.B)
            {
                DungeonExplorer.gameManager.gameMap.mainPlayer.playerCoins -= 2;
                switch (active_shop)
                {
                    case shops.shop_attack_power:
                        {
                            Console.Write("Bought 1 attack power for 2 coins!");
                            active_shop = shops.shop_attack_power;
                            DungeonExplorer.gameManager.gameMap.mainPlayer.playerDamage += 1;
                            return;
                        }
                    case shops.shop_health:
                        {
                            Console.Write("Bought 1 health for 2 coins!");
                            active_shop = shops.shop_health;
                            DungeonExplorer.gameManager.gameMap.mainPlayer.healthSystem.health += 1;
                            return;
                        }
                    case shops.shop_armor:
                        {
                            Console.Write("Bought 1 armor for 2 coins!");
                            active_shop = shops.shop_armor;
                            DungeonExplorer.gameManager.gameMap.mainPlayer.healthSystem.armor += 1;
                            return;
                        }
                    default:
                        break;
                }

            }
        }


    }
}
