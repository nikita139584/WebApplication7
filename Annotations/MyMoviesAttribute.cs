using System.ComponentModel.DataAnnotations;

namespace Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MyMoviesAttribute : ValidationAttribute
    {
        private static string[]? myMovies;

        public MyMoviesAttribute(string[] Movies)
        {
            myMovies = Movies;
        }

        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                string? strval = value.ToString();

                for (int i = 0; i < myMovies?.Length; i++)
                {
                    if (strval == myMovies[i])
                        return true;
                }
            }

            return false;
        }
    }
}