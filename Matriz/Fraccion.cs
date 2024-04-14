using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Fraccion
    {
        private int numerador, denominador;

        public Fraccion(int numerador, int denominador)
        {
            if(denominador==0)
            {
                throw new ArithmeticException("El denominador no puede ser 0");
            }
            else if (numerador == 0)
            {
                this.numerador = 0;
                this.denominador = 1;
            }
            else if(denominador<0)
            {
                this.numerador = -numerador;
                this.denominador = -denominador;
            }
            else
            {
                this.numerador = numerador;
                this.denominador = denominador;
            }
        }

        public int Numerador { get => numerador; set => numerador = value; }
        public int Denominador { get => denominador; set => denominador = value; }

        public static Fraccion operator +(Fraccion a, Fraccion b)
        {
            int mcm = Math.Max(a.denominador, b.denominador);
            while (!(mcm % a.denominador == 0 && mcm % b.denominador == 0))
                ++mcm;
            int num = mcm / a.denominador * a.numerador + mcm / b.denominador * b.numerador, den = mcm;
            mcm = MCD(num, den);
            return new Fraccion(num / mcm, den / mcm);
        }

        public static Fraccion operator -(Fraccion a, Fraccion b)
        {
            int mcm = Math.Max(a.denominador, b.denominador);
            while (!(mcm % a.denominador == 0 && mcm % b.denominador == 0))
                ++mcm;
            int num = mcm / a.denominador * a.numerador - mcm / b.denominador * b.numerador, den = mcm;
            mcm = MCD(num, den);
            Fraccion diferencia = new Fraccion(num / mcm, den / mcm);
            return diferencia;
        }
        public bool Comparar(Fraccion fraccion)
        {
            return numerador==fraccion.numerador&&denominador==fraccion.denominador;
        }

        public static Fraccion operator *(Fraccion a, Fraccion b)
        {
            int mcd = MCD(a.numerador, b.denominador), mcd1 = MCD(a.denominador, b.numerador);
            return new Fraccion(a.numerador / mcd * (b.numerador / mcd1), a.denominador / mcd1 * (b.denominador / mcd));
        }

        public static Fraccion operator /(Fraccion a, Fraccion b )
        {
            int mcd = MCD(a.numerador, b.numerador), mcd1 = MCD(a.denominador, b.denominador);
            return new Fraccion(a.numerador / mcd * (b.denominador / mcd1), a.denominador / mcd1 * (b.numerador / mcd));
        }
        private static int MCD(int a, int b)
        {
            int mcd = 1;
            for (int i = 2; i <= Math.Abs(a) && i <= Math.Abs(b); i++)
                if (a % i == 0 && b % i == 0)
                    mcd = i;
            return mcd;
        }
        public override string ToString() => denominador == 1 ? numerador.ToString() : $"{numerador}/{denominador}";
    }
}
