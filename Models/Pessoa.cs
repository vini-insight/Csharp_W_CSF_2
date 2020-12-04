using System;
using System.Text.RegularExpressions;

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

public class Pessoa
{    
    // public string Nome { get; set; } = "Zerezima";
    // public string Cpf { get; set; } = "00000000000";
    // public string DataNascimento { get; set; } = "17/09/1988";
    // public char Sexo { get; set; } = 'M';

    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string DataNascimento { get; set; }
    public char Sexo { get; set; }
    
    public Boolean ChecarSeCamposPreenchidos() // checa se todos campos foram preenchidos
    {   
        //retiraEspacosBrancos = s.Trim();
        if (!String.IsNullOrWhiteSpace(this.Nome.Trim()) &&
            !String.IsNullOrWhiteSpace(this.Cpf.Trim()) &&
            !String.IsNullOrWhiteSpace(this.DataNascimento.Trim()) &&
            !String.IsNullOrWhiteSpace(this.Sexo.ToString().Trim()))
        {                
            return true;
        }
        else
        {
            throw new Exception("preencha todos os campos");
        }                                                         
    }
    
    public Boolean ChecarSeTemDoisNomes()
    {            
        if(this.Nome.Contains(" ")) // se um Nome tem espaços é porque tem pelo menos duas palavras na string
        {                    
            return true;
        }
        else
        {
            throw new Exception("digite o Nome completo com espaços entre eles");
        }            
    }

    public bool ValidarCpf()
    {
        if(this.Cpf.Length == 11)
        {
            char[] chars = this.Cpf.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i]))
                {
                    throw new Exception("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");
                }
            }
            return true;
        }
        else
        {            
            throw new Exception("CPF só tem 11 dígitos. você digitou mais (ou menos) do que 11");
        }
    }
    
    public bool ValidarData()
    {            
        DateTime dateValue;
        //Regex r = new Regex(@"(^\d{2}\/\d{2}\/\d{4}$)"); // ^ corresponde ao inicio e $ ao fim. (grupo indexado) os parenteses representam inicio e fim do grupo indexado        
        //if(r.Match(this.DataNascimento).Success)
        if (DateTime.TryParse(this.DataNascimento, out dateValue) && dateValue <= DateTime.Today) // datas com formato correto e não podem ser datas futuras
        {            
            return true;
        }
        else
        {            
            throw new Exception("data inválida: use o formato DD/MM/AAAA");
        }
    } 
    
    public Boolean ValidarSexo() // como o tipo de dado de Sexo é char, então mesmo que digite a palavra toda, vai pegar apenas a primeira letra.
    {            
        if(this.Sexo.ToString().Equals("F") ||
           this.Sexo.ToString().Equals("M") ||
           this.Sexo.ToString().Equals("f") ||
           this.Sexo.ToString().Equals("m")) // só é válido se for qualquer uma dessas opções
        {                    
            return true;
        }
        else
        {            
            throw new Exception("Sexo inválido: use o formato F ou f ou M ou m. sexo não pode ser vazio");
        }
    }
}
