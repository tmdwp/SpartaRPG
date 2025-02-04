using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace SpartaRPG
{
    //캐릭터 관련
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; set; }
        int Attack { get; }
        int Defense { get; }

        string Job { get; }
    }

    internal class RPG
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n이 곳에 오신 것을 환영합니다.");
            Console.WriteLine("당신의 이름은?");
            string name = Console.ReadLine();
            Player player = new Player(name);

            Town town = new Town();
            Shop shop = new Shop();
            shop.SettingShop(); //상점 목록 초기화
            town.Start(player, shop);
        }
    }
}
