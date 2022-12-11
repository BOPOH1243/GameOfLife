using System;
using System.Collections.Generic;
using System.Text;

namespace tlg
{
    public class InfiniteArray<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public T[,] items;

        public InfiniteArray(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            items = new T[width, height];
        }

        public T this[int x, int y]
        {
            get
            {
                while (x < 0) x += Width;
                if (x >= Width) x = x % Width;

                while (y < 0) y += Height;
                if (y >= Height) y = y % Height;

                return items[x, y];
            }

            set
            {
                if (x < 0 || x >= Width) return;
                if (y < 0 || y >= Height) return;
                items[x, y] = value;
            }
        }
    }
    class GameOfLife
    {
        public InfiniteArray<bool> game_field;
        readonly Random rand = new Random();
        public GameOfLife(int width, int height, double liveDensity)
        {
            game_field = new InfiniteArray<bool>(width,height);
            for(int _width=0; _width < game_field.Width; _width++)
            {
                for(int _height=0; _height<game_field.Height; _height++)
                {
                    game_field[_width, _height] = rand.NextDouble() < liveDensity;
                }
            }
        }
        public bool[,] get_game_field()
        {
            return game_field.items;
        }
        public bool get_next_state(int width, int height)
        {
            var neighbours = new bool[8] 
            {
                game_field[width,height+1],
                game_field[width,height-1],
                game_field[width+1,height],
                game_field[width+1,height+1],
                game_field[width+1,height-1],
                game_field[width-1,height+1],
                game_field[width-1,height],
                game_field[width-1,height-1],
            };
            int count_of_alive_neighbours = 0;
            foreach(bool cell in neighbours)
            {
                if (cell)
                    count_of_alive_neighbours++;
            }
            if (game_field[width, height])
                return count_of_alive_neighbours == 2 || count_of_alive_neighbours == 3;
            else
                return count_of_alive_neighbours == 3;
        }
        public void Next()
        {
            var new_game_field = new InfiniteArray<bool>(game_field.Width, game_field.Height);
            for(int width = 0; width < new_game_field.Width; width++)
            {
                for(int height = 0; height < new_game_field.Height; height++)
                {
                    new_game_field[width, height] = get_next_state(width, height);
                }
            }
            game_field = new_game_field;
            return;
        }
    }
}
