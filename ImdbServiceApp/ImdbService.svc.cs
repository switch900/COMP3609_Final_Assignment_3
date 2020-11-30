using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace ImdbServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ImdbService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ImdbService.svc or ImdbService.svc.cs at the Solution Explorer and start debugging.
    public class ImdbService : IImdbService
    {
        public void DoWork()
        {
        }

        public List<Movies> GetAllMovies()
        {
            List<Movies> userlst = new List<Movies>();
            ImdbEntities tstDb = new ImdbEntities();
            var lstUsr = from k in tstDb.Movies select k;
            foreach (var item in lstUsr)
            {
                Movies usr = new Movies();
                usr.tconst = item.tconst;
                usr.titleType = item.titleType;
                usr.primaryTitle = item.primaryTitle;
                usr.originalTitle = item.originalTitle;
                usr.isAdult = item.isAdult;
                usr.startYear = item.startYear;
                usr.endYear = item.endYear;
                usr.runtimeMinutes = item.runtimeMinutes;
                usr.genres = item.genres;
                userlst.Add(usr);
            }
            return userlst;
        }

        public Movies GetAllMoviesById(string id)
        {

            ImdbEntities tstDb = new ImdbEntities();
            var lstUsr = from k in tstDb.Movies where k.tconst == id select k;
            Movies usr = new Movies();
            foreach (var item in lstUsr)
            {
                
                usr.tconst = item.tconst;
                usr.titleType = item.titleType;
                usr.primaryTitle = item.primaryTitle;
                usr.originalTitle = item.originalTitle;
                usr.isAdult = item.isAdult;
                usr.startYear = item.startYear;
                usr.endYear = item.endYear;
                usr.runtimeMinutes = item.runtimeMinutes;
                usr.genres = item.genres;


            }

            return usr;
        }

        public int DeleteMovieById(string Id)
        {
            ImdbEntities tstDb = new ImdbEntities();
            Movies usrdtl = new Movies();
            usrdtl.tconst = Id;
            tstDb.Entry(usrdtl).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int AddMovie(string Name, string Email)
        {
            ImdbEntities tstDb = new ImdbEntities();
            Movies usrdtl = new Movies();
            /*usrdtl.Name = Name;
            usrdtl.Email = Email;*/
            tstDb.Movies.Add(usrdtl);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }
        public int UpdateMovie(int Id, string Name, string Email)
        {
            ImdbEntities tstDb = new ImdbEntities();
            Movies usrdtl = new Movies();
           /* usrdtl.Id = Id;
            usrdtl.Name = Name;
            usrdtl.Email = Email;*/
            tstDb.Entry(usrdtl).State = EntityState.Modified;

            int Retval = tstDb.SaveChanges();
            return Retval;
        }

    }
}  


