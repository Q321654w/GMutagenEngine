namespace GMutagenEngine.Infrastructure.DynamicSchemas.Tests.Contexts.Generation;

public class Company
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }
    public Dictionary<string, Person> ManagersByDepartment { get; set; }
}