using PharmaGO.Core.Entities.Base;

namespace PharmaGO.Core.Entities;

public class Person : Entity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    
    /// <summary>
    /// ID do usuário no sistema de autenticação (ASP.NET Core Identity).
    /// Nullable porque nem toda Person precisa ter login (ex: clientes sem cadastro).
    /// 
    /// Nota arquitetural: Idealmente, o Domain não deveria conhecer Identity,
    /// mas optei por simplicidade e pragmatismo. Em sistemas do mundo real,
    /// considere usar uma tabela de mapeamento separada ou o mais adequado para o projeto.
    /// </summary>
    public Guid? IdentityUserId { get; set; }
}