namespace Library.Common
{
    public static  class EntityValidations
    {
        public static class BookValidations
        {
            public const int TitleMinLenght = 10;
            public const int TitleMaxLenght = 50;

            public const int AuthorMinLenght = 5;
            public const int AuthorMaxLenght = 50;

            public const int DescriptionMinLenght = 5;
            public const int DescriptionMaxLenght = 5000;

            public const int ImageUrlMinLenght = 5;
            public const int ImageUrlMaxLenght = 2000;
        }

        public static class CategoryValidations
        {
            public const int NameMinLenght = 5;
            public const int NameMaxLenght = 50;
        }
    }
}
