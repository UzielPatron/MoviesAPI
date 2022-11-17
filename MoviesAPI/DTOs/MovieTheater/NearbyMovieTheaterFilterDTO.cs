using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.MovieTheater
{
    public class NearbyMovieTheaterFilterDTO
    {
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Logitude { get; set; }
        public int distanceInKm = 10;
        public int maxDistanceInKm = 500;
        public int DistanceInKm
        {
            get
            {
                return distanceInKm;
            }
            set
            {
                distanceInKm = (value > maxDistanceInKm) ? maxDistanceInKm : value;
            }
        }
    }
}
