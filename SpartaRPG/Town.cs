using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRPG
{

    //마을
    public class Town
    {
        public Rest rest = new Rest();
        public void Start(Player player, Shop shop) //마을 입장 시
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("\t여기는 마을입니다.");
                Console.WriteLine("던전에 들어가기 전 행동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식");
                Console.WriteLine("\n원하는 행동을 선택해주세요.");
                int move; //선택한 행동
                while (!int.TryParse(Console.ReadLine(), out move))
                {
                    Console.WriteLine("정해진 행동의 번호가 아닙니다.");
                    Console.WriteLine("원하는 행동을 선택해주세요.");
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
                    case 4:
                        rest.INN(player);
                        Console.Clear();
                        break;
                    case 1010:
                        player.Gold += 90000;
                        Console.WriteLine("\n-------------\n테스트용 골드 충전\n-------------\n");
                        player.Balance();
                        Console.ReadKey(true);
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

}
