using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDonaMariana_WinApp.ModuloQuestao;

namespace TestesDonaMariana_WinApp.BancoDeDados
{
    public class RepositorioMateriaBancoDeDados : IRepositorioMateria
    {
        private const string enderecoBanco = "Data Source=(LocalDB)\\MSSqlL" +
                "ocalDB;Initial Catalog=TestesMarianaDB;Inte" +
                "grated Security=True;Pooling=False";

        private const string sqlInserir = @"INSERT INTO [TBMATERIA]
                        (
                            [TITULO],
                            [SERIE],
                            [DISCIPLINA_NUMERO],
                            [QUESTOES_NUMERO]                            
                        )
                        VALUES
                        (
                            @TITULO,
                            @SERIE,
                            @DISCIPLINA_NUMERO,
                            @QUESTOES_NUMERO
                        )";

        private const string sqlEditar = @"UPDATE [TBMATERIA]
                                SET
                                    [TITULO] = @TITULO,
                                    [SERIE] = @SERIE,
                                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO,
                                    [QUESTOES_NUMERO] = @QUESTOES_NUMERO
                               WHERE
                                    [NUMERO] = @NUMERO";

        private const string sqlExcluir = @"DELETE FROM [TBMATERIA]
                                            WHERE
                                                [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos = @"SELECT
                                                        CM.[NUMERO],
                                                        CM.[TITULO],
                                                        CM.[SERIE],
                                                        CM.[QUESTOES_NUMERO],
                                                        CM.[DISCIPLINA_NUMERO],
                                                        CD.[TITULO] AS TITULO_DISCIPLINA
                                                    FROM
                                                        [TBMATERIA] AS CM LEFT JOIN
                                                        [TBDISCIPLINA] AS CD
                                                    ON
                                                        CM.DISCIPLINA_NUMERO = CD.NUMERO";

        private const string sqlSelecionarPorNumero = @"SELECT
                                                        CM.[NUMERO],
                                                        CM.[TITULO],
                                                        CM.[SERIE],
                                                        CM.[QUESTOES_NUMERO],
                                                        CM.[DISCIPLINA_NUMERO],
                                                        CD.[TITULO] AS TITULO_DISCIPLINA
                                                    FROM
                                                        [TBMATERIA] AS CM LEFT JOIN
                                                        [TBDISCIPLINA] AS CD
                                                    ON
                                                        CM.DISCIPLINA_NUMERO = CD.NUMERO
                                                    WHERE
                                                        CM.[NUMERO] = @NUMERO";

        private const string sqlSelecionarQuestoesMateria = @"SELECT
                                                                [NUMERO],
                                                                [TITULO],
                                                                [GABARITO],
                                                                [PERGUNTA1],
                                                                [PERGUNTA2],
                                                                [PERGUNTA3],
                                                                [PERGUNTA4]
                                                            FROM
                                                                [TBQUESTOES]
                                                            WHERE
                                                                [MATERIA_NUMERO] = @MATERIA_NUMERO";



        public ValidationResult Inserir(Materia novaMateria)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(novaMateria);

            if (resultadoValidacao.IsValid == false)
            {
                return resultadoValidacao;
            }

            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoBanco);

            comandoInsercao.Parameters.AddWithValue("TITULO", novaMateria.Titulo);
            comandoInsercao.Parameters.AddWithValue("SERIE", novaMateria.Serie);
            comandoInsercao.Parameters.AddWithValue("QUESTOES_NUMERO", novaMateria.Questoes.Count);
            comandoInsercao.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaMateria.Disciplina.Numero);

            conexaoBanco.Open();

            var id = comandoInsercao.ExecuteScalar();
            novaMateria.Numero = Convert.ToInt32(id);

            conexaoBanco.Close();

