namespace SOTISProj.Services
{
    public interface IDataService
    {
        void SavePDFContent(string data);

        string GetPDFContent();

        void SaveAcmSubTree(string data);

        string GetAcmSubTree();

        void SaveTermsRelations(string data);

        string GetTermsRelations();
    }

}
