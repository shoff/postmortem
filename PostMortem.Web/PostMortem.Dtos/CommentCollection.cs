namespace PostMortem.Dtos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommentCollection : IList<CommentDto>
    {
        private readonly List<CommentDto> comments = new List<CommentDto>();

        private Guid questionId;

        // ReSharper disable once ConvertToAutoProperty
        public Guid QuestionId
        {
            get => this.questionId;
            internal set => this.questionId = value;
        }

        public IEnumerator<CommentDto> GetEnumerator()
        {
            return this.comments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(CommentDto commentDto)
        {
            if (this.questionId == null || this.questionId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add comments unless the Question property has first been assigned.");
            }
            commentDto.QuestionId = this.questionId;
            this.comments.Add(commentDto);
        }

        public void Clear()
        {
            this.comments.Clear();
        }

        public bool Contains(CommentDto item)
        {
            return this.comments.Contains(item);
        }

        public void CopyTo(CommentDto[] array, int arrayIndex)
        {
            this.comments.CopyTo(array, arrayIndex);
        }

        public bool Remove(CommentDto item)
        {
            return this.comments.Remove(item);
        }

        public int Count => this.comments.Count;
        public bool IsReadOnly => false;
        public int IndexOf(CommentDto item)
        {
            return this.comments.IndexOf(item);
        }

        public void Insert(int index, CommentDto item)
        {
            if (this.questionId == null || this.questionId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add comments unless the Question property has first been assigned.");
            }

            item.QuestionId = this.questionId;
            this.comments.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.comments.RemoveAt(index);
        }

        public CommentDto this[int index]
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
    }
}