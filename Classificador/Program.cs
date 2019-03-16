using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador
{
    class Program
    {
        #region [classe Individuo]
        public class Individuo
        {
            private string _classe;
            private double _a, _b, _c, _d;

            //Construtor. Os Doubles variam de acordo com a quantidade de atributos da tabela.
            public Individuo(string classe, double a, double b, double c, double d)
            {
                _a = a;
                _b = b;
                _c = c;
                _d = d;
                _classe = classe;
            }

            public double a {
                get
                {
                    return _a;
                }
                set
                {
                    _a = value;
                }
            }

            public double b
            {
                get
                {
                    return _b;
                }
                set
                {
                    _b = value;
                }
            }

            public double c
            {
                get
                {
                    return _c;
                }
                set
                {
                    _c = value;
                }
            }

            public double d
            {
                get
                {
                    return _d;
                }
                set
                {
                    _d = value;
                }
            }
            public string classe
            {
                get
                {
                    return _classe;
                }
                set
                {
                    _classe = value;
                }
            }
        }
        #endregion
        #region [Functions]

        public static string[] CarregarDataBase()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\databaseBalanca.txt");

            return lines;
        }
        public static List<Individuo> SeparadorDeAtributos(string[] dataBase)
        {
            List<Individuo> individuos = new List<Individuo>();

            foreach (var dados in dataBase)
            {
                string[] colunas = dados.Split(',');
                string classe = colunas[0];
                double a = Convert.ToDouble(colunas[1]), b = Convert.ToDouble(colunas[2]), c = Convert.ToDouble(colunas[3]), d = Convert.ToDouble(colunas[4]);

                Individuo individuo = new Individuo(classe, a, b, c, d);
                individuos.Add(individuo);
            }
            return individuos;
        }
        public static double obterDistanciaEuclidiana(Individuo ind1, Individuo ind2)
        {
            //raiz quadrada da soma das diferenças dos valores dos atributos elevado ao quadrado.
            //Raiz Quadrada: Math.Sqrt(var);
            //Potência: Math.Pow(Var, Elevação);
            double soma = Math.Pow((ind1.a - ind2.a), 2)
                + Math.Pow((ind1.b - ind2.b), 2)
                + Math.Pow((ind1.c - ind2.c), 2)
                + Math.Pow((ind1.d - ind2.d), 2);
            return Math.Sqrt(soma);
        }

        public static string[] ClassificadorDeAmostras(List<Individuo> C1, List<Individuo> C2 , int k)
        {
            if(k%2 == 0)
            {
                k--;
                k = k <= 0 ? 1 : k;
            }

            var tam = C1.Count();
            string[] classesC1 = new string[tam];
            int posicao = 0;
            foreach (var elemento1 in C1)
            {
                var distanciaIndividuos = new List<KeyValuePair<double, Individuo>>();
                foreach (var elemento2 in C2)
                {
                    double distancia = obterDistanciaEuclidiana(elemento1, elemento2);
                    distanciaIndividuos.Add(new KeyValuePair<double, Individuo>(distancia, elemento1));
                }

                int contadorL = 0, contadorR = 0, contadorB = 0, contK = 0;
                foreach (var dist in distanciaIndividuos)
                {
                    string classe = dist.Value.classe;

                    if (classe == "L") //nome dos arquivos
                        contadorL++;
                    if (classe == "R")
                        contadorR++;
                    if (classe == "B")
                        contadorB++;
                    if (contK > k)
                        break;

                    contK++;
                }
                string classeClassificacao;
                if (contadorL >= contadorR && contadorL >= contadorB)
                    classeClassificacao = "L";
                else if (contadorR >= contadorL && contadorR >= contadorB)
                    classeClassificacao = "R";
                else
                    classeClassificacao = "B";
                classesC1[posicao] = classeClassificacao;
                posicao++;
                distanciaIndividuos = null;
            }
            return classesC1;
        }




        #endregion

        static void Main(string[] args)
        {
            List<Individuo> individuos = new List<Individuo>();
            string[] database = CarregarDataBase();
            individuos = SeparadorDeAtributos(database);
            int quantidadeIndividuos = individuos.Count();
            int contador = 1;
            List<Individuo> Z1 = new List<Individuo>();
            List<Individuo> Z2 = new List<Individuo>();
            List<Individuo> Z3 = new List<Individuo>();
            #region Divisão dos Z's
            foreach (var divisao in individuos)
            {
                #region teste
                /*int ContadorAnteriorZ1 = 1, ContadorAnteriorZ2 = 2, ContadorAnteriorZ3 = 3;
                if(contador==1 || contador != 3 
                    || contador == ContadorAnteriorZ1 + 2)
                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    if(Z1.Count < quantidadeIndividuos/4)
                    Z1.Add(add);

                    ContadorAnteriorZ1 = contador;
                    contador++;
                    continue;
                }

                if (contador == 2 
                    || contador == ContadorAnteriorZ2 + 2)
                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    if (Z2.Count < quantidadeIndividuos / 4)
                        Z2.Add(add);

                    ContadorAnteriorZ2 = contador;
                    contador++;
                    continue;
                }

                if (contador == 3 
                    || contador == ContadorAnteriorZ3 + 2 
                    || Z1.Count() == quantidadeIndividuos/4 && Z2.Count == quantidadeIndividuos/4)                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    if (Z3.Count < (quantidadeIndividuos - Z1.Count() - Z2.Count()))
                        Z3.Add(add);

                    ContadorAnteriorZ3 = contador;
                    contador++;
                    continue;
                }*/
                #endregion
                if(contador <= 156)
                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    Z1.Add(add);
                }
                if (contador > 156 && contador <= 312)
                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    Z2.Add(add);
                }
                if (contador > 312)
                {
                    Individuo add = new Individuo(divisao.classe, divisao.a, divisao.b, divisao.c, divisao.d);
                    Z3.Add(add);
                }
                contador++;
            }
            #endregion
            int K = 1;
            int acertos = 0;
            int a = 0;
                string[] classeObtida = ClassificadorDeAmostras(Z1, Z2, K);

            foreach (var metricaAcertos in Z1)
            {
                Console.WriteLine(metricaAcertos.classe.ToString() + "///" + classeObtida[a].ToString());
                if (metricaAcertos.classe == classeObtida[a])
                { 
                    acertos++;
                }
                a++;
            }

                Console.WriteLine("Quantidade de acertos:" + acertos);
            Console.ReadKey();

            //testa as classes para ver se não foi esquecido nenhuma virgula
            //randomizador de dados e distribuidor

            //CLASSE TAOKEI
            //CARREGAMENTO DO ARQUIVO NO CONSOLE
            // DIVISÃO DA BASE E TESTES
            //TAXA DE ACERTO - TROCADOURO ( Z1 POR Z2)
            //MEDIA E DESVIO DE PADRÃO
            //ARMAZENAMENTO DE ACERTOS
        }
    }
}