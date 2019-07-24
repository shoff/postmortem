namespace PostMortem.Domain.Questions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionCollection : IList<Question>, IReadOnlyCollection<Question>
    {
        private readonly List<Question> questions = new List<Question>();
        private Guid projectId;

        // ReSharper disable once ConvertToAutoProperty
        public Guid ProjectId
        {
            get => this.projectId;
            internal set => this.projectId = value;
        }
        public IEnumerator<Question> GetEnumerator()
        {
            return this.questions.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public void Add(Question question)
        {
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Project property has first been assigned.");
            }
            question.ProjectId = this.projectId;
            this.questions.Add(question);
        }
        public void Clear()
        {
            this.questions.Clear();
        }
        public bool Contains(Question item)
        {
            return this.questions.Contains(item);
        }
        public void CopyTo(Question[] array, int arrayIndex)
        {
            this.questions.CopyTo(array, arrayIndex);
        }
        public bool Remove(Question item)
        {
            return this.questions.Remove(item);
        }
        public int Count => this.questions.Count;
        public bool IsReadOnly => false;
        public int IndexOf(Question item)
        {
            return this.questions.IndexOf(item);
        }
        public void Insert(int index, Question item)
        {
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the CommentId property has first been assigned.");
            }

            item.ProjectId = this.projectId;
            this.questions.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            this.questions.RemoveAt(index);
        }
        public Question this[int index]
        {
            get => this.questions[index];
            set
            {
                if (this.projectId == null || this.projectId == Guid.Empty)
                {
                    throw new ApplicationException(
                        "Cannot add questions unless the CommentId property has first been assigned.");
                }

                value.ProjectId = this.ProjectId;
                this.questions[index] = value;
            }
        }

        public void AddRange(IEnumerable<Question> dtos)
        {
            Guard.IsNotNull(dtos, nameof(dtos));
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Project property has first been assigned.");
            }

            dtos.Each(dto => dto.ProjectId = this.projectId); //TODO: Is this even needed?
            this.questions.AddRange(dtos);
        }
    }
}