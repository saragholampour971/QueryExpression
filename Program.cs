
using System.Linq.Expressions;
using QueryExpression;


QueryApi<Invoice> x=new QueryApi<Invoice>("localhost:3000");
var ans=x.Filter(invoice => invoice.TotalQuantity>3).Filter(i=>i.Number=="sara").getResult();
Console.WriteLine(ans);

