namespace SistemaBancario.Modelos;

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException(string mensagem) : base(mensagem)
    {
    }
}
