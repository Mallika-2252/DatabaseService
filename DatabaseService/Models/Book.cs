using System;
using System.Collections.Generic;

namespace DatabaseService.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public DateOnly PublishedDate { get; set; }

    public int CopiesAvailable { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
