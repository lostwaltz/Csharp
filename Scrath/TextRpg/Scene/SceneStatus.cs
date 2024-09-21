using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Scene
{
    internal class SceneStatus : Scene
    {
        private IPlayerInterface Interface;

        public SceneStatus(IPlayerInterface playerInterface)
        {
            curEnumScene = SCENE.SCENE_STATUS;
            Interface = playerInterface;
            stringBuilder.Append("0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>> ");
        }

        public override void SceneUpdate()
        {
            if (GameManager.instance == null)
                return;

            Console.WriteLine(Interface.GetPlaterStatusText());
            Console.Write(stringBuilder);

            int.TryParse(Console.ReadLine(), out selectcNumber);

            if(selectcNumber == 0)
            {
                if(null != SceneManager.instance)
                    SceneManager.instance.SceneChange(SCENE.SCENE_LOBY);
            }
            else
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }
        }
    }
}
