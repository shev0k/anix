﻿namespace AniX_Shared.DomainModels;

public class AnimeViews
{
    public int Id { get; set; }
    public int AnimeId { get; set; }
    public int UserId { get; set; }
    public DateTime ViewDate { get; set; }
}