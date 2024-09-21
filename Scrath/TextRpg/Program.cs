using System.Runtime.CompilerServices;
using TextRpg.Scene;



namespace TextRpg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitTextRpg();

            UpdateGame();
        }

        private static void InitTextRpg()
        {
            GameManager.InitGameManager();
            SceneManager.InitSceneManager();

            if (SceneManager.instance == null && GameManager.instance == null)
                return;

        }

        private static void UpdateGame()
        {
            if (SceneManager.instance == null)
                return;

            bool runGame = true;
            while(runGame)
            {
                Console.Clear();

                SceneManager.instance.CurScene.SceneUpdate();

            }
        }


    }


}

