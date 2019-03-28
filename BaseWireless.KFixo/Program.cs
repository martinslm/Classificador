using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseIris.Kfixo
{
    class Program
    {
        #region [classe Individuo]
        public class Individuo
        {
            private string _classe;
            private double _a, _b, _c, _d, _e, _f, _g;
            private bool _trocado;
            private bool _usado;
            private bool _errado;

            //Construtor. Os Doubles variam de acordo com a quantidade de atributos da tabela.
            public Individuo(string classe, double a, double b, double c, double d, double e, double f, double g)
            {
                _a = a;
                _b = b;
                _c = c;
                _d = d;
                _e = e;
                _f = f;
                _g = g;
                _classe = classe;
                _trocado = false;
            }
            public double a
            {
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
            public double e
            {
                get
                {
                    return _e;
                }
                set
                {
                    _e = value;
                }
            }
            public double f
            {
                get
                {
                    return _f;
                }
                set
                {
                    _f = value;
                }
            }
            public double g
            {
                get
                {
                    return _g;
                }
                set
                {
                    _g = value;
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
            public bool usado
            {
                get
                {
                    return _usado;
                }
                set
                {
                    _usado = value;
                }
            }
            public bool errado
            {
                get
                {
                    return _errado;
                }
                set
                {
                    _errado = value;
                }
            }
        }
        #endregion
        #region [classe Indicadores]
        public class Indicadores
        {
            private int _acertos;
            private int _erros;
            private double _taxaDeAcerto;

            public int acertos
            {
                get
                {
                    return _acertos;
                }
                set
                {
                    _acertos = value;
                }
            }
            public int erros
            {
                get
                {
                    return _erros;
                }
                set
                {
                    _erros = value;
                }
            }
            public double taxaDeAcerto
            {
                get
                {
                    return _taxaDeAcerto;
                }
                set
                {
                    _taxaDeAcerto = value;
                }
            }

            public Indicadores(int acertos, int erros, double taxaDeAcerto)
            {
                _acertos = acertos;
                _erros = erros;
                _taxaDeAcerto = taxaDeAcerto;
            }
        }
        #endregion
        #region [Functions]

        public static string[] CarregarDataBase()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\wireless.txt");

            return lines;
        }
        //Faz a leitura da base de dados e transforma ela em objetos do tipo individuo. 
        public static List<Individuo> SeparadorDeAtributos(string[] dataBase)
        {
            List<Individuo> individuos = new List<Individuo>();

            foreach (var dados in dataBase)
            {
                string[] colunas = dados.Split(',');
                string classe = colunas[7];
                double a = Convert.ToDouble(colunas[1]), b = Convert.ToDouble(colunas[2]), c = Convert.ToDouble(colunas[3]), d = Convert.ToDouble(colunas[4]), e = Convert.ToDouble(colunas[5]), f = Convert.ToDouble(colunas[6]), g = Convert.ToDouble(colunas[0]);

                Individuo individuo = new Individuo(classe, a, b, c, d, e, f, g);
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
                + Math.Pow((ind1.d - ind2.d), 2)
                + Math.Pow((ind1.e - ind2.e), 2)
                + Math.Pow((ind1.f - ind2.f), 2)
                + Math.Pow((ind1.g - ind2.g), 2);
            return Math.Sqrt(soma);
        }
        public static string[] ClassificadorDeAmostras(List<Individuo> C1, List<Individuo> C2, int k)
        {
            if (k % 2 == 0)
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
                distanciaIndividuos.Sort((x, y) => x.Key.CompareTo(y.Key));

                int contadorum = 0, contadordois = 0, contadortres = 0, contadorquatro = 0;
                for (int i = 0; i < k; i++)
                {
                    string classe = distanciaIndividuos[i].Value.classe;

                    if (classe == "1") //nome dos arquivos
                        contadorum++;
                    if (classe == "2")
                        contadordois++;
                    if (classe == "3")
                        contadortres++;
                    if (classe == "4")
                        contadorquatro++;
                }
                string classeClassificacao;
                if (contadorum >= contadordois && contadorum >= contadortres && contadorum >= contadorquatro)
                    classeClassificacao = "1";
                else if (contadordois >= contadorum && contadordois >= contadortres && contadordois >= contadorquatro)
                    classeClassificacao = "2";
                else if (contadortres >= contadorum && contadortres >= contadordois && contadortres >= contadorquatro)
                    classeClassificacao = "3";
                else
                    classeClassificacao = "4";
                classesC1[posicao] = classeClassificacao;
                posicao++;
                distanciaIndividuos = null;
            }
            return classesC1;
        }
        #endregion

        static void Main(string[] args)
        {
            List<Indicadores> ResultadoTestes = new List<Indicadores>();
            List<Individuo> individuos = new List<Individuo>();
            string[] database = CarregarDataBase();
            individuos = SeparadorDeAtributos(database);
            int quantidadeIndividuos = individuos.Count();
            int K = 27;

            List<Individuo> Listum = new List<Individuo>();
            List<Individuo> Listdois = new List<Individuo>();
            List<Individuo> Listtres = new List<Individuo>();
            List<Individuo> Listquatro = new List<Individuo>();
            List<Individuo> Z1 = new List<Individuo>();
            List<Individuo> Z2 = new List<Individuo>();
            List<Individuo> Z3 = new List<Individuo>();

            Console.WriteLine("Iniciando... \n Database: Wireless utilizando K fixo.");
            for (int contador = 1; contador <= 30; contador++)
            {
                int acertos = 0, erros = 0;
                double taxaDeAcerto = 0;
                #region Divisão por Classe (Base pra dividir os Z's)
                foreach (var indv in individuos)
                {
                    if (indv.classe == "1")
                    {
                        Listum.Add(indv);
                        continue;
                    }
                    if (indv.classe == "2")
                    {
                        Listdois.Add(indv);
                        continue;
                    }
                    if (indv.classe == "3")
                    {
                        Listtres.Add(indv);
                        continue;
                    }
                    if (indv.classe == "4")
                    {
                        Listquatro.Add(indv);
                        continue;
                    }
                }
                #endregion
                #region Divisão dos Z's
                Random randNum = new Random();
                Individuo AuxAdd;
                #region [Setosa para Z1]
                while (Z1.Count() < 13)
                {
                    AuxAdd = Listum.ElementAt(randNum.Next(Listum.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z1.Add(AuxAdd);
                    }
                }
                #endregion
                #region [um para Z2] 
                while (Z2.Count() < 13)
                {

                    AuxAdd = Listum.ElementAt(randNum.Next(Listum.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z2.Add(AuxAdd);
                    }
                }
                #endregion
                #region [um para Z3]
                while (Z3.Count() < 24)
                {
                    AuxAdd = Listum.Where(c => c.usado == false).First();
                    AuxAdd.usado = true;
                    Z3.Add(AuxAdd);
                }
                #endregion
                #region [dois para Z1]
                while (Z1.Count() < 26)
                {
                    AuxAdd = Listdois.ElementAt(randNum.Next(Listdois.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z1.Add(AuxAdd);
                    }
                }
                #endregion
                #region [dois para Z2]
                while (Z2.Count() < 26)
                {
                    AuxAdd = Listdois.ElementAt(randNum.Next(Listdois.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z2.Add(AuxAdd);
                    }
                }
                #endregion
                #region [dois para Z3]
                while (Z3.Count() < 48)
                {
                    AuxAdd = Listdois.Where(c => c.usado == false).First();
                    AuxAdd.usado = true;
                    Z3.Add(AuxAdd);
                }
                #endregion
                #region [tres para Z1]
                while (Z1.Count() < 38)
                {
                    AuxAdd = Listtres.ElementAt(randNum.Next(Listtres.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z1.Add(AuxAdd);
                    }
                }
                #endregion
                #region [tres para Z2]
                while (Z2.Count() < 38)
                {
                    AuxAdd = Listtres.ElementAt(randNum.Next(Listtres.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z2.Add(AuxAdd);
                    }
                }
                #endregion
                #region [tres para Z3]
                while (Z3.Count() < 74)
                {
                    AuxAdd = Listtres.Where(c => c.usado == false).First();
                    AuxAdd.usado = true;
                    Z3.Add(AuxAdd);
                }
                #endregion
                #region [quatro para Z1]
                while (Z1.Count() < 38)
                {
                    AuxAdd = Listquatro.ElementAt(randNum.Next(Listquatro.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z1.Add(AuxAdd);
                    }
                }
                #endregion
                #region [quatro para Z2]
                while (Z2.Count() < 38)
                {
                    AuxAdd = Listquatro.ElementAt(randNum.Next(Listquatro.Count() - 1));
                    if (!AuxAdd.usado)
                    {
                        AuxAdd.usado = true;
                        Z2.Add(AuxAdd);
                    }
                }
                #endregion
                #region [quatro para Z3]
                while (Z3.Count() < 74)
                {
                    AuxAdd = Listquatro.Where(c => c.usado == false).First();
                    AuxAdd.usado = true;
                    Z3.Add(AuxAdd);
                }
                #endregion
                #endregion
                string[] classeObtida = ClassificadorDeAmostras(Z1, Z2, K);
                int a = 0;
                Individuo auxTroca = null, auxTroca2 = null;
                #region Verifica os errados em Z1
                foreach (var metricaAcertos in Z1)
                {
                    if (metricaAcertos.classe != classeObtida[a])
                    {
                        metricaAcertos.errado = true;
                    }
                    //indicador de posição da classe - Ou seja, auxiliar do Foreach
                    a++;
                }
                #endregion
                #region [Troca o que tá errado em Z1 com algum Z2 com classe igual]
                while (Z1.Any(e => e.errado == true))
                {
                    auxTroca = Z1.Where(e => e.errado == true).First();
                    auxTroca2 = Z2.Where(c => c.classe == auxTroca.classe).First();
                    Z1.Remove(auxTroca);
                    Z2.Remove(auxTroca2);
                    auxTroca.trocado = true;
                    auxTroca.errado = false;
                    auxTroca2.trocado = true;
                    Z1.Add(auxTroca2);
                    Z2.Add(auxTroca);
                }
                #endregion
                #region [Limpeza das Variaveis para a "Próxima Rodada"]
                foreach (var limpZ1 in Z1)
                {
                    limpZ1.trocado = false;
                    limpZ1.usado = false;
                }

                foreach (var limpZ2 in Z2)
                {
                    limpZ2.trocado = false;
                    limpZ2.usado = false;
                }
                #endregion
                //Testa Z3 com Z2 e retorna os resultados.
                classeObtida = ClassificadorDeAmostras(Z3, Z2, K);
                #region Verifica os acertos em Z3
                a = 0;
                foreach (var metricaAcertos in Z3)
                {
                    if (metricaAcertos.classe == classeObtida[a])
                    {
                        acertos++;
                    }
                    else
                    {
                        erros++;
                    }
                    //indicador de posição da classe - Ou seja, auxiliar do Foreach
                    a++;
                }
                #endregion

                #region [Grava a quantidade de acertos para fazer os relatorios depois]
                taxaDeAcerto = (acertos * 100) / Z3.Count();
                Indicadores indicador = new Indicadores(acertos, erros, taxaDeAcerto);
                ResultadoTestes.Add(indicador);
                #endregion

                Console.WriteLine("Rodada " + contador + "...\n" + "Taxa de Acerto: " + taxaDeAcerto + "%");
            }
            Console.WriteLine("... \n...");
            #region [Calculo Final]
            double soma = 0, media, desvioPadrao;
            foreach (var baseDeCalculo in ResultadoTestes)
            {
                soma += baseDeCalculo.taxaDeAcerto;
            }
            media = soma / ResultadoTestes.Count();
            soma = 0;
            foreach (var baseDeCalculo in ResultadoTestes)
            {
                soma += Math.Pow((baseDeCalculo.taxaDeAcerto - media), 2);
            }
            desvioPadrao = Math.Sqrt(soma / ResultadoTestes.Count());
            Console.WriteLine("Media:" + media);
            Console.WriteLine("Desvio Padrão:" + desvioPadrao);
            #endregion
            Console.ReadKey();

        }
    }
}
