using System;
using System.Collections.Generic;

namespace NewsFeedApp.Models;

public partial class Article
{
    public long Id { get; set; }

    public string ArticleId { get; set; } = null!;

    public string ArticleTitle { get; set; } = null!;

    public string ArticleLink { get; set; } = null!;

    public int? AuthorId { get; set; }

    public DateTime? ArticleDate { get; set; }

    public bool Active { get; set; }

    public string CorrelationId { get; set; } = null!;

    public virtual Author? Author { get; set; }
}
