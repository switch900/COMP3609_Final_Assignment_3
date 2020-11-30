using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImdbServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IImdbService" in both code and config file together.
    [ServiceContract]
    public interface IImdbService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<Movies> GetAllMovies();

        [OperationContract]
        int AddMovie(string Name, string Email);

        [OperationContract]
        Movies GetAllMoviesById(string id);

        [OperationContract]
        int UpdateMovie(int Id, string Name, string Email);

        [OperationContract]
        int DeleteMovieById(string Id);
    }
}
