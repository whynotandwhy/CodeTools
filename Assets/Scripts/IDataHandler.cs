public enum SaveMethod
{

}

public interface IDataHandler
{
    void LoadGrid(string stringIdentifier, SaveMethod fileInfo = default);
    void SaveGrid(string stringIdentifier, SaveMethod fileInfo = default);
}
