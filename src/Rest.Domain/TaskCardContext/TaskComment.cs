using System;

namespace Rest.Domain.TaskCardContext;

public class TaskComment
{
    public long Id { get; private set; }
    public DateTime Created { get; private set; }
    public string Comment { get; private set; }
    public string CommentedBy { get; private set; }

    public TaskComment(string comment, string commentedBy)
    {
        Created = DateTime.Now;
        Comment = comment;
        CommentedBy = commentedBy;
    }
}
