using ExpressionConsole;
using System.Linq;
using System.Linq.Expressions;

var girls = new List<Girl>()
{
    new Girl{ Age = 30, Name = "A", IsAlive = true },
    new Girl{ Age = 31, Name = "A", IsAlive = true },
    new Girl{ Age = 32, Name = "A", IsAlive = false },
    new Girl{ Age = 20, Name = "B", IsAlive = false },
    new Girl{ Age = 25, Name = "B", IsAlive = false },
    new Girl{ Age = 40, Name = "Z", IsAlive = true },
    new Girl{ Age = 10, Name = "C", IsAlive = false },
};
var girlsQueryable = girls.AsQueryable();

Console.WriteLine("Enter property for ordering");
var fieldName = Console.ReadLine();

Console.WriteLine("Enter property Name for searching");
var fieldNameForFilter = Console.ReadLine();
Console.WriteLine("Enter value for searching");
var fieldValueForFilter = Console.ReadLine();

// girl
var paramExp = Expression.Parameter(typeof(Girl), "girl");

// girl.Name
var propertyExp = Expression.Property(paramExp, fieldName);

// girl.Name
var propertyExpAsObject = Expression.Convert(propertyExp, typeof(object));

// girl => girl.Name
var linqExp = Expression.Lambda<Func<Girl, object>>(
    propertyExpAsObject,
    paramExp
    );

var ordered = girlsQueryable
    .OrderBy(linqExp);

// girl.Name
var propertyForFilteringExp = Expression.Property(paramExp, fieldNameForFilter);
var constExp = Expression.Constant(fieldValueForFilter);
// girl.Name == "A"
var isEqExp = Expression.Equal(propertyForFilteringExp, constExp);

var nullExp = Expression.Constant(null);
// girl.Name == null
var isNullExp = Expression.Equal(propertyForFilteringExp, nullExp);

// girl.Name == "A" || girl.Name == null
var orExp = Expression.Or(isEqExp, isNullExp);

// girl => girl.Name == "A" || girl.Name == null
var linqForFilterExp = Expression.Lambda<Func<Girl, bool>>(
    orExp,
    paramExp
    );

var filtered = ordered.Where(linqForFilterExp);

Console.WriteLine(string.Join("\r\n", filtered));