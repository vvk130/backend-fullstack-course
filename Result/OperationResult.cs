public class OperationResult<T>
{
    public bool Success => !ValidationErrors.Any();
    
    public T? Value { get; set; }

    public string? message { get; set; }
    
    public Dictionary<string, string[]> ValidationErrors { get; } = new();

    public void AddError(string key, string errorMessage)
    {
        if (!ValidationErrors.ContainsKey(key))
        {
            ValidationErrors[key] = new[] { errorMessage };
        }
        else
        {
            ValidationErrors[key] = ValidationErrors[key].Concat(new[] { errorMessage }).ToArray();
        }
    }
    
    public void AddErrors(Dictionary<string, string[]> errors)
    {
        foreach (var kvp in errors)
        {
            if (!ValidationErrors.ContainsKey(kvp.Key))
                ValidationErrors[kvp.Key] = kvp.Value;
            else
                ValidationErrors[kvp.Key] = ValidationErrors[kvp.Key].Concat(kvp.Value).ToArray();
        }
    }
}
