namespace Hydra.Presentation.Web.Models
{
    public class CourseViewModel
    {
        public string Id { get; set; }

        public int BootcampClassId { get; set; }

        public string CourseName { get; set; }

        public string TrainerName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EvaluationDate { get; set; }

        public int Progress { get; set; }
    }
}
