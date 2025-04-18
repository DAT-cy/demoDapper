using System.Reflection;
using System.Xml.Linq;

namespace ClassLibrary1.DAL.DbConnection;

public class SqlQueryLoader
{
    public string GetQueryFromXml(string queryName)
    {
        // Lấy đường dẫn đến thư mục chứa file XML (nằm trong output)
        var basePath = Path.Combine(AppContext.BaseDirectory, "XML");

        if (!Directory.Exists(basePath))
            throw new DirectoryNotFoundException($"XML folder not found at: {basePath}");

        // Lấy tất cả file .xml trong thư mục
        var xmlFiles = Directory.GetFiles(basePath, "*.xml");

        foreach (var file in xmlFiles)
        {
            var xml = XDocument.Load(file);

            var queryElement = xml.Descendants()
                .FirstOrDefault(x => x.Name.LocalName == queryName);

            if (queryElement != null)
            {
                return queryElement.Value.Trim();
            }
        }

        throw new Exception($"Query '{queryName}' not found in any XML file in {basePath}");
    }
}