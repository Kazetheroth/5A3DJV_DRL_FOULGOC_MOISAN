namespace Utils
{
    public class Vector2Int
    {
        public int x { set; get; }
        public int y { set; get; }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return x + " " + y;
        }
    }
}