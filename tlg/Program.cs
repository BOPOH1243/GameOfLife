using System;

namespace tlg
{
    class Program
    {
        static void Main(string[] args)
        {
            char alive_char = '+';
            char non_alive_char = ' ';
            int height = 160;
            int width = 600;
            double density = 0.5;
            var gol = new GameOfLife(width, height,density);
            Console.WriteLine("готово");
            Console.ReadLine();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            while (true)
            {
                var game_field = gol.get_game_field();
                for(int _height = 0; _height < height; _height++)
                {
                    var str = new char[width];
                    for(int _width = 0; _width < width; _width++)
                    {
                        if (game_field[_width, _height])
                            str[_width] = alive_char;
                        else
                            str[_width] = non_alive_char;
                    }
                    Console.WriteLine(str);
                }
                Console.SetCursorPosition(0, 0);
                gol.Next();
            }
        }
    }
}
