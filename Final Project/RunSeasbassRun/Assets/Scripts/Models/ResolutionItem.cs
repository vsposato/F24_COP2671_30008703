namespace Models
{
    public class ResolutionItem
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ResolutionItem(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}