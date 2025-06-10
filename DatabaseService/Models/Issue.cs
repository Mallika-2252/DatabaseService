using System;
using System.Collections.Generic;

namespace DatabaseService.Models;

public partial class Issue
{
    public int IssueId { get; set; }

    public int BookId { get; set; }

    public int MemberId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public bool? Returned { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
