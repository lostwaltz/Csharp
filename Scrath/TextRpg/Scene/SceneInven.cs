using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Scene
{
    internal class SceneInven : Scene
    {
        private IPlayerInterface Interface;

        public SceneInven(IPlayerInterface playerInterface)
        {
            curEnumScene = SCENE.SCENE_INVEN;

            stringBuilder.Append("인벤토리\n");
            stringBuilder.Append("보유 중인 아이템을 관리할 수 있습니다.\n");
            Interface = playerInterface;
        }

        public override void SceneUpdate()
        {
            Console.WriteLine(stringBuilder);

            Console.WriteLine(Interface.GetPlayerItemListText(false, false));
            
            Console.WriteLine("1. 장착관리\n0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            int.TryParse(Console.ReadLine(), out selectcNumber);

            if (0 > selectcNumber || 1 < selectcNumber)
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }
            else if (1 == selectcNumber)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(stringBuilder);
                    Console.WriteLine(Interface.GetPlayerItemListText(true, true));

                    Console.Write("0. 나가기\n\n");
                    Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                    int.TryParse(Console.ReadLine(), out selectcNumber);
                    if (0 == selectcNumber)
                        return;

                    Interface.SetEquipItemTogle(selectcNumber - 1);
                }
            }
            else
            {
                if (null != SceneManager.instance)
                    SceneManager.instance.SceneChange(SCENE.SCENE_LOBY);
            }
        }
    }
}

