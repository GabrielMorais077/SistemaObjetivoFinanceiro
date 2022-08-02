using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaObjetivoFinanceiro
{
    public class program
    {

        static void Main(string[] args)

        {
            Console.WriteLine("----- SISTEMA OBJETIVO FINANCEIRO ------");
            var Objetivos = new List<ObjetivoFinanceiro>();

            ExibirMenu();
            var opcao = Console.ReadLine();
            while (opcao != "0")
            {
                switch (opcao)
                {
                    case "1": //cadastro
                        CadastrarObjetivo();

                        break;
                    case "2": //deposito
                        RealizarOperacao(TipoOperacao.Deposito);

                        break;
                    case "3": //saque
                        RealizarOperacao(TipoOperacao.Saque);

                        break;
                    case "4": // resumo
                        ObterDetalhe();

                        break;
                    default: //opção invalida
                        Console.WriteLine("Opcao invalida");
                        break;
                }
                Console.WriteLine("\n");
                ExibirMenu();
                opcao = Console.ReadLine();

            }














            void ExibirMenu()
            {
                Console.WriteLine("Digite 1 para cadastro de objetivo");
                Console.WriteLine("Digite 2 para realizar um Depósito");
                Console.WriteLine("Digite 3 para realizar um saque");
                Console.WriteLine("Digite 4 Exibir resumo");
                Console.WriteLine("Digite 0 para encerrar");
            }

            void CadastrarObjetivo()
            {
                Console.WriteLine("Digite um objetivo");
                var Titulo = Console.ReadLine();

                Console.WriteLine("Digite o valor do objetivo");
                var ValorObjetivo = decimal.Parse(Console.ReadLine());
                var Objetivo = new ObjetivoFinanceiro(Titulo, ValorObjetivo);
                Objetivos.Add(Objetivo);
                Console.WriteLine($"Obejtivo: {Objetivo.Id} Foi criado com sucesso");




            }
            void RealizarOperacao(TipoOperacao tipo)
            {
                Console.WriteLine("Digite o Id do objetivo");
                var IdObjetivo = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o valor da operação: ");
                var Valor = decimal.Parse(Console.ReadLine());

                var Operacao = new operacao(Valor, tipo, IdObjetivo);
                var objetivo = Objetivos.SingleOrDefault(o => o.Id == IdObjetivo);
                objetivo.Operacoes.Add(Operacao);
                Console.WriteLine($"{tipo} Realizado no valor de R$: {Valor} concluido com sucesso");

            }
            void ObterDetalhe()
            {
                Console.WriteLine("Digite o Id do objetivo");
                var IdObjetivo = int.Parse(Console.ReadLine());

                
                var objetivo = Objetivos.SingleOrDefault(o => o.Id == IdObjetivo); //magia acontece aqui
                objetivo.imprimirResumo();
            }






        }



    }
    public class ObjetivoFinanceiro
    {
        public ObjetivoFinanceiro(string titulo, decimal? valorObjetivo)
        {
            int Id = new Random().Next(0, 1000);
            Titulo = titulo;
            ValorObjetivo = valorObjetivo;
            Operacoes = new List<operacao>();

        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public decimal? ValorObjetivo { get; private set; }
        public List<operacao> Operacoes { get; private set; }
        public decimal Saldo => ObterSaldo();

        public decimal ObterSaldo()
        {
            var totalDeposito = Operacoes.Where(o => o.tipo == TipoOperacao.Deposito).Sum(o => o.Valor);
            var totalSaque = Operacoes.Where(o => o.tipo == TipoOperacao.Saque).Sum(o => o.Valor);
            return totalDeposito - totalSaque;
        }
        public virtual void imprimirResumo()
        {
            Console.WriteLine($"Objetivo {Titulo}, Valor: {ValorObjetivo}, com Saldo R$: {Saldo}");
            if(Saldo >= ValorObjetivo)
            {
                Console.WriteLine($"Parabens você Atingiu o valor necessario para realizar seu objetivo");
            }

        }
    }
    public enum TipoOperacao { 
        Saque = 0,
        Deposito = 1
        
    }

    public class operacao
    {
        public operacao( decimal valor, TipoOperacao tipo, int idObjetivo)
        {
            Id = new Random().Next(0, 1000); //gerando id random 
            Valor = valor;
            this.tipo = tipo;
            IdObjetivo = idObjetivo;
        }

        public int Id { get; private set; }
        public decimal Valor { get; private set; }
        public TipoOperacao tipo { get; private set; }
        public int IdObjetivo { get; private set; }
    }
}
