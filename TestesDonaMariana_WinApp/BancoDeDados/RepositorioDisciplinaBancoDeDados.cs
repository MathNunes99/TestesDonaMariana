using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDonaMariana_WinApp.ModuloDisciplina;

namespace TestesDonaMariana_WinApp.BancoDeDados
{
    public class RepositorioDisciplinaBancoDeDados : IRepositorioDisciplina
    {
        private const string enderecoBanco = "Data Source=(LocalDB)\\MSSqlL" +
                "ocalDB;Initial Catalog=TestesMarianaDB;Inte" +
                "grated Security=True;Pooling=False";

        private const string sqlInserir = @"INSERT INTO [TBDISCIPLINA]
                        (
                            [TITULO]
                        )
                        VALUES
                        (
                            @TITULO
                        )";

        private const string sqlEditar = @"UPDATE [TBDISCIPLINA]
                                SET
                                    [TITULO] = @TITULO
                               WHERE
                                    [NUMERO] = @NUMERO";

        private const string sqlExcluir = @"DELETE FROM [TBDISCIPLINA]
                                            WHERE
                                                [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos = @"SELECT [NUMERO],[TITULO] FROM [TBDISCIPLINA]";

        private const string sqlSelecionarPorNumero = @"SELECT [NUMERO],[TITULO]
                                                        FROM [TBDISCIPLINA]
                                                        WHERE [NUMERO] = @NUMERO";

        public ValidationResult Inserir(Disciplina novaDisciplina)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(novaDisciplina);

            if (resultadoValidacao.IsValid == false)
            {
                return resultadoValidacao;
            }            

            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir,conexaoBanco);            

            comandoInsercao.Parameters.AddWithValue("TITULO", novaDisciplina.Titulo);

            conexaoBanco.Open();

            var id = comandoInsercao.ExecuteScalar();
            novaDisciplina.Numero = Convert.ToInt32(id);

            conexaoBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Disciplina disciplina)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                return resultadoValidacao;
            }

            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoBanco);

            comandoEdicao.Parameters.AddWithValue("NUMERO", disciplina.Numero);
            comandoEdicao.Parameters.AddWithValue("TITULO", disciplina.Titulo);

            conexaoBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Disciplina disciplina)
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", disciplina.Numero);
            comandoExclusao.Parameters.AddWithValue("TITULO", disciplina.Titulo);

            conexaoBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
            {
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possivel Remover o registro"));
            }

            conexaoBanco.Close();

            return resultadoValidacao;
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarTodos = new SqlCommand(sqlSelecionarTodos, conexaoBanco);

            conexaoBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecionarTodos.ExecuteReader();

            List<Disciplina> disciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                disciplinas.Add(disciplina);
            }

            conexaoBanco.Close();

            return disciplinas;
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarPorNumero = new SqlCommand(sqlSelecionarPorNumero, conexaoBanco);

            comandoSelecionarPorNumero.Parameters.AddWithValue("NUMERO", numero);

            conexaoBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecionarPorNumero.ExecuteReader();

            Disciplina disciplina = null;

            if (leitorDisciplina.Read())
            {
                disciplina = ConverterParaDisciplina(leitorDisciplina);
            }

            conexaoBanco.Close();

            return disciplina;
        }

        private static Disciplina ConverterParaDisciplina(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string titulo = Convert.ToString(leitorDisciplina["TITULO"]);

            var disciplina = new Disciplina
            {
                Numero = numero,
                Titulo = titulo
            };
            return disciplina;
        }

        

        
    }
}
