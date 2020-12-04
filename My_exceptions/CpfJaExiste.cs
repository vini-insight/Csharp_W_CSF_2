using System;

public class CpfJaExiste : Exception
{
    public CpfJaExiste() : base ("Já existe alguém com este Cpf")
    {
        // 
    }
    // throw new Exception("Já existe alguém com este Cpf");    
}
