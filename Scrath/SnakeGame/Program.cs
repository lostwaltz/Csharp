using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 뱀의 초기 위치와 방향을 설정하고, 그립니다.
        Point p = new Point(10, 5, '*');
        Snake snake = new Snake(p, 5, Direction.DOWN);

        snake.Draw();
        
        // 음식의 위치를 무작위로 생성하고, 그립니다.
        FoodCreator foodCreator = new FoodCreator(10, 2, '$');
        foodCreator.Draw();

        int createFoodTimer = 0;
        // 게임 루프: 이 루프는 게임이 끝날 때까지 계속 실행됩니다.
        while (true)
        {
            if(createFoodTimer % 10  == 0)
            {
                foodCreator.CreateFood();
            }

            // 키 입력이 있는 경우에만 방향을 변경합니다.
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        snake.m_direction = Direction.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        snake.m_direction = Direction.DOWN;
                        break;
                    case ConsoleKey.LeftArrow:
                        snake.m_direction = Direction.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        snake.m_direction = Direction.RIGHT;
                        break;
                }
            }
            // 뱀이 이동하고, 음식을 먹었는지, 벽이나 자신의 몸에 부딪혔는지 등을 확인하고 처리하는 로직을 작성하세요.
            Point preTailPoint = snake.snakePointList[snake.snakePointList.Count - 1];

            snake.Move();
            Point snakeHeadPoint = snake.snakePointList[0];
            int cnt = 0;
            foreach(Point snakePoint in snake.snakePointList)
            {
                if(snakeHeadPoint.x == snakePoint.x && snakeHeadPoint.y == snakePoint.y && cnt != 0)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("몸 부딪힘");

                    Console.ReadKey();
                }
                cnt++;
            }
            if(foodCreator.foodPointList.Count != 0)
            {
                foreach (Point foodPoint in foodCreator.foodPointList)
                {
                    if (snakeHeadPoint.x == foodPoint.x && snakeHeadPoint.y == foodPoint.y)
                    {
                        snake.snakePointList.Add(new Point(preTailPoint.x, preTailPoint.y, '*'));
                        foodCreator.foodPointList.Remove(foodPoint);
                        snake.eatFoodCount++;
                        break;
                    }
                }
            }

            // 이동, 음식 먹기, 충돌 처리 등의 로직을 완성하세요.            
            Console.Clear();
            snake.Draw();
            foodCreator.Draw();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("현재 길이 {0} 먹은 음식 수 {1}", snake.snakePointList.Count(), snake.eatFoodCount);
            createFoodTimer++;
            Thread.Sleep(200); // 게임 속도 조절 (이 값을 변경하면 게임의 속도가 바뀝니다)

            // 뱀의 상태를 출력합니다 (예: 현재 길이, 먹은 음식의 수 등)
        }
    }
}
public class Snake
{
    public int eatFoodCount = 0;

    public Direction m_direction { get; set; }

    public List<Point> snakePointList = new List<Point>();

    public Snake(Point p, int length, Direction direction)
    {
        m_direction = direction;
        for (int i = 0;  i < length; i++)
        {
            switch(direction)
            {
                case Direction.LEFT:
                    snakePointList.Add(new Point(p.x + i, p.y, '*'));
                    break;
                case Direction.RIGHT:
                    snakePointList.Add(new Point(p.x - i, p.y, '*'));
                    break;
                case Direction.UP:
                    snakePointList.Add(new Point(p.x, p.y +i, '*'));
                    break;
                case Direction.DOWN:
                    snakePointList.Add(new Point(p.x, p.y - i, '*'));
                    break;
            }
        }
    }
    public void Move()
    {
        for (int i = snakePointList.Count - 1; i >= 1; i--)
        {
            snakePointList[i].x = snakePointList[i - 1].x;
            snakePointList[i].y = snakePointList[i - 1].y;
        }
        switch (m_direction)
        {
            case Direction.LEFT:
                snakePointList[0].x = snakePointList[0].x - 1;
                break;
            case Direction.RIGHT:
                snakePointList[0].x = snakePointList[0].x + 1;
                break;
            case Direction.UP:
                snakePointList[0].y = snakePointList[0].y - 1;
                break;
            case Direction.DOWN:
                snakePointList[0].y = snakePointList[0].y + 1;
                break;
        }
    }
    public void Draw()
    {
        foreach(Point p in snakePointList)
        {
            p.Draw();
        }
    }
}
public class FoodCreator
{
    public List<Point> foodPointList = new List<Point>();
    char m_sym;

    public FoodCreator(int x, int y, char sym)
    {
        m_sym = sym;
        foodPointList.Add(new Point(x, y, m_sym));
    }

    public Point CreateFood()
    {
        int x = new Random().Next(10, 80);
        int y = new Random().Next(1, 20);

        foodPointList.Add(new Point(x, y, m_sym));

        return foodPointList[foodPointList.Count - 1];
    }

    public void Draw()
    {
        foreach (Point p in foodPointList)
        {
            p.Draw();
        }
    }

}

public class Point
{
    public int x { get; set; }
    public int y { get; set; }
    public char sym { get; set; }

    // Point 클래스 생성자
    public Point(int _x, int _y, char _sym)
    {
        x = _x;
        y = _y;
        sym = _sym;
    }

    // 점을 그리는 메서드
    public void Draw()
    {
        Console.SetCursorPosition(x, y);
        Console.Write(sym);
    }

    // 점을 지우는 메서드
    public void Clear()
    {
        sym = ' ';
        Draw();
    }

    // 두 점이 같은지 비교하는 메서드
    public bool IsHit(Point p)
    {
        return p.x == x && p.y == y;
    }
}
// 방향을 표현하는 열거형입니다.
public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}