using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    internal class GameManager
    {
        public GameManager()
        {
            playerInterface = player;
        }

        private Player player = new Player(new Inven(new TextRpg.Component.ItemList()));

        public IPlayerInterface playerInterface {  get; private set; }

        public static GameManager? instance;

        public static void InitGameManager()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
        }
    }
}
