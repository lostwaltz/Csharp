using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Scene
{
    internal class SceneLoby : Scene
    {
        public SceneLoby()
        {
            curEnumScene = SCENE.SCENE_LOBY;
            stringBuilder.Append("스파르타 마을에 오신 여러분 환영합니다.\r\n" +
                "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\r\n\r\n" +
                "1. 상태 보기\r\n" +
                "2. 인벤토리\r\n" +
                "3. 상점\r\n" +
                "4. 던전입장\r\n" +
                "5. 휴식하기\n" +
                "6. 저장하기\n\n원하시는 행동을 입력해주세요." +
                "\r\n>> ");
        }

        public override void SceneUpdate()
        {
            Console.Write(stringBuilder);

            int.TryParse(Console.ReadLine(), out selectcNumber);

            if (1 > selectcNumber || (int)SCENE.SCENE_END < selectcNumber)
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }

            if (SceneManager.instance == null)
                return;

            if (SceneManager.instance == null)
                return;

                switch (selectcNumber)
            {
                case 1:
                    SceneManager.instance.SceneChange(SCENE.SCENE_STATUS);
                    break;
                case 2:
                    SceneManager.instance.SceneChange(SCENE.SCENE_INVEN);
                    break;
                case 3:
                    SceneManager.instance.SceneChange(SCENE.SCENE_SHOP);
                    break;
                case 4:
                    SceneManager.instance.SceneChange(SCENE.SCENE_DUNGEON);
                    break;
                case 5:
                    SceneManager.instance.SceneChange(SCENE.SCENE_REST);
                    break;
                case 6:
                    if (GameManager.instance == null)
                        return;
                    GameManager.instance.playerInterface.SavePlayerData();
                    break;
            }
        }
    }
}
