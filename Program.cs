using System.Globalization;
using System.Text;
using SistemaBancario.Modelos;

namespace SistemaBancario;

class Program
{
    
    static List<ContaBancaria> contas = new List<ContaBancaria>();
    static int proximoNumero = 1;

    static void Main()
    {
        
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

        int opcao;

        do
        {
            ExibirMenu();
            opcao = LerInteiro("Escolha uma opção: ");

            try
            {
                switch (opcao)
                {
                    case 1: CriarConta(); break;
                    case 2: Depositar(); break;
                    case 3: Sacar(); break;
                    case 4: Rendimento(); break;
                    case 5: Emprestimo(); break;
                    case 6: ListarContas(); break;
                    case 0: Console.WriteLine("Encerrando o sistema..."); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (SaldoInsuficienteException ex)
            {
                Console.WriteLine($"Operação negada: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Dado inválido: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operação inválida: {ex.Message}");
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcao != 0);
    }

    static void ExibirMenu()
    {
        Console.Clear();
        Console.WriteLine("===== SISTEMA BANCÁRIO =====");
        Console.WriteLine("1 - Criar conta");
        Console.WriteLine("2 - Depositar");
        Console.WriteLine("3 - Sacar");
        Console.WriteLine("4 - Aplicar rendimento (Poupança)");
        Console.WriteLine("5 - Solicitar empréstimo (Empresarial)");
        Console.WriteLine("6 - Listar contas");
        Console.WriteLine("0 - Sair");
        Console.WriteLine("============================");
    }

    static void CriarConta()
    {
        Console.WriteLine("\nTipo de conta: 1 - Corrente | 2 - Poupança | 3 - Empresarial");
        int tipo = LerInteiro("Tipo: ");

        Console.Write("Titular: ");
        string titular = Console.ReadLine() ?? "";
        decimal saldoInicial = LerDecimal("Saldo inicial: ");

        ContaBancaria conta;

        switch (tipo)
        {
            case 1:
                conta = new ContaCorrente(proximoNumero, titular, saldoInicial);
                break;
            case 2:
                conta = new ContaPoupanca(proximoNumero, titular, saldoInicial);
                break;
            case 3:
                decimal limite = LerDecimal("Limite de empréstimo: ");
                conta = new ContaEmpresarial(proximoNumero, titular, saldoInicial, limite);
                break;
            default:
                Console.WriteLine("Tipo inválido.");
                return;
        }

        contas.Add(conta);
        proximoNumero++;
        Console.WriteLine($"Conta criada com sucesso! Número: {conta.NumeroConta}");
    }

    static void Depositar()
    {
        ContaBancaria? conta = BuscarConta();
        if (conta == null) return;

        decimal valor = LerDecimal("Valor do depósito: ");
        conta.Depositar(valor);
        Console.WriteLine($"Depósito realizado. Novo saldo: {conta.Saldo:C}");
    }

    static void Sacar()
    {
        ContaBancaria? conta = BuscarConta();
        if (conta == null) return;

        decimal valor = LerDecimal("Valor do saque: ");
        conta.Sacar(valor); 
        Console.WriteLine($"Saque realizado. Novo saldo: {conta.Saldo:C}");
    }

    static void Rendimento()
    {
        ContaBancaria? conta = BuscarConta();
        if (conta == null) return;

       
        if (conta is IRentavel rentavel)
        {
            decimal percentual = LerDecimal("Percentual de rendimento (%): ");
            rentavel.AplicarRendimento(percentual);
            Console.WriteLine($"Novo saldo: {conta.Saldo:C}");
        }
        else
        {
            Console.WriteLine("Apenas contas poupança possuem rendimento.");
        }
    }

    static void Emprestimo()
    {
        ContaBancaria? conta = BuscarConta();
        if (conta == null) return;

        
        if (conta is IEmprestavel emprestavel)
        {
            Console.WriteLine($"Limite disponível: {emprestavel.LimiteDisponivel:C}");
            decimal valor = LerDecimal("Valor do empréstimo: ");
            emprestavel.SolicitarEmprestimo(valor);
            Console.WriteLine($"Empréstimo aprovado. Novo saldo: {conta.Saldo:C}");
        }
        else
        {
            Console.WriteLine("Apenas contas empresariais podem solicitar empréstimo.");
        }
    }

    static void ListarContas()
    {
        if (contas.Count == 0)
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        foreach (ContaBancaria conta in contas)
        {
            conta.ExibirInformacoes();
            Console.WriteLine();
        }
    }

    static ContaBancaria? BuscarConta()
    {
        int numero = LerInteiro("Número da conta: ");
        ContaBancaria? conta = contas.Find(c => c.NumeroConta == numero);

        if (conta == null)
            Console.WriteLine("Conta não encontrada.");

        return conta;
    }

   
    static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            try
            {
                return int.Parse(Console.ReadLine() ?? "");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Digite um número inteiro.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Número muito grande.");
            }
        }
    }

    static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            try
            {
                return decimal.Parse(Console.ReadLine() ?? "");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Digite um valor numérico (ex.: 150,75).");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Número muito grande.");
            }
        }
    }
}