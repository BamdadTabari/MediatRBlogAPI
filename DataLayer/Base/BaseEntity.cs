﻿namespace DataLayer;
public interface IBaseEntity { }

public class BaseEntity : IBaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string Slug { get; set; }
}
