using Everything.Data;
using Everything.Data.Models;

namespace WebPortalEverthing.Services
{
    public class TagGameHelper
    {
        public class MoveTileRequest
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public List<int[]> ConvertToSerializableArray(int[,] array)
        {
            var list = new List<int[]>();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                var row = new int[array.GetLength(1)];

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j] = array[i, j];
                }
                list.Add(row);
            }

            return list;
        }

        public int[] ConvertToFlatArray(int[,] array)
        {
            var length = array.GetLength(0) * array.GetLength(1);

            var flatArray = new int[length];

            var count = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    flatArray[count] = array[i, j];
                    count++;
                }
            }

            return flatArray;
        }

        public int[,] ConvertFlatArrayToMultidimensional(int[] array)
        {
            var length = (int)Math.Sqrt(array.Length);

            var multidimensionalArray = new int[length, length];

            var count = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    multidimensionalArray[i, j] = array[count];
                    count++;
                }
            }

            return multidimensionalArray;
        }

        public int[] GetTagsFromDb(WebDbContext webDbContext, AuthService authService)
        {
            var userId = authService.GetUserId()!.Value;

            var tagGameData = webDbContext.TagGame.FirstOrDefault(x => x.Id == userId);

            if (tagGameData == null)
            {
                throw new Exception();
            }

            var tags = tagGameData.Tags;

            return tags;
        }

        public void CahangeTags(WebDbContext webDbContext, AuthService authService, int[,] tags)
        {
            var userId = authService.GetUserId()!.Value;

            var userData = webDbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (userData == null)
            {
                return;
            }

            var flatTags = ConvertToFlatArray(tags);

            webDbContext.TagGame.FirstOrDefault(x => x.Creator == userData)!.Tags = flatTags;

            webDbContext.SaveChanges();
        }
    }
}