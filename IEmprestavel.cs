namespace SistemaBancario.Modelos;


public interface IEmprestavel
{
    decimal LimiteDisponivel { get; }
    void SolicitarEmprestimo(decimal valor);
}