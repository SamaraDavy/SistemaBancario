namespace SistemaBancario.Modelos;

public abstract class ContaBancaria
{
    public int NumeroConta { get; private set; }
    public string Titular { get; private set; }
    public decimal Saldo { get; protected set; }

    protected ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
    {
        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("O titular não pode ser vazio.");
        if (saldoInicial < 0)
            throw new ArgumentException("O saldo inicial não pode ser negativo.");

        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;
    }
   
    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do depósito deve ser maior que zero.");

        Saldo += valor;
    }
    public abstract void Sacar(decimal valor);

    public virtual void ExibirInformacoes()
    {
        Console.WriteLine($"Conta nº {NumeroConta} | Titular: {Titular} | Saldo: {Saldo:C}");
    }
}
