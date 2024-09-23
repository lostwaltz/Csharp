using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Scene;

namespace TextRpg
{
    class SceneManager
    {
        Scene.Scene[] sceneList = new Scene.Scene[(int)SCENE.SCENE_END];

        public static SceneManager? instance;

        public delegate void sceneChageCallback();
        private event sceneChageCallback? sceneChangeCallbackEvent;

        private Scene.Scene curScene = new SceneLoby();

        public Scene.Scene CurScene
            { get { return curScene; } private set { } }

        public static void InitSceneManager()
        {
            if (instance == null)
            {
                instance = new SceneManager();

                if (GameManager.instance == null)
                    return;

                instance.sceneList[(int)SCENE.SCENE_LOBY] = new SceneLoby();
                instance.sceneList[(int)SCENE.SCENE_STATUS] = new SceneStatus(GameManager.instance.playerInterface);
                instance.sceneList[(int)SCENE.SCENE_INVEN] = new SceneInven(GameManager.instance.playerInterface);
                instance.sceneList[(int)SCENE.SCENE_SHOP] = new SceneShop(GameManager.instance.playerInterface);
                instance.sceneList[(int)SCENE.SCENE_DUNGEON] = new SceneDungeon(GameManager.instance.playerInterface);
                instance.sceneList[(int)SCENE.SCENE_REST] = new SceneRest(GameManager.instance.playerInterface);
            }
        }

        public void SetSceneChangedCallback(sceneChageCallback callback)
        {
            sceneChangeCallbackEvent += callback;
        }

        public void SceneChange(SCENE sceneType)
        {
            curScene = sceneList[(int)sceneType];
            sceneChangeCallbackEvent?.Invoke();
        }
    }
}
