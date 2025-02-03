using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public enum ItemCategory
    {
        Weapon = 1,
        Armor
    }

    public interface Item
    {
        public string Name {  get; }
        public int Category {  get;}
        public int Stat {  get; set; }
        public string Descript { get;}
    }

    public class MyItem : Item
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public int Stat { get; set; }
        public string Descript { get; set; }
        public bool IsEquip;
 
        public MyItem(string name,  int category, int stat, string descript)
        {
            Name = name;
            Category = category;
            Stat = stat;
            Descript = descript;
            IsEquip = false;
        }

        public void Equip(Player player)
        {
            if (IsEquip == false)
            {
                if (Category == (int)ItemCategory.Weapon)
                {
                    player.Attack += Stat;
                    player.EquipAttack += Stat;
                }
                else if (Category == (int)ItemCategory.Armor)
                {
                    player.Defense += Stat;
                    player.EquipDefense += Stat;
                }
                IsEquip = true;
            }
            else
            {
                if (Category == 1)
                {
                    player.Attack -= Stat;
                    player.EquipAttack -= Stat;
                }
                else if (Category == 2)
                {
                    player.Defense -= Stat;
                    player.EquipDefense -= Stat;
                }
                IsEquip = false;
            }
        }
        public void Inform()
        {
            if (IsEquip == true)
            {
                Console.Write("[E]");
            }
            if (Category == 1)
                Console.WriteLine(Name + " | 공격력 +" + Stat + " | " + Descript);
            else Console.WriteLine(Name + " | 방어력 +" + Stat + " | " + Descript);
        }
    }

    public class ShopItem : Item
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public int Stat { get; set; }
        public string Descript { get; set; }
        public bool IsBuy;
        public int Price;
        public ShopItem(string name, int category, int stat, string descript, int price)
        {
            Name = name;
            Category = category;
            Stat = stat;
            Descript = descript;
            IsBuy = false;
            Price = price;
        }

        public void Inform()
        {
            if (Category == 1)
                Console.Write(Name + " | 공격력 +" + Stat + " | " + Descript);
            else Console.Write(Name + " | 방어력 +" + Stat + " | " + Descript);
            if (IsBuy)
            {
                Console.WriteLine(" | 구매 완료");
            }
            else Console.WriteLine(" | " + Price+" G");
        }
    }
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; set; }
        int Attack { get; }
        int Defense { get; }

        string Job { get; }
    }

    public class Player : ICharacter
    {
        public int Level { get; set; }
        public string Name { get; }
        public string Job { get; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public Dictionary<int, MyItem> Inventory { get; set; }

        public int EquipAttack = 0;
        public int EquipDefense = 0;
        public Player(string name)
        {
            Level = 1;
            Name = name;
            Job = "Warrior";
            Health = 100;
            Attack = 10;
            Defense = 5;
            Gold = 1500;
            Inventory = new Dictionary<int, MyItem>();
        }

        public void StatusOpen()
        {
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("상태 보기");
            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Lv." + Level);
            Console.WriteLine(Name+"("+Job+")");
            Console.Write("공격력: " + Attack);
            if (EquipAttack != 0)
            {
                Console.WriteLine("(+" + EquipAttack + ")");
            }
            else Console.WriteLine();
            Console.Write("방어력: " + Defense);
            if (EquipDefense != 0)
            {
                Console.WriteLine("(+" + EquipDefense + ")");
            }
            else Console.WriteLine();
            Console.WriteLine("체력: "+ Health);
            Console.WriteLine("Gold: " + Gold);
            Console.WriteLine("\n-------------------------------------------");
        }

        public void OpenInventory()
        {
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("인벤토리");
            Console.WriteLine("\n-------------------------------------------");
            int count = 1;
            foreach (MyItem item in Inventory.Values)
            {
                Console.Write("-"+count+" ");
                item.Inform();
                count++;
            }
            count = 0;

            Console.Write("다음 행동(장비 번호 입력 시 장착/장착 해제) : ");
            while(!int.TryParse(Console.ReadLine(), out count)){
                Console.WriteLine("잘못된 입력입니다.");
            }
            if(count != 0) {
            count--;
                if (count < Inventory.Count)
                {
                    Inventory[count].Equip(this);
                    Console.WriteLine("아이템을 장착했습니다");
                    StatusOpen();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 마을로 돌아갑니다.");
                }
            }
        }
    }

    public class Shop
    {
        private List<ShopItem> shopList;
        public Shop()
        {
            shopList = new List<ShopItem>();

        }
        public void SettingShop()
        {
            shopList.Add(new ShopItem("수련자 갑옷", (int)ItemCategory.Armor, 5, "수련자용 갑옷이다.", 500));
            shopList.Add(new ShopItem("무쇠 갑옷", (int)ItemCategory.Armor, 9, "무쇠로 만든 갑옷이다.", 1000));
            shopList.Add(new ShopItem("강철 갑옷", (int)ItemCategory.Armor, 15, "강철로 만든 갑옷이다.", 2000));
            shopList.Add(new ShopItem("목검", (int)ItemCategory.Weapon, 3, "생각보다 단단한 목검이다.", 500));
            shopList.Add(new ShopItem("훈련용 검", (int)ItemCategory.Weapon, 5, "훈련용이지만 날이 서있는 검이다.", 1200));
            shopList.Add(new ShopItem("매우 무거운 검", (int)ItemCategory.Weapon, 15, "많이 무거운 검이다. 다치지 않게 조심하자.", 2300));
        }
        public void ShopMenu(Player player)
        {
            int count = 1;
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("상점");
            Console.WriteLine("\n-------------------------------------------");
            foreach (ShopItem item in shopList) 
            {
                Console.Write("-" +count+" ");
                item.Inform();
                count++;
            }
            ShopBuy(player);
        }

        public void ShopBuy(Player player)
        {
            int buy;
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("현재 G : " + player.Gold + "G\n");
            Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ 구매할 아이템 번호)");
            while(!int.TryParse(Console.ReadLine(), out buy))
            {
                Console.WriteLine("아이템 번호가 존재하지 않습니다.");
                Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ 구매할 아이템 번호)");
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
                        MyItem buyedItem = new MyItem(selectItem.Name, selectItem.Category, selectItem.Stat, selectItem.Descript);
                        player.Inventory.Add(player.Inventory.Count, buyedItem);
                        selectItem.IsBuy = true;

                        Console.WriteLine("아이템을 구매하였습니다.");
                        Console.WriteLine("현재 G : " + player.Gold + "G\n");
                    }
                    else
                    {
                        Console.WriteLine("소지한 골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 마을로 돌아갑니다.");
            }
        }
    }

    public class Town
    {
        public void Start(Player player, Shop shop)
        {
            while (true)
            {
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("여기는 마을입니다.");
                Console.WriteLine("던전에 들어가기 전 행동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점\n");
                Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
                int move;
                while (!int.TryParse(Console.ReadLine(), out move))
                {
                    Console.WriteLine("정해진 행동의 번호가 아닙니다.");
                    Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
                }

                switch (move)
                {
                    case 1:
                        Console.Clear();
                        player.StatusOpen();
                        break;
                    case 2:
                        Console.Clear();
                        player.OpenInventory();
                        break;
                    case 3:
                        Console.Clear();
                        shop.ShopMenu(player);
                        break;
                    default:
                        Console.WriteLine("정해진 행동이 아닙니다. 게임하기 싫으시군요?");
                        Console.WriteLine("원하시는 대로 게임을 종료합니다.");
                        Console.WriteLine("좋은 하루 되시길 바랍니다.\n\n");
                        return;
                }
            }
        }
    }

    internal class RPG
    {
        static void Main(string[] args)
        {
            Console.WriteLine("당신의 이름은?");
            string name = Console.ReadLine();
            Player player = new Player(name);
            MyItem item = new MyItem("낡은 나무검", 1, 1, "낡은 나무 검이다");
            player.Inventory.Add(player.Inventory.Count, item);
            Town town = new Town();
            Shop shop = new Shop();
            shop.SettingShop();
            town.Start(player, shop);
        }
    }
}
