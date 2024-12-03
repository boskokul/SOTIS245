namespace SOTISProj.DTO
{
    public class NumPair
    {
        public int termNum {  get; set; }
        public int parentNum { get; set; }

        public NumPair() { }

        public NumPair(int termNum, int parentNum)
        {
            this.termNum = termNum;
            this.parentNum = parentNum;
        }

    }
}
