namespace AirportProject4.Shared
{
    public static class GenertaPasswordHelper
    {
        public static string GeneratePassportNumber()
        {
            var random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return $"{letters[random.Next(letters.Length)]}{letters[random.Next(letters.Length)]}{random.Next(1000000, 9999999)}";
        }
    }
}
