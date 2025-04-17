using System.Reflection;
using System.Xml.Linq;

namespace ClassLibrary1.DAL.DbConnection;

public class SqlQueryLoader
{
    public string GetQueryFromXml(string queryName)
    {
        // Lấy đường dẫn đến file XML
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var xmlPath = Path.Combine(basePath!, "XML", "Dapper.xml");

        if (!File.Exists(xmlPath))
            throw new FileNotFoundException($"XML file not found at: {xmlPath}");

        // Đọc file XML
        var xml = XDocument.Load(xmlPath);

        // Tìm thẻ chứa câu truy vấn dựa trên tên truy vấn
        var queryElement = xml.Descendants()
            .FirstOrDefault(x => x.Name.LocalName == queryName);

        // Nếu tìm thấy câu truy vấn, trả về giá trị của nó, nếu không sẽ ném ngoại lệ
        return queryElement?.Value.Trim() ?? throw new Exception($"Query '{queryName}' not found in XML file.");
    }
}