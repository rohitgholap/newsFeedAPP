using System;
using System.Collections.Generic;

namespace NewsFeedApp.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
