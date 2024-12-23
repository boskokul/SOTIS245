﻿namespace SOTISProj.DTO
{
    public class TestParametersDTO
    {
        public string FieldName { get; set; }
        public int DefQuestionsNum { get; set; }
        public List<NumPair> ConQuestionsNum { get; set;}
        public string Name { get; set; }

        public TestParametersDTO() { ConQuestionsNum = new List<NumPair>(); }

        public TestParametersDTO(string fieldName, int defQuestionsNum, List<NumPair> questionsNum, string name)
        {
            FieldName = fieldName;
            DefQuestionsNum = defQuestionsNum;
            ConQuestionsNum = questionsNum;
            Name = name;
        }   
    }
}
