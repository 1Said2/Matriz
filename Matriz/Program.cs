using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriz
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            int[,] matriz;
            int m, determinante;
            Console.WriteLine("Tamaño de la matriz");
            m=int.Parse(Console.ReadLine());
            matriz = Matriz.RellenarMatriz(m);
            determinante = Matriz.Determinante(matriz);
            Console.WriteLine($"La determinante de la matriz es {determinante}");
           if (determinante == 0)
            {
                Console.WriteLine("La matriz no tiene inversa");
            }
            else
            {
                Matriz.paso = 1;
                Console.WriteLine("La matriz inversa es:");
                Matriz.Diagonal(Matriz.MatrizAmpliada(matriz),0,0);
            }
            Console.WriteLine("Ingrese 1 para agregar otra matriz o cualquier otro numero para salir");
            m= int.Parse(Console.ReadLine());
            if(m == 1)
            {
                Main();
            }
        }
    }
}
