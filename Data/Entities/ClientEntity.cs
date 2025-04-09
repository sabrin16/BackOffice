using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Microsoft.EntityFrameworkCore.Index(nameof(ClientName), IsUnique = true)]

public class ClientEntity
{
    [Key]

    public string Id { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}

public class Project
{
     [Key]

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Image { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "date")] 
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    public decimal? Budget {  get; set; }

    public DateTime Created {  get; set; } = DateTime.Now;


    [ForeignKey(nameof(Client))]

    public string ClientId { get; set; } = null!;

    public virtual ClientEntity Client { get; set; } = null!;


    [ForeignKey(nameof(User))]

    public string UserId { get; set; } = null!;

    public virtual UserEntity User { get; set; } = null!;


    [ForeignKey(nameof(Status))]

    public int StatusId { get; set; }

    public virtual StatusEntity Status { get; set; }
}