namespace TagGame.Classes.Base
{
    public class Field
    {
        private Random _random = new();

        private int[,] _tags;

        public Field(int count)
        {
            _tags = new int[count, count];
            var valueCount = 1;

            for (var masX = 0; masX < _tags.GetLength(0); masX++)
            {
                for (var masY = 0; masY < _tags.GetLength(1); masY++)
                {
                    _tags[masX, masY] = valueCount;
                    valueCount++;
                }
            }
            _tags[count - 1, count - 1] = 0;
        }

        public void Change()
        {
            var countOfRotates = _tags.GetLength(0) * _tags.GetLength(1) * 2;

            var isCool = true;
            while (isCool)
            {
                for (var i = 0; i < countOfRotates; i++)
                {
                    var randomFieldX1 = _random.Next(0, _tags.GetLength(0));
                    var randomFieldY1 = _random.Next(0, _tags.GetLength(1));

                    var randomFieldX2 = _random.Next(0, _tags.GetLength(0));
                    var randomFieldY2 = _random.Next(0, _tags.GetLength(1));

                    var changeInt = _tags[randomFieldX1, randomFieldY1];

                    _tags[randomFieldX1, randomFieldY1] = _tags[randomFieldX2, randomFieldY2];

                    _tags[randomFieldX2, randomFieldY2] = changeInt;
                }
                if (IsSolvable(_tags))
                {
                    isCool = false;
                }
            }
        }

        public int[,] GetTags()
        {
            return _tags;
        }

        public void ChangePositions(int newPositionX, int newPositionY)
        {
            var oldPositionX = 0;
            var oldPositionY = 0;

            for (var masX = 0; masX < _tags.GetLength(0); masX++)
            {
                for (var masY = 0; masY < _tags.GetLength(1); masY++)
                {
                    if (_tags[masX, masY] == 0)
                    {
                        oldPositionX = masX;
                        oldPositionY = masY;
                    }
                }
            }

            var changeValue = _tags[oldPositionX, oldPositionY];
            _tags[oldPositionX, oldPositionY] = _tags[newPositionX, newPositionY];
            _tags[newPositionX, newPositionY] = changeValue;
        }

        public bool IsWin()
        {
            var winMas = _tags
                    .Cast<int>()
                    .Where(x => x != 0)
                    .OrderBy(x => x)
                    .Concat(_tags.Cast<int>().Where(x => x == 0))
                    .ToArray();

            var ourMas = _tags.Cast<int>().ToArray();

            return ourMas.SequenceEqual(winMas);
        }

        public bool IsSolvable(int[,] tags)
        {
            int[] flatTags = tags.Cast<int>().ToArray();
            int inversionCount = 0;

            for (int i = 0; i < flatTags.Length; i++)
            {
                for (int j = i + 1; j < flatTags.Length; j++)
                {

                    if (flatTags[i] != 0 && flatTags[j] != 0 && flatTags[i] > flatTags[j])
                    {
                        inversionCount++;
                    }
                }
            }

            return inversionCount % 2 == 0;
        }
    }
}
