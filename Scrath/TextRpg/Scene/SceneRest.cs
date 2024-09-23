
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Scene
{
    internal class SceneRest : Scene
    {
        private IPlayerInterface Interface;
        int restGold = 500;
        int helingHelthPoin = 100;

        public SceneRest(IPlayerInterface playerInterface)
        {
            curEnumScene = SCENE.SCENE_DUNGEON;
            Interface = playerInterface;

            stringBuilder.Append("휴식하기\n");
            stringBuilder.Append("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : ");
            
        }

        public override void SceneUpdate()
        {
            Console.Write(stringBuilder);

            Console.Write("{0})\n\n", Interface.GetPlayerGold());

            Console.Write("1. 휴식하기\n0. 나가기\n\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            int.TryParse(Console.ReadLine(), out selectcNumber);
            int playerGold = Interface.GetPlayerGold();

            if (selectcNumber < 0 || selectcNumber > 1)
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }
            else if(selectcNumber == 1)
            {
                if(playerGold >= restGold)
                {
                    Interface.AddPlayerGold(-restGold);
                    Interface.AddPlayerHelth(helingHelthPoin);

                    Console.Write("휴식을 완료했습니다.");
                }
                else
                {
                    Console.Write("Gold 가 부족합니다");
                }

                Thread.Sleep(1000);
            }
            else
            {
                if (null == SceneManager.instance)
                    return;

                SceneManager.instance.SceneChange(SCENE.SCENE_LOBY);
            }


        }
    }
}
