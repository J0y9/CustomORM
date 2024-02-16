namespace CustomORM.ORM;

public class ConnectionString
{
    public static string MyConnectionString  = "Server=.\\SqlExpress;Initial Catalog=OrmDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public static string MasterConnectionString = ReplaceDbNameWithMaster();
    public static string GetDbName()
    {
        foreach (var s in MyConnectionString.Split(";"))
        {
            if (s.ToLower().Contains("initial catalog"))
            {
                return s.Remove(0,16);
            }
        }
        return "";
    }

    public static string ReplaceDbNameWithMaster()
    {
        return MyConnectionString.Replace(GetDbName(),"master");
    }
    
    
}