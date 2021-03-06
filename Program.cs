﻿using System;
using System.Data.SQLite;
using System.Text;
using System.Data;

namespace ConsoleAppSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Conectando-se ao SQLite para demonstrar as operações de CRUD");
                //Construindo a string de conexão
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                //Data source
                sb.DataSource = @"C:\SQLite\sqlite-tools-win32-x86-3310100\senac.db";
                //Conectando-se ao SQLite
                Console.WriteLine("Conectando-se ao SQLite...");
                using (SQLiteConnection con = new SQLiteConnection(sb.ConnectionString))
                {
                    //Criando uma tabela e inserindo alguns registros
                    Console.WriteLine("Criando tabela, pressione qualquer tecla para continuar...");
                    //Aguardando ação do usuário
                    Console.ReadKey(true);
                    StringBuilder builder = new StringBuilder();
                    //builder.Append("USE senac.db;");
                    builder.Append("CREATE TABLE endereco ( ");
                    builder.Append(" Codigo INTEGER PRIMARY KEY AUTOINCREMENT, ");
                    builder.Append(" Logradouro VARCHAR(50) NOT NULL, ");
                    builder.Append(" Numero VARCHAR(9) NOT NULL ");
                    builder.Append("); ");
                    builder.Append("INSERT INTO endereco (Logradouro, Numero) VALUES ");
                    builder.Append("('Rua Alberto Andaló', '5689'), ");
                    builder.Append("('Rua Tereza P. Padovez', '77'), ");
                    builder.Append("('Rua Treze de Maio', '666'); ");
                    string sql = builder.ToString();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con) )
                    {
                        //Abrindo a conexão
                        con.Open();
                        Console.WriteLine("Conexão aberta!");
                        //Executando o SQL
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Tabela criada!");
                        //Fechando a conexão
                        con.Close();
                        Console.WriteLine("Conexão fechada!");
                    }
                    //Demonstração de insert
                    Console.WriteLine("Inserindo uma nova linha na tabela, pressione qualquer tecla para continuar...");
                    //Aguardando ação do usuário
                    Console.ReadKey(true);
                    builder.Clear();
                    builder.Append("INSERT INTO endereco (logradouro, numero) ");
                    builder.Append("VALUES (@logradouro, @numero);");
                    sql = builder.ToString();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        //Parâmetros
                        cmd.Parameters.AddWithValue("@logradouro", "Rua Bady Bassit");
                        cmd.Parameters.AddWithValue("@numero","657");
                        //Abrindo a conexão
                        con.Open();
                        Console.WriteLine("Conexão aberta!");
                        //Número de linhas afetadas pelo SQL
                        int linhas = cmd.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(linhas + " linha(s) afetada(s)");
                        Console.ResetColor();
                        //Fechando a conexão
                        con.Close();
                        Console.WriteLine("Conexão fechada!");
                    }
                    string numero = "666";
                    //Demonstração de Update
                    Console.WriteLine("Atualizando o logradouro do número " + numero + ", pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                    builder.Clear();
                    builder.Append("UPDATE Endereco SET logradouro = 'Av. Indio Tibiriçá' WHERE numero = @numero;");
                    sql = builder.ToString();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@numero",numero);
                        //Abrindo a conexão
                        con.Open();
                        Console.WriteLine("Conexão aberta!");
                        int linhas = cmd.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(linhas + " linha(s) afetada(s)");
                        Console.ResetColor();
                        //Fechando a conexão
                        con.Close();
                        Console.WriteLine("Conexão fechada!");
                    }
                    //Demonstração de Delete
                    numero = "77";
                    Console.WriteLine("Excluindo o logradouro do número " + numero + ", pressione qualquer tecla para continuar");
                    Console.ReadKey(true);
                    builder.Clear();
                    builder.Append("DELETE FROM Endereco WHERE numero = @numero;");
                    sql = builder.ToString();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@numero", numero);
                        //Abrindo a conexão
                        con.Open();
                        Console.WriteLine("Conexão aberta!");
                        int linhas = cmd.ExecuteNonQuery();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(linhas + " linha(s) afetada(s)");
                        Console.ResetColor();
                        //Fechando a conexão
                        con.Close();
                        Console.WriteLine("Conexão fechada!");
                    }
                    //Demonstração select
                    Console.WriteLine("Lendo todos os registros da tabela, pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                    sql = @"SELECT * FROM Endereco;";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        //Abrindo a conexão
                        con.Open();
                        Console.WriteLine("Conexão aberta!");
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable schemaTable = reader.GetSchemaTable();
                            while (reader.Read())
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                Console.ResetColor();
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(reader.VisibleFieldCount + " registro(s) no banco de dados");
                            Console.ResetColor();
                        }
                        //Fechando a conexão
                        con.Close();
                        Console.WriteLine("Conexão fechada!");
                    }
                }
            }
            catch(SQLiteException e)
            {
                Console.WriteLine("Erro: " + e.ToString());
            }
        }
    }
}
