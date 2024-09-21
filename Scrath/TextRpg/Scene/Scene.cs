using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum SCENE { SCENE_LOBY, SCENE_STATUS, SCENE_INVEN, SCENE_SHOP, SCENE_DUNGEON, SCENE_END }


namespace TextRpg.Scene
{
    internal abstract class Scene
    {
        protected int selectcNumber = -1;
         
        protected StringBuilder stringBuilder = new StringBuilder();
        protected void ClearAndPushText(string text)
        {
            stringBuilder.Clear();
            stringBuilder.Append(text);
        }

        public SCENE curEnumScene { get; set; }

        abstract public void SceneUpdate();
    }
}
