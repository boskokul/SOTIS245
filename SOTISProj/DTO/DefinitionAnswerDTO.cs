namespace SOTISProj.DTO
{
    public class DefinitionAnswerDTO
    {
        public int Id { get; set; }
        public int DefQuestionId { get; set; }
        public string Term { get; set; }
        public string AnsweredDefinition { get; set; }
        public bool IsCorrect { get; set; }
        public int TestSampleId { get; set; }
    }
}
