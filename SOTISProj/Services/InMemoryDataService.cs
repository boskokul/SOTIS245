namespace SOTISProj.Services
{
    public class InMemoryDataService : IDataService
    {
        private string _parsedPDF = "";
        private string _acmSubTree = "";
        private string _termsRelations = "";

        
        public void SavePDFContent(string data)
        {
            _parsedPDF = data;
        }

        public string GetPDFContent()
        {
            return _parsedPDF;
        }

        public void SaveAcmSubTree(string data)
        {
            _acmSubTree = data;
        }

        public string GetAcmSubTree()
        {
            return _acmSubTree;
        }

        public void SaveTermsRelations(string data)
        {
            _termsRelations = data;
        }

        public string GetTermsRelations()
        {
            return _termsRelations;
        }
    }

}
