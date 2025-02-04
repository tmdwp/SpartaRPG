using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRPG
{

    //플레이어 정보
    public class Player : ICharacter
    {
        public int Level { get; set; }
        public string Name { get; }
        public string Job { get; }
        public int Health { get; set; }
        private int MaxHp { get; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public Dictionary<int, MyItem> Inventory { get; set; }
        public int equipWeapon = -1, equipArmor = -1;
        public int EquipAttack = 0;
        public int EquipDefense = 0;
        public Player(string name)
        {
            Level = 1;
            Name = name;
            Job = "전사";
            MaxHp = 100;
            Health = MaxHp;
            Attack = 10;
            Defense = 5;
            Gold = 1500;
            Inventory = new Dictionary<int, MyItem>();
        }

//능력치 확인
        public void StatusOpen()
        {
            int select;
            do
            {
                select = -1;
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------\n");
                Console.WriteLine("\t\t상태 보기");
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("Lv." + Level);
                Console.WriteLine(Name + "(" + Job + ")");
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
                Console.WriteLine("체력: " + Health);
                Balance();
                Console.WriteLine("\n-------------------------------------------");

                if (select == 0)
                    break;
                Console.WriteLine("0. 나가기");
                Console.Write("원하는 행동을 입력해주세요: ");


            } while ((!int.TryParse(Console.ReadLine(), out select)) || select != 0);

        }

//인벤토리 확인
        public void OpenInventory()
        {
            int input = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------" +
                    "\n\t\t인벤토리\n\n-------------------------------------------" +
                    "\n\t\t[ 아이템 목록 ]\n");


                foreach (MyItem item in Inventory.Values)
                {
                    item.Inform();
                }
                Console.WriteLine("\n-------------------------------------------\n");

                Console.WriteLine("\n다음 행동? 0 : 나가기 1: 장비 장착 관리");
                while (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                if (input == 0)
                    break;
                else if (input == 1)
                {
                    Console.Clear();
                    Console.WriteLine("\n-------------------------------------------" +
                        "\n\t\t인벤토리\n\n-------------------------------------------" +
                        "\n\t\t[ 아이템 목록 ]\n");
                    foreach (MyItem item in Inventory.Values)
                    {
                        Console.Write("- " + input + " ");
                        item.Inform();
                        input++;
                    }
                    Console.WriteLine("\n-------------------------------------------\n");

//아이템 장착 관리
                    Console.Write("\n 장착/장착 해제할 아이템 : (0 - 나가기 | 장비 번호 - 장착/해제)");
                    while (!int.TryParse(Console.ReadLine(), out input))
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    if (input != 0)
                    {
                        input--;
                        if (input < Inventory.Count)
                        {
                            if (Inventory[input].Category == (int)ItemCategory.Weapon && equipWeapon != -1)
                                Inventory[equipWeapon].Equip(this, equipWeapon);
                            else if (Inventory[input].Category == (int)ItemCategory.Armor && equipArmor != -1)
                                Inventory[equipArmor].Equip(this, equipArmor);
                            Inventory[input].Equip(this, input);
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                }
            } while (input != 0);
        }

        public void HpMax() //체력 최대치로 회복
        {
            Console.WriteLine("체력이 최대치로 회복되었습니다!");
            Health = MaxHp;
        }
        public void Balance() //현재 가지고 있는 잔액 확인
        {
            Console.WriteLine($"잔액 : {Gold}G");
        }
    }

}
