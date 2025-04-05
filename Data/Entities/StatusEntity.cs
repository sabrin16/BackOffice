﻿using System.ComponentModel.DataAnnotations;


namespace Data.Entities;

[Microsoft.EntityFrameworkCore.Index(nameof(StatusName), IsUnique = true)]

public class StatusEntity
{
    [Key]

    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
