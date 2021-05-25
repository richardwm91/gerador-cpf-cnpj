using System;

namespace gerador_cpf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escolha para gerar");
            Console.WriteLine("\t1 - CPF");
            Console.WriteLine("\t2 - CNPJ");
            var tipo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quantos " + (tipo == 1 ? "CPFs" : "CNPJs") + " gostaria de gerar?");
            var qtd = Convert.ToInt32(Console.ReadLine());

            if (tipo == 1)
                Console.WriteLine(GeradorCPF(qtd));
            else if (tipo == 2)
                Console.WriteLine(GeradorCNPJ(qtd));
            else
                Console.WriteLine("Opção inválida, tente novamente");
            Console.ReadKey();
        }

        private static string GeradorCPF(int qtd)
        {
            //calc: http://clubes.obmep.org.br/blog/a-matematica-nos-documentos-cpf/

            Int64 soma1, soma2, i, erro, parte1, parte2, parte3, dig1, parte5, parte6, parte7, dig2;
            Int64[] numero = new Int64[13];
            Random rand = new Random();
            var resultado = "";

            var posicaoCPF = 1;
            //para ter x numeros de na saída da geração
            for (posicaoCPF = 1; posicaoCPF <= qtd; posicaoCPF++)
            {
                //Caso queira gerar 100% aleatorio
                //for (i = 1; i <= 9; i++)
                //{
                //    erro = 1;
                //    do
                //    {
                //        if (erro > 1) 
                //            erro = 1;
                //        numero[i] = (rand.Next()) % 9;
                //        erro++;
                //    } while (numero[i] > 9 || numero[i] < 0);
                //}

                //Se não, pode definir numeros do primeiro ao oitavo digito
                //nesse caso, quero os 4 primeiros como 7
                //Dígitos definidos
                numero[1] = 7;
                numero[2] = 7;
                numero[3] = 7;
                numero[4] = 7;

                //Completo com numeros aleatorios para aumentar a quantidade de CPFs distintos
                for (i = 5; i <= 8; i++)
                {
                    erro = 1;
                    do
                    {
                        if (erro > 1)
                            erro = 1;
                        numero[i] = (rand.Next()) % 9;
                        erro++;
                    } while (numero[i] > 9 || numero[i] < 0);
                }

                //Digíto do estado RS (sou de SP, não quero que conflite com nenhum CPF da minha região)
                numero[9] = 0;
                /*
                    1 – DF, GO, MS, MT e TO
                    2 – AC, AM, AP, PA, RO e RR
                    3 – CE, MA e PI
                    4 – AL, PB, PE, RN
                    5 – BA e SE
                    6 – MG
                    7 – ES e RJ
                    8 – SP
                    9 – PR e SC
                    0 – RS
                 */

                //Primeiro digito verificador
                soma1 = ((numero[1] * 10) +
                      (numero[2] * 9) +
                      (numero[3] * 8) +
                      (numero[4] * 7) +
                      (numero[5] * 6) +
                      (numero[6] * 5) +
                      (numero[7] * 4) +
                      (numero[8] * 3) +
                      (numero[9] * 2));

                parte1 = (soma1 / 11);
                parte2 = (parte1 * 11);
                parte3 = (soma1 - parte2);
                dig1 = (11 - parte3);
                if (dig1 > 9) dig1 = 0;

                //Segundo digito verificador
                soma2 = ((numero[1] * 11) +
                      (numero[2] * 10) +
                      (numero[3] * 9) +
                      (numero[4] * 8) +
                      (numero[5] * 7) +
                      (numero[6] * 6) +
                      (numero[7] * 5) +
                      (numero[8] * 4) +
                      (numero[9] * 3) +
                      (dig1 * 2));
                parte5 = (soma2 / 11);
                parte6 = (parte5 * 11);
                parte7 = (soma2 - parte6);
                dig2 = (11 - parte7);
                if (dig2 > 9) dig2 = 0;

                //Impressao do numero completo
                for (i = 1; i <= 9; i++)
                {
                    //numeros do CPF
                    resultado += Convert.ToString(numero[i]);
                }
                // dois últimos digitos
                resultado += dig1.ToString() + dig2.ToString() + Environment.NewLine;
            }

            return resultado;
        }

        public static string GeradorCNPJ(int qtd)
        {
            // calc: http://www.macoratti.net/alg_cnpj.htm#:~:text=O%20n%C3%BAmero%20que%20comp%C3%B5e%20o,que%20s%C3%A3o%20os%20d%C3%ADgitos%20verificadores.
            Int64 soma1, soma2, i, cnpj, parte1, parte2, parte3, dig1, parte5, parte6, parte7, dig2;
            Int64[] numero = new Int64[13];
            Random rand = new Random();
            var resultado = "";

            for (cnpj = 1; cnpj <= qtd; cnpj++)
            {
                for (i = 1; i <= 8; i++)
                {
                    numero[i] = (rand.Next()) % 9;
                }
                numero[9] = 0;
                numero[10] = 0;
                numero[11] = 0;
                numero[12] = (rand.Next()) % 9;

                //Primeiro digito verificador
                soma1 = ((numero[1] * 5) +
                      (numero[2] * 4) +
                      (numero[3] * 3) +
                      (numero[4] * 2) +
                      (numero[5] * 9) +
                      (numero[6] * 8) +
                      (numero[7] * 7) +
                      (numero[8] * 6) +
                      (numero[9] * 5) +
                      (numero[10] * 4) +
                      (numero[11] * 3) +
                      (numero[12] * 2));
                parte1 = (soma1 / 11);
                parte2 = (parte1 * 11);
                parte3 = (soma1 - parte2);
                dig1 = (11 - parte3);
                if (dig1 > 9) dig1 = 0;

                //Segundo digito verificador
                soma2 = ((numero[1] * 6) +
                      (numero[2] * 5) +
                      (numero[3] * 4) +
                      (numero[4] * 3) +
                      (numero[5] * 2) +
                      (numero[6] * 9) +
                      (numero[7] * 8) +
                      (numero[8] * 7) +
                      (numero[9] * 6) +
                      (numero[10] * 5) +
                      (numero[11] * 4) +
                      (numero[12] * 3) +
                      (dig1 * 2));
                parte5 = (soma2 / 11);
                parte6 = (parte5 * 11);
                parte7 = (soma2 - parte6);
                dig2 = (11 - parte7);
                if (dig2 > 9) dig2 = 0;

                //Impressao do numero completo
                for (i = 1; i <= 12; i++)
                {
                    //numeros do CNPJ
                    resultado += Convert.ToString(numero[i]);
                }
                // dois últimos digitos
                resultado += dig1 + "" + dig2 + Environment.NewLine;
            }
            return resultado;
        }
    }
}
