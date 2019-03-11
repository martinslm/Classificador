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

        public static string ClassificadorDeAmostras(List<Individuo> individuos, Individuo novoExemplo, int k)
        {
            if(k%2 == 0)
            {
                k--;
                k = k <= 0 ? 1 : k;
            }

            int quantidadeElementos = individuos.Count();
            var distanciaIndividuos = new List<KeyValuePair<double, int>>();
            for(int i = 0; i < quantidadeElementos; i++)
            {
                double distancia = obterDistanciaEuclidiana(individuos[i], novoExemplo);
                distanciaIndividuos.Add(new KeyValuePair<double, int>(distancia, i));
            }

            int contador1 = 0, contador2 = 0, contador3 = 0, contK = 0;
            foreach( var dist in distanciaIndividuos)
            {
                string classe = individuos[dist.Value].classe;

                if (classe == "Iris-setosa") //nome dos arquivos
                    contador1++;
                if (classe == "Iris-versicolor")
                    contador2++;
                if (classe == "Iris-virginica")
                    contador3++;
                if (contK > k)
                    break;

                contK++;
            }
            string classeClassificacao;
            if (contador1 >= contador2 && contador1 >= contador3)
                classeClassificacao = "Iris-setosa";
            else if (contador2 >= contador1 && contador2 >= contador3)
                classeClassificacao = "Iris-versicolor";
            else
                classeClassificacao = "Iris-virginica";
            return classeClassificacao;
        }




        #endregion

        static void Main(string[] args)
        {
            List<Individuo> individuos = new List<Individuo>();
            string[] database = CarregarDataBase();
            individuos = SeparadorDeAtributos(database);
            //SOMENTE PARA TESTE SERÁ APAGADO DEPOIS.
            foreach(var teste in individuos)
            {
                Console.WriteLine(teste.classe + "," + teste.a + "," + teste.b + "," + teste.c + "," + teste.c);
            }

            int K = 3;
            int tamTreinamento = 105; //ajustar para o tamanho do arquivo X 0,25
            for (int i = 0; i<tamTreinamento; i++)
            {
                string classe;
                double a, b, c, d;
                a = Convert.ToDouble(Console.ReadLine());
                b = Convert.ToDouble(Console.ReadLine());
                c = Convert.ToDouble(Console.ReadLine());
                d = Convert.ToDouble(Console.ReadLine());
                classe = Console.ReadLine();
                 
                Individuo individuo = new Individuo(classe, a, b, c, d);

                individuos.Add(individuo);
            }

            int acertos = 0;
            int tamanhoTestes = 150 - tamTreinamento;

            for(int i = 0; i < tamanhoTestes; i++)
            {
                string classe;
                double a, b, c, d;

                a = Convert.ToDouble(Console.ReadLine());
                b = Convert.ToDouble(Console.ReadLine());
                c = Convert.ToDouble(Console.ReadLine()); 
                d = Convert.ToDouble(Console.ReadLine());
                classe = Console.ReadLine();

                Individuo individuo = new Individuo(classe, a, b, c, d);
                string classeObtida = ClassificadorDeAmostras(individuos, individuo, K);
                Console.WriteLine("Classe esperada:" + classe + "\nClasse obtida:" + classeObtida);

                if (classe == classeObtida)
                    acertos++;

                var erros = tamanhoTestes - acertos;

                Console.WriteLine("Quantidade de acertos:" + acertos + "\nQuantidade de erros:" + erros);

            }

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