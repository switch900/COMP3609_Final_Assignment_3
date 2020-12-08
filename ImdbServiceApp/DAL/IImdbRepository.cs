using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ImdbServiceApp.DAL
{
        [ServiceContract]
        public interface IImdbRepository
        {
            [OperationContract]
            IEnumerable<Movies> GetAllMovies();

            [OperationContract]
            IEnumerable<Movies> GetAllMoviesByPredicate(string predicate);

            [OperationContract]
            Task<int> GetAllMoviesByPredicateCountAsync(string predicate);

            [OperationContract]
            IEnumerable<Movies> GetAllMoviesPaged(string predicate, int pagesize, int startindex);

            [OperationContract]
            int AddMovie(Movies movie);

            [OperationContract]
            Movies GetAllMoviesById(string id);

            [OperationContract]
            int UpdateMovie(Movies movie);

            [OperationContract]
            int DeleteMovieById(string id);
        }
}