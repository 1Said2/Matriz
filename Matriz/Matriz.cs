using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriz
{
    internal class Matriz
    {
        public static int paso;
        public static int [,] RellenarMatriz(int m)
        {
            int[,] matriz = new int[m, m];
            int posicion_filas, posicion_columas;
            Console.WriteLine("Rellene la matriz de arriba hacia abajo");
            posicion_filas = Console.CursorTop;
            posicion_columas = 0;
            for (int j = 0; j < m; j++)
            {
                Console.SetCursorPosition(posicion_columas, posicion_filas);
                for (int i = 0; i < m; i++)
                {
                    Console.SetCursorPosition(posicion_columas, Console.CursorTop);
                    matriz[i,j]=int.Parse(Console.ReadLine());
                }
                posicion_columas += Enumerable.Range(0, m).Select(i => matriz[i, j]).ToArray().OrderByDescending(x => x.ToString().Length).First().ToString().Length+3;
            }
            return matriz;
        }
        public static int Determinante(int[,] matriz)
        {
            int m, determinante;
            m = matriz.GetLength(0);
            if (m!=matriz.GetLength(1))
            {
                throw new ArithmeticException("No es una matriz cuadrada");
            }
            if(m==1)
            {
                return matriz[0, 0];
            }
            determinante = 0;
            for(int j=0;j<m;j++)
            {
                determinante += matriz[0, j] * Cofactor(matriz,0,j);
            }
            return determinante;
        }
        private static int Cofactor(int[,] matriz, int fila, int columna)
        {
            return (int)Math.Pow(-1, fila+columna) * Determinante(Submatriz(matriz, fila, columna));
        }
        public static int[,] Submatriz(int[,] matriz,int fila_eliminar, int columna_eliminar)
        {
            int m,x,y;
            m=matriz.GetLength(0);
            int[,] submatriz;
            submatriz=new int[m-1,m-1];
            x = 0;
            for(int i=0;i<m;i++)
            {
                y = 0;
                for(int j=0;j<m&&i!=fila_eliminar; j++)
                {
                    if(j!=columna_eliminar)
                    {
                        submatriz[x,y++] = matriz[i,j];
                    }
                }
                if(y!=0)
                {
                    x++;
                }
            }
            return submatriz;
        }
        public static Fraccion[,] MatrizAmpliada(int[,] matriz)
        {
            Fraccion [,] matriz_ampliada;
            int m;
            m = matriz.GetLength(0);
            matriz_ampliada = new Fraccion[m,m*2];
            for(int i=0;i<m;i++)
            {
                for(int j=0;j<m;j++)
                {
                    matriz_ampliada[i, j] = new Fraccion(matriz[i, j], 1);
                    matriz_ampliada[i, j+m] = new Fraccion(0, 1);

                }
                matriz_ampliada[i, i + m] = new Fraccion(1, 1);
            }
            return matriz_ampliada;
        }
        public static Fraccion[,] Diagonal(Fraccion[,] matriz, int fila, int columna)
        {
            Fraccion uno = new Fraccion(1, 1);
            for (int j = matriz.GetLength(1) - 1; !matriz[fila, columna].Comparar(uno); j--)
                matriz[fila, j] = matriz[fila, j] / matriz[fila, columna];
            Imprimir(matriz);
            return TInferior(matriz, fila + 1, columna);
        }
        private static Fraccion[,] TInferior(Fraccion[,] matriz, int fila, int columna)
        {
            if (fila == matriz.GetLength(0))
                return TSuperior(matriz, matriz.GetLength(0) - 2, matriz.GetLength(0) - 1);
            else
            {
                Fraccion cero = new Fraccion(0, 1);
                for (int i = fila; i < matriz.GetLength(0); i++)
                {
                    for (int j = matriz.GetLength(1) - 1; !matriz[i, columna].Comparar(cero); j--)
                        matriz[i, j] = matriz[i, j] - matriz[i, columna] * matriz[fila - 1, j];
                    Imprimir(matriz);
                }
                return Diagonal(matriz, fila, columna + 1);
            }
        }
        private static Fraccion[,] TSuperior(Fraccion[,] matriz, int fila, int columna)
        {
            if (columna == -1)
                return matriz;
            else
            {
                Fraccion cero = new Fraccion(0, 1);
                for (int i = fila; i >= 0; i--)
                {
                    for (int j = matriz.GetLength(1) - 1; !matriz[i, columna].Comparar(cero); j--)
                        matriz[i, j] = matriz[i, j] - matriz[i, columna] * matriz[fila + 1, j];
                    Imprimir(matriz);
                }
                return TSuperior(matriz, fila - 1, columna - 1);
            }
        }
        private static void Imprimir(Fraccion[,] p)
        {
            Console.WriteLine("Paso " + paso++);
            for (int u = 0; u < p.GetLength(0); u++)
                for (int v = 0; v < p.GetLength(1); v++)
                    Console.Write(v < p.GetLength(1) - 1 ? p[u, v] + "\t" : p[u, v] + "\n");
        }
    }
}
