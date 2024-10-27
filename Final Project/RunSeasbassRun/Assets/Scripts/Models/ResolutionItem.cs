namespace Models
{
    /// <summary>
    /// Represents a resolution item with width and height properties.
    /// </summary>
    public class ResolutionItem
    {
        /// <summary>
        /// Gets the width of the resolution item.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the resolution item.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionItem"/> class.
        /// </summary>
        /// <param name="width">The width of the resolution item.</param>
        /// <param name="height">The height of the resolution item.</param>
        public ResolutionItem(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}