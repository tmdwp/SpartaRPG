

namespace SpartaRPG
{

    //아이템 관련
    public interface Item
    {
        public string Name { get; }
        public int Category { get; }
        public int Stat { get; set; }
        public string Descript { get; }
        public int Price { get; set; }
    }

    public enum ItemCategory //아이템 카테고리 분류
    {
        Weapon = 1,
        Armor,
        chaos
    }

    //내가 가진 아이템
    public class MyItem : Item
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public int Stat { get; set; }
        public string Descript { get; set; }
        public int Price { get; set; }

        public bool IsEquip;

        public MyItem(string name, int category, int stat, string descript, int price = 0 )
        {
            Name = name;
            Category = category;
            Stat = stat;
            Descript = descript;
            IsEquip = false;
            Price = price;
        }

        public void Equip(Player player, int itemNum)
        {
            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    if (IsEquip == false)
                    {
                        player.Attack += Stat;
                        player.EquipAttack += Stat;
                        IsEquip = true;
                        player.equipWeapon = itemNum;
                        Console.WriteLine("\n아이템을 장착했습니다");
                    }
                    else
                    {
                        player.equipWeapon = -1;
                        player.Attack -= Stat;
                        player.EquipAttack -= Stat;
                        IsEquip = false;
                        Console.WriteLine("\n아이템을 장착 해제했습니다");
                    }
                    break;
                case (int)ItemCategory.Armor:
                    if (IsEquip == false)
                    {
                        player.Defense += Stat;
                        player.EquipDefense += Stat;
                        IsEquip = true;
                        player.equipArmor = itemNum;
                        Console.WriteLine("\n아이템을 장착했습니다");
                    }
                    else
                    {
                        player.equipArmor = -1;
                        player.Defense -= Stat;
                        player.EquipDefense -= Stat;
                        IsEquip = false;
                        Console.WriteLine("\n아이템을 장착 해제했습니다");
                    }
                    break;
                case (int)ItemCategory.chaos:
                    if (IsEquip == false)
                    {
                        player.Attack = new Random().Next(-100, 100);
                        player.Defense = new Random().Next(-100, 100);
                        player.Health = new Random().Next(1, 1000);
                        IsEquip = true;
                        Console.WriteLine("\n아이템을 장착했습니다");
                    }
                    else
                    {
                        Console.WriteLine("\n!!!!! 해제할 수 없다! !!!!!");
                    }
                    break;
            }

        }
        public void Inform(bool isShop = false)
        {
            if (IsEquip == true)
            {
                Console.Write("[E]");
            }

            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    if(isShop)
                        Console.WriteLine(Name + " | 공격력 +" + Stat + " | " + Descript + " | "+ Price);
                    else Console.WriteLine(Name + " | 공격력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.Armor:
                    if (isShop)
                        Console.WriteLine(Name + " | 방어력 +" + Stat + " | " + Descript + " | " + Price);
                    else Console.WriteLine(Name + " | 방어력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.chaos:
                    Console.WriteLine(Name + " | ???? | " + Descript);
                    break;
            }
        }
    }

    //상점 아이템
    public class ShopItem : Item
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public int Stat { get; set; }
        public string Descript { get; set; }
        public bool IsBuy;
        public int Price { get; set; }
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
            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    Console.Write(Name + " | 공격력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.Armor:
                    Console.Write(Name + " | 방어력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.chaos:
                    Console.Write(Name + " | ???? | " + Descript);
                    break;
            }
            if (IsBuy)
            {
                Console.WriteLine(" | 구매 완료");
            }
            else Console.WriteLine(" | " + Price + " G");
        }
    }
}

