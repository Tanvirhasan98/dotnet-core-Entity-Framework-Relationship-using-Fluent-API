namespace EFCoreStudentRelationship
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
       

        public virtual ICollection<Student> Students { get; set; }
      

    }
}
