namespace SistemaBancario.Modelos;

public class ContaEmpresarial : ContaBancaria, IEmprestavel
{
    public decimal LimiteEmprestimo { get; private set; }
    public decimal EmprestimoUtilizado { get; private set; }

    public ContaEmpresarial(int numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
        : base(numeroConta, titular, saldoInicial)
    {
        if (limiteEmprestimo < 0)
            throw new ArgumentException("O limite de empréstimo não pode ser negativo.");

        LimiteEmprestimo = limiteEmprestimo;
    }

    public decimal LimiteDisponivel => LimiteEmprestimo - EmprestimoUtilizado;

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser maior que zero.");

        if (valor > Saldo)
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente. Saldo atual: {Saldo:C}. Se precisar, solicite um empréstimo.");

        Saldo -= valor;
    }

    public void SolicitarEmprestimo(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do empréstimo deve ser maior que zero.");

        if (valor > LimiteDisponivel)
            throw new InvalidOperationException(
                $"Valor acima do limite disponível ({LimiteDisponivel:C}).");

        EmprestimoUtilizado += valor;
        Saldo += valor;
    }

    public override void ExibirInformacoes()
    {
        Console.Write("[Empresarial] ");
        base.ExibirInformacoes();
        Console.WriteLine($"   Limite de empréstimo: {LimiteEmprestimo:C} | Disponível: {LimiteDisponivel:C}");
    }
}
