using System;
using System.Linq;
using System.Globalization;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] dados;
            double[] valores, xi, fi, fac, fri, frac, xifi, K, li, Li, pm, pmfi;
            double range, maior, menor, anterior, media, moda, mediana, soma, classe, h, delta1, delta2, ordem;
            int n, qtde;

            n = 0;

            Console.Write("Informe os dados: ");
           
            dados = Console.ReadLine().Split(' ');

            foreach(string elemento in dados)
            {
                n++;
            }

            valores = new double[n];
            xi = new double[n];
            fi = new double[n];

            for(int i = 0; i < n; i++)
            {
                valores[i] = double.Parse(dados[i]);
            }

            Array.Sort(valores);
            maior = valores.Max();
            menor = valores.Min();

            range = maior - menor;

            Console.WriteLine();
            Console.Write("rol = ");

            for(int i = 0; i < n; i++)
            {
                Console.Write(valores[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("amplitude = " + range);
            Console.WriteLine("maior = " + maior);
            Console.WriteLine("menor = " + menor);
            Console.WriteLine("quantidade = " + n);

            anterior = -1;
            qtde = 0;

            for(int i = 0; i < n; i++)
            {
                if (valores[i] == anterior)
                {
                    fi[qtde - 1]++;
                }
                else
                {
                    qtde++;
                    if (i == 0)
                    {
                        xi[i] = valores[i];
                        fi[i] = 1;
                    }
                    else
                    {
                        xi[qtde - 1] = valores[i];
                        fi[qtde - 1] = 1;
                    }
                }

                anterior = valores[i];
            }

            fac = new double[n];
            fri = new double[n];
            frac = new double[n];
            xifi = new double[n];

            for(int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    fac[i] += fi[i];
                }
                else
                {
                    fac[i] = fi[i] + fac[i - 1];
                }
            }

            for(int i = 0; i < n; i++)
            {
                fri[i] = fi[i] / fac[qtde - 1] * 100.0;
            }

            for(int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    frac[i] += fri[i];
                }
                else
                {
                    frac[i] = fri[i] + frac[i - 1];
                }
            }

            soma = 0.0;

            for(int i = 0; i < n; i++)
            {
                xifi[i] = xi[i] * fi[i];
                soma += xifi[i];
            }

            moda = 0.0;

            for(int i = 0; i < n; i++)
            {
                if (fi[i] == fi.Max())
                {
                    moda = xi[i];
                }
            }

            Console.WriteLine();
            Console.WriteLine("Tabela sem intervalo de classes: ");
            Console.WriteLine();

            
            Console.WriteLine("    xi    |    fi    |    fac    |   fri   |   frac   |   xi * fi  ");

            for(int i = 0; i < qtde; i++)
            {
                Console.Write("    " + xi[i]);
                Console.Write("         " + fi[i]);
                Console.Write("          " + fac[i]);
                Console.Write("          " + fri[i].ToString("F2", CultureInfo.InvariantCulture) + "%");
                Console.Write("     " + frac[i].ToString("F2", CultureInfo.InvariantCulture) + "%");
                Console.Write("      " + xifi[i]);
                Console.WriteLine();
            }


            media = soma / n;
            mediana = (n + 1) / 2.0;

            Console.WriteLine("media = " + media.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("moda = " + moda.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("mediana = " + mediana.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine();

            Console.WriteLine("Tabela com intervalo de classes: ");

            
            classe = Math.Sqrt(n);

            classe = Math.Ceiling(classe);

            int t = (int) classe;

            K = new double[t];

            for(int i = 0; i < t; i++)
            {
                K[i] = i + 1.0;
            }

            h = range / classe;

            h = Math.Ceiling(h);

            li = new double[n];
            Li = new double[n];

            for(int i = 0; i < t; i++)
            {
                if(i == 0)
                {
                    li[i] = menor;
                }
                else
                {
                    li[i] = Li[i - 1];
                }

                Li[i] = li[i] + h;

            }

            for(int i = 0; i < qtde; i++)
            {
                fi[i] = 0.0;
                fac[i] = 0.0;
                fri[i] = 0.0;
                frac[i] = 0.0;
            }

            for(int i = 0; i < t; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (valores[j] >= li[i] && valores[j] < Li[i])
                    {
                        fi[i] = fi[i] + 1.0;
                    }
                }
            }

            for(int i = 0; i < t; i++)
            {
                if (i == 0)
                {
                    fac[i] = fi[i];
                }
                else
                {
                    fac[i] = fi[i] + fac[i - 1];
                }
            }

            for(int i = 0; i < t; i++)
            {
                fri[i] = fi[i] / n * 100.0;
            }

            for(int i = 0; i < t; i++)
            {
                if(i == 0)
                {
                    frac[i] = fri[i];
                }
                else
                {
                    frac[i] = fri[i] + frac[i - 1];
                }
            }

            pm = new double[t];
            pmfi = new double[t];
            
            for(int i = 0; i < t; i++)
            {
                pm[i] = (li[i] + Li[i]) / 2.0;
            }

            for(int i = 0; i < t; i++)
            {
                pmfi[i] = fi[i] * pm[i];
            }

            Console.WriteLine();
            Console.WriteLine(" classes |         xi         |    fi    |    fac    |    fri    |   frac   |   Pm   |   Pm * fi  ");

            for (int i = 0; i < t; i++)
            {
                Console.Write("    " + K[i] + "       ");
                Console.Write(li[i] + " ");
                Console.Write("| - - - ");
                Console.Write(Li[i] + "          ");
                Console.Write(fi[i].ToString("F0", CultureInfo.InvariantCulture) + "           ");
                Console.Write(fac[i].ToString("F0", CultureInfo.InvariantCulture) + "       ");
                Console.Write(fri[i].ToString("F2", CultureInfo.InvariantCulture) + "%       ");
                Console.Write(frac[i].ToString("F2", CultureInfo.InvariantCulture) + "%     ");
                Console.Write(pm[i].ToString("F2", CultureInfo.InvariantCulture) + "    ");
                Console.Write(pmfi[i].ToString("F2", CultureInfo.InvariantCulture) + "    ");
                Console.WriteLine();
            }

            soma = 0.0;

            for(int i = 0; i < t; i++)
            {
                soma += pmfi[i];
            }

            media = soma / n;

            maior = fi.Max();

            for(int i = 0; i < t; i++)
            {
                if (fi[i] == maior)
                {
                    if (i == 0)
                    {
                        delta1 = maior - 0.0;
                        delta2 = maior - fi[i + 1];
                        moda = li[i] + (delta1 / (delta1 + delta2)) * h;
                    }
                    else if (i + 1 == t)
                    {
                        delta1 = maior - fi[i - 1];
                        delta2 = maior - 0.0;
                        moda = li[i] + (delta1 / (delta1 + delta2)) * h;
                    }
                    else
                    {
                        delta1 = maior - fi[i - 1];
                        delta2 = maior - fi[i + 1];
                        moda = li[i] + (delta1 / (delta1 + delta2)) * h;
                    }
                }
            }

            ordem = n / 2.0;

            for(int i = 0; i < t; i++)
            {
                if(ordem >= li[i] && ordem <= Li[i])
                {
                    if(i == 0)
                    {
                        mediana = li[i] + ((ordem - 0.0) / fi[i]) * h;
                    }
                    else 
                    {
                       mediana = li[i] + ((ordem - fac[i - 1]) / fi[i]) * h;
                    }
                    
                }
            }

            Console.WriteLine();
            Console.WriteLine("média = " + media.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("moda = " + moda.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("mediana = " + mediana.ToString("F2", CultureInfo.InvariantCulture));

        }
    }
}

