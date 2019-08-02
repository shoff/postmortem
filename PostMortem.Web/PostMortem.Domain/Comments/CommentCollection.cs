namespace PostMortem.Domain.Comments
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class CommentCollection : IList<Comment>, IReadOnlyCollection<Comment>
    {
        private readonly List<Comment> comments = new List<Comment>();

        private Guid questionId;

        // ReSharper disable once ConvertToAutoProperty
        public Guid QuestionId
        {
            get => this.questionId;
            internal set => this.questionId = value;
        }

        public IEnumerator<Comment> GetEnumerator()
        {
            return this.comments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(Comment comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            if (this.questionId == null || this.questionId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add comments unless the Question property has first been assigned.");
            }

            // no way this can be null, ReSharper is just being dense
            // ReSharper disable once PossibleNullReferenceException
            comment.QuestionId = this.questionId;
            this.comments.Add(comment);
        }

        public void Clear()
        {
            this.comments.Clear();
        }

        public bool Contains(Comment comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            return this.comments.Contains(comment);
        }

        // ReSharper disable once ParameterHidesMember
        public void CopyTo(Comment[] comments, int arrayIndex)
        {
            this.comments.CopyTo(comments, arrayIndex);
        }

        public bool Remove(Comment comment)
        {
            return this.comments.Remove(comment);
        }

        public int Count => this.comments.Count;
        public bool IsReadOnly => false;
        public int IndexOf(Comment comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            return this.comments.IndexOf(comment);
        }

        public void Insert(int index, Comment comment)
        {
            Guard.IsNotNull(comment, nameof(comment));
            if (this.questionId == null || this.questionId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add comments unless the Question property has first been assigned.");
            }

            // no way this can be null, ReSharper is just being dense
            // ReSharper disable once PossibleNullReferenceException
            comment.QuestionId = this.questionId;
            this.comments.Insert(index, comment);
        }

        public void RemoveAt(int index)
        {
            this.comments.RemoveAt(index);
        }

        public Comment this[int index]
        {
            get => this.comments[index];
            set
            {
                if (this.questionId == null || this.questionId == Guid.Empty)
                {
                    throw new ApplicationException(
                        "Cannot add comments unless the Question property has first been assigned.");
                }

                value.QuestionId = this.QuestionId;
                this.comments[index] = value;
            }
        }

        public void AddRange(ICollection<Comment> comments)
        {
            Guard.IsNotNull(comments, nameof(comments));
            if (this.questionId == null || this.questionId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Project property has first been assigned.");
            }

            comments.Each(dto => dto.QuestionId = this.questionId);
            this.comments.AddRange(comments);
        }
    }
}