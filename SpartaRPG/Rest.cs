using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRPG
{
    public class Rest
    {
        public void INN(Player player) {
            int act;
            do
            {
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------\n");
                Console.WriteLine("\t\t여관");
                Console.WriteLine("\n-------------------------------------------\n");
                Console.WriteLine("500G 지불 시 체력을 회복합니다.\n");

                player.Balance();
                Console.WriteLine("\n\n1. 휴식하기");
                Console.WriteLine("0. 나가기");

                ; //행동 선택
                while (!int.TryParse(Console.ReadLine(), out act))
                {
                    Console.WriteLine("숫자가 아닙니다.");
                    Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
                }
                if(act == 1)
                {
                    Console.WriteLine("\n-------------------");
                    if (player.Gold >= 500)
                    {
                        Console.WriteLine("500G를 지불합니다.");
                        player.Gold -= 500;
                        player.Balance();
                        player.HpMax();
                    }
                }
                else if(act != 0)
                {
                    Console.WriteLine("\n-------------------");
                    Console.WriteLine("해당 번호에 맞는 행동이 없습니다!");
                }
                else
                {
                    Console.WriteLine("\n-------------------");
                    Console.WriteLine("여관에서 나갑니다.");
                }
                Console.ReadKey(true);
            } while (act != 0);
        }
    }
}
