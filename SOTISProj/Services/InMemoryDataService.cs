namespace SOTISProj.Services
{
    public class InMemoryDataService : IDataService
    {
        private string _parsedPDF = "";
        private string _acmSubTree = "";
        private Dictionary<string, RelatedTerms> _termsRelations = new Dictionary<string, RelatedTerms>();
        private Dictionary<string, string> _termsDefinitions = new Dictionary<string, string>();

        
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

        public void SaveTermsRelations(Dictionary<string, RelatedTerms> data)
        {
            _termsRelations = data;
        }

        public Dictionary<string, RelatedTerms> GetTermsRelations()
        {
            return _termsRelations;
        }

        public void SaveTermsDefinitions(Dictionary<string,string> data)
        {
            _termsDefinitions = data;
        }

        public Dictionary<string,string> GetTermsDefinitions()
        {
            return _termsDefinitions;
        }
    }

}
