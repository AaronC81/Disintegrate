namespace Disintegrate.Customization
{
    /// <summary>
    /// A text field which may be replaced with a value by a <see cref="PresenceProvider"/>.
    /// </summary>
    public class TextField
    {
        public TextField(string name, string example)
        {
            Name = name;
            Example = example;
        }

        /// <summary>
        /// The name of this field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// An example of a value which this field could contain, used for generating previews..
        /// </summary>
        public string Example { get; }
    }
}
