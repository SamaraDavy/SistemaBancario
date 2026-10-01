using System.Globalization;
namespace SistemaBancario.Modelos;

public class ContaCorrente : ContaBancaria
{
    public const decimal TaxaSaque = 2.50m;

    public ContaCorrente(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser maior que zero.");

        decimal total = valor + TaxaSaque;

        if (total > Saldo)
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente. Saque de {valor:C} + taxa de {TaxaSaque:C} = {total:C}.");

        Saldo -= total;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Conta nº {NumeroConta} | Titular: {Titular} | Saldo: {Saldo.ToString("C", new CultureInfo("pt-BR"))}"); ;
    }
}
