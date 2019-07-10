namespace PostMortem.Web.Dtos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ChaosMonkey.Guards;
    using Zatoichi.Common.Infrastructure.Extensions;

    public class QuestionCollection : IList<QuestionDto>
    {
        private readonly List<QuestionDto> questions = new List<QuestionDto>();
        private Guid projectId;

        // ReSharper disable once ConvertToAutoProperty
        public Guid ProjectId
        {
            get => this.projectId;
            internal set => this.projectId = value;
        }
        public IEnumerator<QuestionDto> GetEnumerator()
        {
            return this.questions.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public void Add(QuestionDto questionDto)
        {
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Project property has first been assigned.");
            }
            questionDto.QuestionId = this.projectId;
            this.questions.Add(questionDto);
        }
        public void Clear()
        {
            this.questions.Clear();
        }
        public bool Contains(QuestionDto item)
        {
            return this.questions.Contains(item);
        }
        public void CopyTo(QuestionDto[] array, int arrayIndex)
        {
            this.questions.CopyTo(array, arrayIndex);
        }
        public bool Remove(QuestionDto item)
        {
            return this.questions.Remove(item);
        }
        public int Count => this.questions.Count;
        public bool IsReadOnly => false;
        public int IndexOf(QuestionDto item)
        {
            return this.questions.IndexOf(item);
        }
        public void Insert(int index, QuestionDto item)
        {
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Question property has first been assigned.");
            }

            item.QuestionId = this.projectId;
            this.questions.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            this.questions.RemoveAt(index);
        }
        public QuestionDto this[int index]
        {
            get => this.questions[index];
            set
            {
                if (this.projectId == null || this.projectId == Guid.Empty)
                {
                    throw new ApplicationException(
                        "Cannot add questions unless the Question property has first been assigned.");
                }

                value.ProjectId = this.ProjectId;
                this.questions[index] = value;
            }
        }

        public void AddRange(ICollection<QuestionDto> dtos)
        {
            Guard.IsNotNull(dtos, nameof(dtos));
            if (this.projectId == null || this.projectId == Guid.Empty)
            {
                throw new ApplicationException(
                    "Cannot add questions unless the Project property has first been assigned.");
            }

            dtos.Each(dto => dto.ProjectId = this.projectId);
            this.questions.AddRange(dtos);
        }
    }
}