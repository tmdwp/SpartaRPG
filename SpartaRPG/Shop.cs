using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRPG
{

    //상점
    public class Shop
    {
        public List<ShopItem> shopList; //상점 목록
        public Shop()
        {
            shopList = new List<ShopItem>();

        }
        public void SettingShop()
        {
            shopList.Add(new ShopItem("가죽 갑옷", (int)ItemCategory.Armor, 5, "가죽 갑옷이다. 생각보다 질기다.", 500));
            shopList.Add(new ShopItem("사슬 갑옷", (int)ItemCategory.Armor, 9, "사슬 갑옷이다.", 1000));
            shopList.Add(new ShopItem("강철 갑옷", (int)ItemCategory.Armor, 15, "강철로 만든 갑옷이다.", 2000));
            shopList.Add(new ShopItem("목검", (int)ItemCategory.Weapon, 3, "단단한 목검이다.", 500));
            shopList.Add(new ShopItem("훈련용 검", (int)ItemCategory.Weapon, 5, "훈련용이지만 날이 서있는 검이다.", 1200));
            shopList.Add(new ShopItem("매우 무거운 검", (int)ItemCategory.Weapon, 15, "매우 무거운 검이다. 발조심 하자.", 2300));
            shopList.Add(new ShopItem("알 수 없음", (int)ItemCategory.chaos, 0, "상점 주인도 왜 이게 있는지 알 수 없는 물건이다.", 10000));
        }
        public void ShopMenu(Player player) //상점 아이템 목록 출력
        {
            int count = 1;
            int act;
            do
            {
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------\n");
                Console.WriteLine("\t\t상점");
                Console.WriteLine("\n-------------------------------------------");

                foreach (ShopItem item in shopList)
                {
                    Console.Write("- " + count + " ");
                    item.Inform();
                    count++;
                }
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                while (!int.TryParse(Console.ReadLine(), out act))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                if (act == 1)
                {
                    ShopBuy(player);
                }
                else if(act == 2)
                {
                    ShopSell(player);
                }
                if (act == 0)
                {
                    Console.WriteLine("마을로 돌아갑니다.\n-------------");
                    Console.WriteLine("\n아무 키나 입력하세요.");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                }
                count = 1;
            } while (act != 0);
        }

        public void ShopBuy(Player player) //아이템 구매
        {
            int buy;
            Console.WriteLine("\n-------------------------------------------\n");
            player.Balance();
            Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + shopList.Count + " - 구매할 아이템 번호)");
            while (!int.TryParse(Console.ReadLine(), out buy))
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + shopList.Count + " - 구매할 아이템 번호)");
            }
            if (buy > 0 && buy <= shopList.Count)
            {
                buy--;
                if (!shopList[buy].IsBuy)
                {
                    if (player.Gold >= shopList[buy].Price)
                    {
                        ShopItem selectItem = shopList[buy];
                        player.Gold -= selectItem.Price;
                        player.Inventory.Add(player.Inventory.Count, new MyItem(selectItem.Name, selectItem.Category, selectItem.Stat, selectItem.Descript, (selectItem.Price * 8 /10)));
                        selectItem.IsBuy = true;

                        Console.WriteLine("아이템을 구매하였습니다.");
                        player.Balance();
                    }
                    else
                    {
                        Console.WriteLine("\n소지한 골드가 부족합니다.");
                        player.Balance();
                    }
                }
                else
                {
                    Console.WriteLine("\n이미 구매한 아이템입니다.");
                }
                buy++;
                Console.WriteLine("\n아무 키나 입력하세요.");
                Console.ReadKey(true);
            }
            else if (buy != 0)
            {
                Console.WriteLine("아이템 번호가 존재하지 않습니다.");
            }
        }

        public void ShopSell(Player player)
        {
            Console.Clear();
            int sell;
            foreach (var item in player.Inventory.Values) {
                item.Inform(true);
            }
            Console.WriteLine("\n-------------------------------------------\n");
            player.Balance();
            Console.WriteLine("판매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + player.Inventory.Count + " - 판매할 아이템 번호)");
            while (!int.TryParse(Console.ReadLine(), out sell))
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("판매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + player.Inventory.Count + " - 판매할 아이템 번호)");
            }
            
            if (sell > 0 && sell <= player.Inventory.Count)
            {
                sell--;
                if (player.Inventory[sell].IsEquip)
                    player.Inventory[sell].Equip(player,sell);
                var sellItem = player.Inventory[sell];
                player.Gold += sellItem.Price;
                player.Inventory.Remove(sell);
                Console.WriteLine("\n아이템을 판매했습니다.");
                
                player.Balance();
                sell++;

                ShopItem item = shopList.Find(item => item.Name == sellItem.Name);
                item.IsBuy = false;

                Console.WriteLine("\n아무 키나 입력하세요.");
                Console.ReadKey(true);
            }
            else if (sell != 0)
            {
                Console.WriteLine("아이템 번호가 존재하지 않습니다.");
            }
        }
    }

}
