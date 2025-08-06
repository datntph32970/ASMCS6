using Microsoft.AspNetCore.Components;

namespace AppView.Components.Models
{
    public class FormFieldConfig<T>
    {
        public string Label { get; set; } = string.Empty;
        public Func<T, string> ValueGetter { get; set; } = _ => string.Empty;
        public Action<T, string>? ValueSetter { get; set; }
        public RenderFragment<T>? Template { get; set; }
    }
}