namespace Disintegrate.Customization
{
    /// <summary>
    /// Represents part of a parsed line, which may be either a string or field.
    /// </summary>
    public class LinePart
    {
        public LinePart(string value, PartKind kind)
        {
            Value = value;
            Kind = kind;
        }

        public enum PartKind { Field, String }

        /// <summary>
        /// The content of this part.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Whether the value of this part is a field's name or a string.
        /// </summary>
        public PartKind Kind { get; }

        public override string ToString() =>
            $"{Kind} ({Value})";
    }
}
