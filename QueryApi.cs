using System.Linq.Expressions;

namespace QueryExpression;

public class QueryApi<T>
{
    public string HostAddresses { get;}
    private string _queryString { get; set; }

    public QueryApi(string x)
    {
        HostAddresses = x+"/"+typeof(T)?.Name;
    }

    public QueryApi<T> Filter(Expression<Func<T, bool>> exp)
    {
     Console.WriteLine(typeof(T).Name);   
        var body = (BinaryExpression)exp.Body;
        var parameter = ((MemberExpression)(body.Left)).Member.Name;
        Console.WriteLine(parameter);
        
        var constant = ((ConstantExpression)body.Right).Value;
        Console.WriteLine(body.NodeType.GetType());
        if (_queryString!=null&&_queryString.Length>0)
        {
            _queryString += "&&";
        }
        _queryString += $"{parameter}{ParseNodeType(body.NodeType)}{constant}";
        // exp.Compile();
        
        // -------------
   
        return this;
    }

    
    static string ParseNodeType(ExpressionType nodeType)
    {
        switch (nodeType)
        {
            case ExpressionType.Add:
                return "+";
            case ExpressionType.Subtract:
                return "-";
            case ExpressionType.Multiply:
                return "*";
            case ExpressionType.Divide:
                return "/";
            case ExpressionType.Modulo:
                return "%";
            case ExpressionType.Equal:
                return "==";
            case ExpressionType.NotEqual:
                return "!=";
            case ExpressionType.LessThan:
                return "<";
            case ExpressionType.LessThanOrEqual:
                return "<=";
            case ExpressionType.GreaterThan:
                return ">";
            case ExpressionType.GreaterThanOrEqual:
                return ">=";
            default:
                throw new Exception("operation not valid");
        }
    }

    public string getResult() => HostAddresses+"?"+_queryString;
}



