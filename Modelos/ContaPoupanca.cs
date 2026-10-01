namespace SistemaBancario.Modelos;

public class ContaPoupanca : ContaBancaria, IRentavel
{
    public ContaPoupanca(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }
    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser maior que zero.");

        if (valor > Saldo)
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente. Saldo atual: {Saldo:C}.");

        Saldo -= valor;
    }

    public void AplicarRendimento(decimal percentual)
    {
        if (percentual <= 0)
            throw new ArgumentException("O percentual de rendimento deve ser maior que zero.");

        decimal rendimento = Math.Round(Saldo * percentual / 100m, 2);
        Saldo += rendimento;
        Console.WriteLine($"Rendimento de {rendimento:C} aplicado.");
    }

    public override void ExibirInformacoes()
    {
        Console.Write("[Poupança] ");
        base.ExibirInformacoes();
    }
}
