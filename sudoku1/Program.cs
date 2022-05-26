using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace sudoku1
{
    class SudokuGame
    {
        static int[,] grid = new int[9, 9];
        static bool[,] zeroGrid = new bool[9, 9];
        static string s;
        static Random ran = new Random();
        static void Generator()
        {
            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = ((i * 3 + i / 3 + j) % 9 + 1);  //Базовое заполнение
                }
            }
        }
        static void Transponing()                          //Транспонирование
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int tmp = grid[i, j];
                    grid[i, j] = grid[j, i];
                    grid[j, i] = tmp;
                }
            }
        }

        static void SwapRows()         //Замена рядов
        {
            Random ran = new Random();
            int first = ran.Next(1, 3);
            int second = ran.Next(1, 3);
            int third = ran.Next(4, 6);
            int fourth = ran.Next(4, 6);
            int fifth = ran.Next(7, 9);
            int sixth = ran.Next(7, 9);
            for (int k = 0; k < 9; k++)
                {
                    int tmp = grid[first - 1, k];
                    grid[first - 1, k] = grid[second - 1, k];
                    grid[second - 1, k] = tmp;
                }
            for (int k = 0; k < 9; k++)
            {
                int tmp = grid[third - 1, k];
                grid[third - 1, k] = grid[fourth - 1, k];
                grid[fourth - 1, k] = tmp;
            }
            for (int k = 0; k < 9; k++)
            {
                int tmp = grid[fifth - 1, k];
                grid[fifth - 1, k] = grid[sixth - 1, k];
                grid[sixth - 1, k] = tmp;
            }

        }
        static void SwapColumns() //Замена столбцов
        {
            Transponing();
            SwapRows();
            Transponing();           
        }
        static void Delete (int x)           //Удаление задач
        {
            switch (x)
            {
                case 1:
                    int colToVanish1 = ran.Next(46, 51);
                    for(int k = 0; k < colToVanish1; k++)
                    {
                        int i = ran.Next() % 9;
                        int j = ran.Next() % 9;
                        if (!zeroGrid[i,j])
                        {
                            grid[i, j] = -1;
                            zeroGrid[i, j] = !zeroGrid[i,j];
                        }
                        else {k--;}                      
                    }
                    Console.WriteLine("Easy");
                    break;
                case 2:
                    int colToVanish2 = ran.Next(51, 56);
                    for (int k = 0; k < colToVanish2; k++)
                    {
                        int i = ran.Next() % 9;
                        int j = ran.Next() % 9;
                        if (!zeroGrid[i, j])       //Проверка на то, убирали уже эту цифру или нет
                        {
                            grid[i, j] = -1;
                            zeroGrid[i, j] = !zeroGrid[i, j];
                        }
                        else { k--; }                      
                    }
                    Console.WriteLine("Medium");
                    break;
                case 3:
                    int colToVanish3 = ran.Next(56,61);
                    for (int k = 0; k < colToVanish3; k++)
                    {
                        int i = ran.Next() % 9;
                        int j = ran.Next() % 9;
                        if (!zeroGrid[i, j])
                        {
                            grid[i, j] = -1;
                            zeroGrid[i, j] = !zeroGrid[i, j];
                        }
                        else { k--; }                    
                    }
                    Console.WriteLine("Hard");
                    break;
            }
                
        }

        static void Draw(ref int[,] grid )    //Отрисовка
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (grid[x, y] == -1) { s += ("  "); }
                    else { s += (grid[x, y] + " "); }
                    if (y == 2 || y == 5) { s += ("| "); }        
                }
                s += ('\n');
                if (x == 2 || x == 5) { s += ("------+-------+------\n"); }
            }
            Console.WriteLine(s);
                      
        }
           
        static void Main(string[] args)
        {
            s = "";
            string s2;          
            Generator();
            for (int i = 0; i < 1000; i++)
            {
                Transponing();
                SwapRows();
                SwapColumns();
            }
            Console.Write("Выберите сложность(1-3): ");
           try
            {
                int diff = Int32.Parse(Console.ReadLine());               
                Delete(diff);
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Draw(ref grid);
            Console.ReadKey();
        }
    }
}
