namespace SOTISProj.Services
{
    public interface IDataService
    {
        void SavePDFContent(string data);

        string GetPDFContent();

        void SaveAcmSubTree(string data);

        string GetAcmSubTree();

        void SaveTermsRelations(Dictionary<string, RelatedTerms> data);

        Dictionary<string, RelatedTerms> GetTermsRelations();

        string GetTermsRelationsPairs();

        string GetTermsDefinitionsPairs();

        void SaveTermsDefinitions(Dictionary<string, string> data);

        Dictionary<string, string> GetTermsDefinitions();
    }

}
