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
            private bool _trocado;

            //Construtor. Os Doubles variam de acordo com a quantidade de atributos da tabela.
            public Individuo(string classe, double a, double b, double c, double d)
            {
                _a = a;
                _b = b;
                _c = c;
                _d = d;
                _classe = classe;
                _trocado = false;
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
            public bool trocado
            {
                get
                {
                    return _trocado;
                }
                set
                {
                    _trocado = value;
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
        //Faz a leitura da base de dados e transforma ela em objetos do tipo individuo. 
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
                    distanciaIndividuos.Add(new KeyValuePair<double, Individuo>(distancia, elemento2));
                }
                distanciaIndividuos.Sort((x,y) => x.Key.CompareTo(y.Key));

                int contadorL = 0, contadorR = 0, contadorB = 0;
                for (int i = 0; i < k; i++)
                {
                    string classe = distanciaIndividuos[i].Value.classe;

                    if (classe == "L") //nome dos arquivos
                        contadorL++;
                    if (classe == "R")
                        contadorR++;
                    if (classe == "B")
                        contadorB++;
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
            #region Divisão por Classe (Base pra dividir os Z's
            List<Individuo> ListL = new List<Individuo>();
            List<Individuo> ListB = new List<Individuo>();
            List<Individuo> ListR = new List<Individuo>();
            foreach (var indv in individuos)
            {
                if (indv.classe == "L")
                {
                    ListL.Add(indv);
                    continue;
                }
                if(indv.classe == "R")
                {
                    ListR.Add(indv);
                    continue;
                }
                if(indv.classe == "B")
                {
                    ListB.Add(indv);
                    continue;
                }
            }
            #endregion
            #region Divisão dos Z's
            int contadorDistL = 1, contadorDistR = 1, contadorDistB = 1;
            List<Individuo> Z1 = new List<Individuo>();
            List<Individuo> Z2 = new List<Individuo>();
            List<Individuo> Z3 = new List<Individuo>();
            //Distribuição de L para Z1, Z2 e Z3
            foreach (var distL in ListL)
            {
                if(contadorDistL <= 72)
                {
                    Z1.Add(distL);
                    contadorDistL++;
                    continue;
                }
                if(contadorDistL <= 144)
                {
                    Z2.Add(distL);
                    contadorDistL++;
                    continue;
                }
                if(contadorDistL <= 288)
                {
                    Z3.Add(distL);
                    contadorDistL++;
                    continue;
                }
             }
            //Distribuição de B para Z1, Z2 e Z3
            foreach (var distB in ListB)
            {
                if (contadorDistB <= 12)
                {
                    Z1.Add(distB);
                    contadorDistB++;
                    continue;
                }
                if (contadorDistB <= 24)
                {
                    Z2.Add(distB);
                    contadorDistB++;
                    continue;
                }
                if (contadorDistB <= 49)
                {
                    Z3.Add(distB);
                    contadorDistB++;
                    continue;
                }
            }
            //Distribuição de R para Z1, Z2 e Z3
            foreach (var distR in ListR)
            {
                if (contadorDistR <= 72)
                {
                    Z1.Add(distR);
                    contadorDistR++;
                    continue;
                }
                if (contadorDistR <= 144)
                {
                    Z2.Add(distR);
                    contadorDistR++;
                    continue;
                }
                if (contadorDistR <= 288)
                {
                    Z3.Add(distR);
                    contadorDistR++;
                    continue;
                }
            }
            #endregion
            int K = 19;
            for (int z = 1; z <= 30; z++)
            {
                string[] classeObtida = ClassificadorDeAmostras(Z1, Z2, K);
                int acertos = 0, erros = 0;
                int a = 0;
                Individuo auxTroca = null, auxTroca2 = null;
                foreach (var metricaAcertos in Z1)
                {
                    if (metricaAcertos.classe == classeObtida[a])
                    {
                        acertos++;
                    }
                    else
                    {
                        erros++;
                        if (metricaAcertos.trocado == false)
                        {
                            auxTroca = metricaAcertos;
                            auxTroca2 = Z2.Where(x => x.classe == metricaAcertos.classe).First();
                        }
                    }
                    a++;
                }
                if (auxTroca != null || auxTroca2 != null)
                {
                    Z1.Remove(auxTroca);
                    Z2.Remove(auxTroca2);
                    auxTroca.trocado = true;
                    auxTroca2.trocado = true;
                    Z1.Add(auxTroca2);
                    Z2.Add(auxTroca);
                }
                Console.WriteLine("Quantidade de acertos:" + acertos);
                Console.WriteLine("Erros: " + erros);
                Console.WriteLine("Taxa de acerto: " + (acertos * 100) / Z1.Count() + "%");
            }
            Console.ReadKey();

            // DIVISÃO DA BASE E TESTES
            //TAXA DE ACERTO - TROCADOURO ( Z1 POR Z2)
            //MEDIA E DESVIO DE PADRÃO
            //ARMAZENAMENTO DE ACERTOS
        }
    }
}