namespace FootballApp.Common
{
    public static class EntityValidations
    {
        public static class LeagueValidations
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int CountryMaxLength = 50;
            public const int CountryMinLength = 3;

            public const int LogoMaxLength = 2048;
        }

        public static class ClubValidations
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int NicknameMaxLength = 50;
            public const int NicknameMinLength = 3;

            public const int LogoMaxLength = 2048;

            public const int WinsMinCount = 0;
            public const int DrawsMinCount = 0;
            public const int LosesMinCount = 0;
            public const int MatchesPlayedMinCount = 0;

            public const int ScoredGoalMinCount = 0;
            public const int ConcededGoalsMinCount = 0;

            public const int MinYear = 1800;
            public const int MaxYear = 2023;
        }
        public static class PlayerValidations
        {
            public const int FirstNameMaxLength = 50;
            public const int FirstNameMinLength = 2;

            public const int CountryMaxLength = 50;
            public const int CountryMinLength = 3;

            public const int PositionMaxLength = 15;
            public const int PositionMinLength = 2;

            public const int LastNameMaxLength = 50;
            public const int LastNameMinLength = 2;

            public const int PictureMaxLength = 2048;

            public const int NumberMinValue = 1;
            public const int NumberMaxValue = 99;

            public const int AgeMinValue = 15;
            public const int AgeMaxValue = 60;

            public const int GoalsMinCount = 0;
            public const int AssistsMinCount = 0;
            public const int MatchesPlayedMinCount = 0;
        }

        public static class StadiumValidations
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int CountryMaxLength = 50;
            public const int CountryMinLength = 3;

            public const int CityMaxLength = 20;
            public const int CityMinLength = 3;

            public const int AddressMaxLength = 150;
            public const int AddressMinLength = 10;

            public const int MinCapacity = 0;
            public const int MaxCapacity = int.MaxValue;
        }
    }
}
