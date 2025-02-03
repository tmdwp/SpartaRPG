using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public struct Item
    {
        public string Name;
        public int Category;
        public int Stat;
        public string Descript;
        public bool IsEquip;

        public Item(string name,  int category, int stat, string descript)
        {
            this.Name = name;
            this.Category = category;
            this.Stat = stat;
            this.Descript = descript;
            this.IsEquip = false;
        }

        public void Equip(Player player)
        {
            if (IsEquip == false)
            {
                if (Category == 1)
                {
                    player.Attack += Stat;
                    player.EquipAttack += Stat;
                }
                else if (Category == 2)
                {
                    player.Defense += Stat;
                    player.EquipDefense += Stat;
                }
                IsEquip = true;
            }
            else
            {
                if (Category == 1)
                    player.EquipAttack -= Stat;
                else if (Category == 2)
                    player.EquipDefense -= Stat;
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

        public Dictionary<int, Item> Inventory { get; set; }

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
            Inventory = new Dictionary<int, Item>();
        }

        public void StatusOpen()
        {
            Console.WriteLine("\n-------------------------------------------");
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
            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("인벤토리");
            Console.WriteLine("\n-------------------------------------------");
            int count = 1;
            foreach (Item item in Inventory.Values)
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



    public class Town
    {
        public void Start(Player player)
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
            Item item = new Item("낡은 나무검", 1, 1, "낡은 나무 검이다");
            player.Inventory.Add(player.Inventory.Count, item);
            Town town = new Town();
            town.Start(player);
        }
    }
}
