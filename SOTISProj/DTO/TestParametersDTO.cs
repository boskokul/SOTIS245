namespace SOTISProj.DTO
{
    public class TestParametersDTO
    {
        public string FieldName { get; set; }
        public int DefQuestionsNum { get; set; }
        public List<NumPair> COnQuestionsNum { get; set;}

        public TestParametersDTO() { COnQuestionsNum = new List<NumPair>(); }

        public TestParametersDTO(string fieldName, int defQuestionsNum, List<NumPair> questionsNum)
        {
            FieldName = fieldName;
            DefQuestionsNum = defQuestionsNum;
            COnQuestionsNum = questionsNum;
        }   
    }
}
