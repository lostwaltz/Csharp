using System;
using System.Xml.Linq;

namespace Scrath_00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //이름과 나이를 입력 받고 출력하는 코드를 작성하세요

            //string stringInfo = Console.ReadLine();

            //string[] words = stringInfo.Split(' ');


            //Console.WriteLine("이름:{0}, 나이:{1}", words[0], words[1]);

            //----------------------------------------------------------------------------------

            //string stringNumber = Console.ReadLine();

            //string[] stringNumbers = stringNumber.Split(' ');
            //int[] numberArray = { int.Parse(stringNumbers[0]), int.Parse(stringNumbers[1]) };

            //Console.WriteLine("{0} + {1} = {2}", numberArray[0], numberArray[1], numberArray[0] + numberArray[1]);
            //Console.WriteLine("{0} - {1} = {2}", numberArray[0], numberArray[1], numberArray[0] - numberArray[1]);
            //Console.WriteLine("{0} * {1} = {2}", numberArray[0], numberArray[1], numberArray[0] * numberArray[1]);
            //Console.WriteLine("{0} / {1} = {2}", numberArray[0], numberArray[1], numberArray[0] / numberArray[1]);

            //----------------------------------------------------------------------------------

            //float celcius = float.Parse(Console.ReadLine());

            //float fahrenhelt = ((9f / 5f) * celcius) + 32f;

            //Console.WriteLine("섭씨:{0}, 화씨:{1}", celcius, fahrenhelt);

            //----------------------------------------------------------------------------------

            //string stringNumber = Console.ReadLine();

            //string[] stringNumbers = stringNumber.Split(' ');

            //float[] numberArray = { float.Parse(stringNumbers[0]), float.Parse(stringNumbers[1]) };
            //numberArray[0] = numberArray[0] / 100f;

            //Console.WriteLine("키:{0}, 몸무게:{1}, BMI:{2}", numberArray[0], numberArray[1], numberArray[1] / (numberArray[0] * numberArray[0]));

            //----------------------------------------------------------------------------------

            //Console.WriteLine("숫자 맞추기 게임을 시작합니다. 1에서 100까지의 숫자 중 하나를 맞춰보세요.\n");
            //int randNumber = new Random().Next(1, 101);


            //int selectNumber = 0;
            //int count = 0;

            //while(randNumber != selectNumber)
            //{
            //    count++;
            //    selectNumber = int.Parse(Console.ReadLine());

            //    Console.WriteLine("숫자를 입력하세요: ");

            //    if (selectNumber < randNumber)
            //    {
            //        Console.WriteLine("너무 작습니다!");
            //    }
            //    else if (selectNumber > randNumber)
            //    {
            //        Console.WriteLine("너무 큽니다!");
            //    }

            //}
            //Console.WriteLine("축하합니다 {0}번 만에 숫자를 맞추었습니다.", count);

            //----------------------------------------------------------------------------------

            //bool playerTurn = false;
            //int[,] tictactoeMap = new int[3,3];
            //int postionOffsetX = -4;
            //int postionOffsetY = 5;

            //while (true)
            //{
            //    Console.Clear();
            //    DrawTicTacToeMap();

            //    if (true == CheckWin())
            //        return;

            //    int selectIndex = int.Parse(Console.ReadLine()) - 1;
            //    playerTurn = !playerTurn;

            //    if (selectIndex < 0 && selectIndex <= 9)
            //        continue;

            //    tictactoeMap[selectIndex / 3, selectIndex % 3] = Convert.ToInt32(playerTurn) + 1;
            //}

            //void DrawTicTacToeMap()
            //{
            //    Console.WriteLine("플레이어 1: X 와 플레이어 2: O");
            //    Console.WriteLine("플레이어 {0}의 차례", Convert.ToInt32(playerTurn) + 1);

            //    Console.SetCursorPosition(0, 1 + postionOffsetY);
            //    Console.Write("_________________");
            //    Console.SetCursorPosition(0, 4 + postionOffsetY);
            //    Console.Write("_________________");

            //    for (int i = 0; i < tictactoeMap.Length; i++)
            //    {
            //        Console.SetCursorPosition(postionOffsetX + (6 * ((i % 3) + 1)), postionOffsetY + (3 * (i / 3))); // 2 5 8

            //        char indexChar = ConvertIndextoASCII(i, tictactoeMap[i / 3, i % 3]);
            //        Console.Write("{0}", indexChar);

            //        Console.SetCursorPosition(5, i + postionOffsetY - 1);
            //        Console.Write('|');
            //        Console.SetCursorPosition(11, i + postionOffsetY - 1);
            //        Console.Write('|');
            //    }

            //    Console.SetCursorPosition(0, 15);
            //};
            //char ConvertIndextoASCII(int index, int arrayValue)
            //{
            //    if (arrayValue == 1)
            //        return 'X';
            //    else if (arrayValue == 2)
            //        return 'O';

            //    return Convert.ToChar(index + 49);
            //}
            //bool CheckWin()
            //{
            //    for(int i = 0; i < 3; i++)
            //    {
            //        if ((tictactoeMap[i, 0] == tictactoeMap[i, 1] && tictactoeMap[i, 1] == tictactoeMap[i, 2]) && 0 != tictactoeMap[i, 0])
            //        {
            //            Console.WriteLine("플레이어 {0} 승리!!", tictactoeMap[i, 0]);
            //            return true;
            //        }
            //    }
            //    for(int i = 0; i < 3; i++)
            //    {
            //        if ((tictactoeMap[0, i] == tictactoeMap[1, i] && tictactoeMap[1, i] == tictactoeMap[2, i]) && 0 != tictactoeMap[0, i])
            //        {
            //            Console.WriteLine("플레이어 {0} 승리!!", tictactoeMap[0, i]);
            //            return true;
            //        }
            //    }
            //    if ((tictactoeMap[0, 0] == tictactoeMap[1, 1] && tictactoeMap[1, 1] == tictactoeMap[2, 2]) && 0 != tictactoeMap[0, 0])
            //    {
            //        Console.WriteLine("플레이어 {0} 승리!!", tictactoeMap[0, 0]);
            //        return true;
            //    }
            //    else if((tictactoeMap[0, 2] == tictactoeMap[1, 2] && tictactoeMap[1, 2] == tictactoeMap[2, 2]) && 0 != tictactoeMap[0, 2])
            //    {
            //        Console.WriteLine("플레이어 {0} 승리!!", tictactoeMap[0, 2]);
            //        return true;
            //    }
            //    return false;
            //}

            //----------------------------------------------------------------------------------
        }
    }
}
