using Microsoft.AspNetCore.Components;

namespace AppView.Components.Models
{
    public class ColumnConfig<T>
    {
        public string Header { get; set; } = string.Empty;
        public Func<T, object?> FieldSelector { get; set; } = _ => null!;
        public RenderFragment<T>? Template { get; set; }
    }
}