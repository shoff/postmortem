namespace PostMortem.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateProjectDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(512)]
        public string ProjectName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; } = DateTime.UtcNow.AddYears(1);
    }
}