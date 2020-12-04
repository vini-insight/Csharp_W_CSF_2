using System;
using MySql.Data.MySqlClient;

public static class DB
{
    private const string CS = @"server=localhost;userid=root;password=root;database=nodejs";

    private static bool BuscarCpf(Pessoa p)
    {
        try
        {
            p.ValidarCpf();
            using var con = new MySqlConnection(CS);
            con.Open();            
            string sql = "SELECT cpf FROM pessoas WHERE (cpf = '" + p.Cpf + "')";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            string consulta = "";
            while (rdr.Read())
            {
                // Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetChar(4));
                // consulta += "" + rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetChar(4) + "";
                consulta += "" + rdr.GetString(0) + "";
            }
            con.Close();
            if(consulta.Equals(p.Cpf)) throw new Exception("Já existe alguém com este Cpf");
            else return false;
        }
        catch (System.Exception)
        {            
            throw; // relança exceção e preserva a pilha stack trace
        }
    }

    private static Pessoa ValidarCamposPessoa(Pessoa p)
    {   
        try        
        {
            // if(p.ChecarSeCamposPreenchidos() && p.ValidarSexo() && p.ChecarSeTemDoisNomes() && p.ValidarData() && p.ValidarCpf() && BuscarCpf(p))
            if(p.ChecarSeCamposPreenchidos() && p.ValidarSexo() && p.ChecarSeTemDoisNomes() && p.ValidarData())
                return p;
            else
                return null; //Console.WriteLine("reveja os campos que não foram validados aqui em acima");
        }        
        catch (System.Exception) // catch (Exception ex)
        {            
            throw; // throw ex;
        }
    }

    private static bool JaInserido(Pessoa p)
    {
        try
        {
            using var con = new MySqlConnection(CS);
            con.Open();
            string sql = "SELECT * FROM pessoas WHERE (nome = '" + p.Nome + "' AND cpf = '" + p.Cpf + "' AND dataNascimento = '" + p.DataNascimento + "' AND sexo = '" + p.Sexo + "')";            
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            string consulta = "";
            while (rdr.Read())
            {
                // Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetChar(4));
                consulta += "" + rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetChar(4) + "";
            }
            con.Close();            
            string sp = p.Nome + " " + p.Cpf + " " + p.DataNascimento + " " + p.Sexo;            
            if(consulta.Equals(sp)) return true;
            else return false;
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }
    
    public static Pessoa InserirNoBancoDados(Pessoa p)
    {            
        try
        {
            BuscarCpf(p);  
            ValidarCamposPessoa(p);          
            using var con = new MySqlConnection(CS);
            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "INSERT INTO pessoas (nome, cpf, dataNascimento, sexo) VALUES ('" + p.Nome + "', '" + p.Cpf + "', '" + p.DataNascimento + "', '" + p.Sexo + "')";            
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();                
            con.Close();
            return p;
        }    
        catch (System.Exception)
        {            
            throw; // relança exceção e preserva a pilha stack trace
        }
    }

    public static Pessoa AtualizarNoBancoDados(Pessoa p)
    {            
        try
        {
            using var con = new MySqlConnection(CS);
            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            // string query = "UPDATE pessoas SET nome = '" + p.Nome + "' WHERE cpf = '" + p.Cpf + "'";
            string query = "UPDATE pessoas SET nome = '" + p.Nome + "', cpf = '" + p.Cpf + "', dataNascimento = '" + p.DataNascimento + "', sexo = '" + p.Sexo + "' WHERE cpf = '" + p.Cpf + "'";                
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();                
            con.Close();
            return p;
        }
        catch (Exception ex)
        {            
            throw ex;
        }             
    }

    public static Pessoa ApagarNoBancoDados(string cpf)
    {    
        try       
        {
            using var con = new MySqlConnection(CS);
            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "DELETE FROM pessoas WHERE cpf = '" + cpf + "'";            
            cmd.CommandText = query;
            var linhasAfetadas = cmd.ExecuteNonQuery();
            con.Close();
            if (linhasAfetadas != 0) return new Pessoa { Cpf = cpf };
            else throw new Exception("NÃO ENCONTRADO");
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }        

    public static string BuscarNoBancoDados() // RETORNA TODAS AS PESSOAS NO BD
    {
        try
        {
            using var con = new MySqlConnection(CS);
            con.Open();
            string sql = "SELECT * FROM pessoas";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            string consulta = "";
            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetChar(4));
                consulta += "" + rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetChar(4) + "\n";
            }
            con.Close();           
            return consulta;
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    public static Pessoa GetPessoa(Pessoa p)
    {
        try
        {
            using var con = new MySqlConnection(CS);
            con.Open();
            // string sql = "SELECT * FROM pessoas WHERE (nome = '" + p.Nome + "' AND cpf = '" + p.Cpf + "' AND dataNascimento = '" + p.DataNascimento + "' AND sexo = '" + p.Sexo + "')";
            string sql = "SELECT * FROM pessoas WHERE cpf = '" + p.Cpf + "'";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();            
            Pessoa retornada = new Pessoa();
            while (rdr.Read())
            {
                // Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetChar(4));
                // consulta += "" + rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetChar(4) + "";
                retornada.Nome = rdr.GetString(1);
                retornada.Cpf = rdr.GetString(2);
                retornada.DataNascimento = rdr.GetString(3);
                retornada.Sexo = rdr.GetChar(4);
            }                        
            con.Close();   
            if(retornada.Cpf.Equals(p.Cpf)) return retornada;            
            else throw new Exception("NÃO ENCONTRADO");
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }

    public static Pessoa AtualizarPropriedades(Pessoa update, Pessoa pessoa)
    {
        if (update.Nome != null) pessoa.Nome = update.Nome;
        if (update.Cpf != null) pessoa.Cpf = update.Cpf;
        if (update.DataNascimento != null) pessoa.DataNascimento = update.DataNascimento;                
        if (update.Sexo != '\0') pessoa.Sexo = update.Sexo;
        return pessoa;
    }
}