            return resultadoValidacao;
        }

        public void AdicionarQuestao(Materia materiaSelecionada, Questao questao)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Editar(Materia materia)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(materia);

            if (resultadoValidacao.IsValid == false)
            {
                return resultadoValidacao;
            }

            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoBanco);

            comandoEdicao.Parameters.AddWithValue("NUMERO", materia.Numero);
            comandoEdicao.Parameters.AddWithValue("TITULO", materia.Titulo);
            comandoEdicao.Parameters.AddWithValue("SERIE", materia.Serie);
            comandoEdicao.Parameters.AddWithValue("QUESTOES_NUMERO", materia.Questoes.Count);
            comandoEdicao.Parameters.AddWithValue("DISCIPLINA_NUMERO", materia.Disciplina.Numero);

            conexaoBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia materia)
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", materia.Numero);
            comandoExclusao.Parameters.AddWithValue("TITULO", materia.Titulo);
            comandoExclusao.Parameters.AddWithValue("QUESTOES_NUMERO", materia.Questoes.Count);
            comandoExclusao.Parameters.AddWithValue("DISCIPLINA_NUMERO", materia.Disciplina.Numero);

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

        public Materia SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarPorNumero = new SqlCommand(sqlSelecionarPorNumero, conexaoBanco);

            comandoSelecionarPorNumero.Parameters.AddWithValue("NUMERO", numero);

            conexaoBanco.Open();

            SqlDataReader leitorMateria = comandoSelecionarPorNumero.ExecuteReader();

            Materia materia = null;

            if (leitorMateria.Read())
            {
                materia = ConverterParaMateria(leitorMateria);
            }

            conexaoBanco.Close();

            CarregarQuestoesMateria(materia);

            return materia;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarTodos = new SqlCommand(sqlSelecionarTodos, conexaoBanco);

            conexaoBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecionarTodos.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorDisciplina.Read())
            {
                Materia materia = ConverterParaMateria(leitorDisciplina);

                materias.Add(materia);
            }

            conexaoBanco.Close();            

            return materias;
        }

        private static Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string titulo = Convert.ToString(leitorMateria["TITULO"]);
            int serie = Convert.ToInt32(leitorMateria["SERIE"]);

            var materia = new Materia
            {
                Numero = numero,
                Titulo = titulo,
                Serie = serie,
            };

            int numeroDisciplina = Convert.ToInt32(leitorMateria["DISCIPLINA_NUMERO"]);
            string tituloDisciplina = Convert.ToString(leitorMateria["TITULO_DISCIPLINA"]);

            materia.Disciplina = new Disciplina
            {
                Numero = numeroDisciplina,
                Titulo = tituloDisciplina
            };

            return materia;
        }

        private void CarregarQuestoesMateria(Materia materia)
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoesMateria, conexaoBanco);

            comandoSelecao.Parameters.AddWithValue("MATERIA_NUMERO", materia.Numero);

            conexaoBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                materia.Questoes.Add(questao);
            }

            conexaoBanco.Close();
        }
        private void AdicionarQuestao()
        {
            SqlConnection conexaoBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarTodos = new SqlCommand(sqlSelecionarTodos, conexaoBanco);

            conexaoBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecionarTodos.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorDisciplina.Read())
            {
                Questao questao = ConverterParaQuestao(leitorDisciplina);

                questoes.Add(questao);
            }

            conexaoBanco.Close();

        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            string titulo = Convert.ToString(leitorQuestao["TITULO"]);
            char gabarito = Convert.ToChar(leitorQuestao["GABARITO"]);
            string pergunta1 = Convert.ToString(leitorQuestao["PERGUNTA1"]);
            string pergunta2 = Convert.ToString(leitorQuestao["PERGUNTA2"]);
            string pergunta3 = Convert.ToString(leitorQuestao["PERGUNTA3"]);
            string pergunta4 = Convert.ToString(leitorQuestao["PERGUNTA4"]);

            var questao = new Questao
            {
                numero = numero,
                titulo = titulo,
                gabarito = gabarito,
                pergunta1 = pergunta1,
                pergunta2 = pergunta2,
                pergunta3 = pergunta3,
                pergunta4 = pergunta4
            };            

            return questao;
        }
    }
}
