using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemApp_4J.Models;
using TestSystemApp_4J.Models.ViewModels;

namespace TestSystemApp_4J.Classes
{
    public static class TestDataClass
    {
        private static List<ViewModelResultUser> viewModelResultUsers;

        private static List<Result> results;
        private static List<User> users;

        public static List<Result> GetResults()
        {
            results = new List<Result>()
            {
                new Result() { Id = 1, UserId = 1, Scores = 12, StartDateTime =  DateTime.Now, EndDateTime = DateTime.Now },
                new Result() { Id = 2, UserId = 1, Scores = 3, StartDateTime =  DateTime.Now, EndDateTime = DateTime.Now },
                new Result() { Id = 3, UserId = 2, Scores = 6, StartDateTime =  DateTime.Now, EndDateTime = DateTime.Now },
                new Result() { Id = 4, UserId = 2, Scores = 1, StartDateTime =  DateTime.Now, EndDateTime = DateTime.Now }
            };

            return results;
        }

        public static List<User> GetUsers()
        {
            users = new List<User>()
            {
                new User() { Id = 1, Login = "ivan", Password = "123" },
                new User() { Id = 2, Login = "patya", Password = "123" },
                new User() { Id = 3, Login = "ilya", Password = "123" }
            };

            return users;
        }

        public static List<ViewModelResultUser> GetViewModelResultUsers()
        {
            viewModelResultUsers = new List<ViewModelResultUser>();

            int idUser = 1;

            var user = GetUsers().FirstOrDefault(x => x.Id == idUser);
            var results = GetResults().Where(x => x.UserId == idUser).ToList();

            int num = 1;
            foreach ( var result in results )
            {
                ViewModelResultUser vm = new ViewModelResultUser()
                {
                    Id = num, UserName = user.Login, Score = result.Scores,
                    MaxScores = result.MaxScores, StartDate = result.StartDateTime.ToShortDateString(),
                    EndDate = result.EndDateTime.ToShortDateString(),
                    StartTime = result.StartDateTime.ToShortTimeString(),
                    EndTime = result.EndDateTime.ToShortTimeString(),
                };

                num++;

                viewModelResultUsers.Add(vm);
            }


            return viewModelResultUsers;
        }
    }
}
